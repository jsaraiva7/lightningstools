﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.HardwareSupport;
using Common.MacroProgramming;
using Common.SimLinkup.Configuration;
using Common.SimLinkup.Scripting;
using Common.SimLinkup.Signals;
using Common.SimSupport;
using log4net;

namespace Common.SimLinkup.Runtime
{
    public class Runtime
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Runtime));
        private readonly List<SignalMapping> _mappings = new List<SignalMapping>();
        private SimLinkupConfig _cfg = SimLinkupConfig.GetConfig();
        private bool _initialized;
        private bool _keepRunning;
        private SimLinkupConfig _settings = SimLinkupConfig.GetConfig();
        private AnalogSignal _loopDurationSignal;
        private AnalogSignal _loopFrequencySignal;
        private uint _desiredFrequency = 60;
        private double _AdjustFrequency =  1000 / 60;
        public Runtime()
        {


            if (_settings == null)
            {
                _settings = new SimLinkupConfig();
            }
            _desiredFrequency = _settings.DesiredFrequency;
            _AdjustFrequency = 1000 / _settings.DesiredFrequency;

            Initialize();
            
        }

        public bool IsRunning { get; private set; }

        public IEnumerable<SignalMapping> Mappings => _mappings;
        public ScriptingContext ScriptingContext { get; private set; }

        public static IHardwareSupportModule[] GetRegisteredHardwareSupportModules()
        {
            //get a list of hardware support modules that are currently registered
            //var hsmRegistry =
            //    HardwareSupportModuleRegistry.Load(
            //        Path.Combine(Util.CurrentMappingProfileDirectory, "HardwareSupportModule.registry"));

            //var modules = hsmRegistry.GetInstances();
            var loader = new HardwareSupportModuleLoader();
            var modules = loader.LoadHardwareSupportModules(SimLinkupConfig.GetConfig().HsmDir, true);
            return modules?.ToArray();
        }

        public static SimSupportModule[] GetRegisteredSimSupportModules()
        {
            //get a list of sim support modules that are currently registered
            //var ssmRegistry =
            //    SimSupportModuleRegistry.Load(Path.Combine(Util.CurrentMappingProfileDirectory,
            //        "SimSupportModule.registry"));
            //var modules = ssmRegistry.GetInstances();
            var loader = new SimSupportModuleLoader();
            var modules = loader.LoadSimSupportModules(SimLinkupConfig.GetConfig().SsmDir, true);
            return modules?.ToArray();
        }

        public void Start()
        {
            _desiredFrequency =  _settings.DesiredFrequency;
            Task.Run(() => MainLoop());
        }

        public void Stop()
        {
            _keepRunning = false;
            var startWaitingTime = DateTime.Now;
            while (IsRunning)
            {
                Thread.Sleep(5);
                var currentTime = DateTime.Now;
                var elapsed = currentTime.Subtract(startWaitingTime);
                if (elapsed.TotalSeconds > 5)
                {
                    IsRunning = false;
                }
            }
            IsRunning = false;
        }

        private void AddPerformanceMonitoringSignals()
        {
            _loopDurationSignal =
                new AnalogSignal
                {
                    Category = "Metrics",
                    CollectionName = "Performance",
                    FriendlyName = "Loop Duration (ms)",
                    Id = "SIMLINKUP__PERFORMANCE__LOOP_DURATION",
                    Index = 0,
                    PublisherObject = this,
                    Source = this,
                    SourceFriendlyName = nameof(SimLinkup),
                    IsAngle = false,
                    IsPercentage = false,
                    MinValue = 0,
                    MaxValue = 1000
                };
            ScriptingContext[_loopDurationSignal.Id] = _loopDurationSignal;


            _loopFrequencySignal =
                new AnalogSignal
                {
                    Category = "Metrics",
                    CollectionName = "Performance",
                    FriendlyName = "Loop Frequency (Hz)",
                    Id = "SIMLINKUP__PERFORMANCE__LOOP_FREQUENCY",
                    Index = 0,
                    PublisherObject = this,
                    Source = this,
                    SourceFriendlyName = nameof(SimLinkup),
                    IsAngle = false,
                    IsPercentage = false,
                    MinValue = 0,
                    MaxValue = 1000
                };
            ScriptingContext[_loopFrequencySignal.Id] = _loopFrequencySignal;
        }

        private void ExecuteOneLoopIteration()
        {

            if(_loopFrequencySignal.State - _desiredFrequency < -0.5)
            {
                _AdjustFrequency -= 0.3;
            }
            else if (_loopFrequencySignal.State - _desiredFrequency > 0.5)
            {
                _AdjustFrequency += 0.3;
            }
            var startTime = DateTime.UtcNow;
            UpdateSimSignals();
            Synchronize();
            var elapsed = DateTime.UtcNow.Subtract(startTime).TotalMilliseconds;
            var toSleep = (int)(_AdjustFrequency - (int) elapsed);
            if (toSleep < 0) toSleep = 1;
            Thread.Sleep(toSleep);
            var endTime = DateTime.UtcNow;
            var loopDuration = endTime.Subtract(startTime).TotalMilliseconds;
            if (loopDuration <= 0) loopDuration = 1;
            _loopDurationSignal.State = loopDuration;
            _loopFrequencySignal.State = 1000.0 / loopDuration;
            
       
        }


        private void Initialize()
        {
            if (_initialized) return;
            ScriptingContext = new ScriptingContext
            {
                SimSupportModules = GetRegisteredSimSupportModules(),
                HardwareSupportModules = GetRegisteredHardwareSupportModules()
            };

            AddPerformanceMonitoringSignals();
            InitializeMappings();
            _initialized = true;
        }

        private void InitializeMappings()
        {


            //var mappingFiles = new DirectoryInfo(Util.CurrentMappingProfileDirectory).GetFiles("*.mapping");
            //JSA Edit - Load Mapping Dir From Config File
            var cfg = SimLinkupConfig.GetConfig();
            if(cfg == null)
                return;
            var mappingFiles = new DirectoryInfo(cfg.MappingDir).GetFiles("*.mapping", SearchOption.AllDirectories);
            foreach (var mappingFile in mappingFiles)
            {
                var profileToLoad = mappingFile.FullName;
                if (!string.IsNullOrEmpty(profileToLoad) && !string.IsNullOrEmpty(profileToLoad.Trim()))
                {
                    var profile = MappingProfile.Load(profileToLoad);
                    foreach (var mapping in profile.SignalMappings)
                    {
                        var origSource = mapping.Source;
                        var origDestination = mapping.Destination;
                        mapping.Source = ResolveSignal(origSource);
                        mapping.Destination = ResolveSignal(origDestination);
                        if (mapping.Source == null || mapping.Destination == null)
                        {
                            _log.Warn(
                                $"A mapping defined in file {profileToLoad} had an unresolvable source or destination signal.");
                            continue;
                        }
                        _mappings.Add(mapping);

                        if (mapping.Source is AnalogSignal)
                        {
                            var mappingSource = mapping.Source as AnalogSignal;
                            var mappingDestination = mapping.Destination as AnalogSignal;

                            var passthru = new AnalogPassthrough {In = mappingSource, Out = mappingDestination};
                            passthru.Refresh();
                        }
                        else if (mapping.Source is DigitalSignal)
                        {
                            var mappingSource = mapping.Source as DigitalSignal;
                            var mappingDestination = (DigitalSignal) mapping.Destination;
                            var passthru = new DigitalPassthrough {In = mappingSource, Out = mappingDestination};
                            passthru.Refresh();
                        }
                        else if (mapping.Source is TextSignal)
                        {
                            var mappingSource = mapping.Source as TextSignal;
                            var mappingDestination = (TextSignal) mapping.Destination;
                            var passthru = new TextPassthrough {In = mappingSource, Out = mappingDestination};
                            passthru.Refresh();
                        }
                    }
                }
            }
        }

        private void MainLoop()
        {
            _keepRunning = true;
            IsRunning = true;
            while (_keepRunning)
                ExecuteOneLoopIteration();
            IsRunning = false;
        }

        private Signal ResolveSignal(Signal signalToResolve)
        {
            if (signalToResolve == null) return null;
            return
                ScriptingContext.AllSignals.FirstOrDefault(
                    signal =>
                        signal.Id != null && signalToResolve.Id != null &&
                        signal.Id.Trim().Equals(signalToResolve.Id.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private void Synchronize()
        {
            ScriptingContext.HardwareSupportModules?.ToList().ForEach(hsm => hsm.Synchronize());
        }

        private void UpdateSimSignals()
        {
            ScriptingContext.SimSupportModules?.ToList().ForEach(ssm => ssm.Update());
        }
    }
}