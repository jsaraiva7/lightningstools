using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using Common.HardwareSupport;
using Common.HardwareSupport.MotorControl;
using Common.MacroProgramming;
using log4net;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Inputs;
using PhccHardwareSupportModule.Phcc.Interfaces;
using PhccHardwareSupportModule.Phcc.Peripherals.Classes;
using p = Phcc;
 

namespace PhccHardwareSupportModule.Phcc
{
    public class PhccHardwareSupportModule : HardwareSupportModuleBase, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private readonly AnalogSignal[] _analogInputSignals;
        private readonly AnalogSignal[] _analogOutputSignals;
        private readonly DigitalSignal[] _digitalInputSignals;
        private readonly DigitalSignal[] _digitalOutputSignals;
        private readonly Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private p.AnalogInputChangedEventHandler _analogInputChangedEventHandler;
        private p.Device _device;
        private p.DigitalInputChangedEventHandler _digitalInputChangedEventHandler;
        private p.I2CDataReceivedEventHandler _i2cDataReceivedEventHandler;
        private bool _isDisposed;

        private PhccHardwareSupportModule()
        {
        }


        /// <summary>
        /// static method added to allow module initialization from other contexts. Returns an HSM object, that can be used to calibrate the outputs. (by actually moving the needles!)
        /// the returned HSM ignores any previous calibrations!
        /// </summary>
        /// <param name="motherboard"></param>
        /// <returns></returns>
        public static PhccHardwareSupportModule GetModuleForCalibration(Motherboard motherboard)
        {
            var hsm = new PhccHardwareSupportModule(motherboard);
            foreach (var calOutput in hsm._analogOutputSignals)
            {
                if (calOutput is CalibratedAnalogSignal)
                {
                    var cal = calOutput as CalibratedAnalogSignal;
                    cal.CalibrationData = null;
                }
                
            }
            return hsm;
        }


        private PhccHardwareSupportModule(Motherboard motherboard) : this()
        {
            try
            {
                if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
                _device = CreateDevice(motherboard.ComPort);
                _analogInputSignals = new PhccAnalogInputs(_device).AnalogInputs.ToArray();
                _digitalInputSignals = new PhccDigitalImputs(_device).DigitalInputs.ToArray();

                CreateOutputSignals(_device, motherboard, out _digitalOutputSignals, out _analogOutputSignals);

                CreateInputEventHandlers();
                RegisterForInputEvents(_device, _i2cDataReceivedEventHandler);
                try
                {
                    StartTalking(_device);
                }
                catch (Exception e)
                {
                    _log.Error(e.Message, e);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
           
        

        public override AnalogSignal[] AnalogInputs => _analogInputSignals;

        public override AnalogSignal[] AnalogOutputs => _analogOutputSignals;

        public override DigitalSignal[] DigitalInputs => _digitalInputSignals;

        public override DigitalSignal[] DigitalOutputs => _digitalOutputSignals;
     

        public override string FriendlyName => "PHCC";

     

        public static IHardwareSupportModule[] GetInstances()
        {
            var toReturn = new List<IHardwareSupportModule>();
            try
            {
                var hsmConfigFilePath = Path.Combine(Util.CurrentMappingProfileDirectory,
                    "PhccHardwareSupportModule.config");
                var hsmConfig = PhccHardwareSupportModuleConfig.Load(hsmConfigFilePath);
                var phccDeviceManagerConfigFilePath = hsmConfig.PhccDeviceManagerConfigFilePath;
                var phccConfigManager = LoadConfiguration(phccDeviceManagerConfigFilePath) ??
                                        LoadConfiguration(Path.Combine(Util.CurrentMappingProfileDirectory,
                                            phccDeviceManagerConfigFilePath));

                foreach (var m in phccConfigManager.Motherboards)
                {
                    IHardwareSupportModule thisHsm = new PhccHardwareSupportModule(m);
                    toReturn.Add(thisHsm);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
            return toReturn.ToArray();
        }

        

        #region Create Input Signals

         



        #endregion

        #region Create Output Signals

       


        private void CreateOutputSignals(p.Device device, Motherboard motherboard, out DigitalSignal[] digitalSignals,
           out AnalogSignal[] analogSignals)
        {
            if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
            var portName = motherboard.ComPort;
            var digitalSignalsToReturn = new List<DigitalSignal>();
            var analogSignalsToReturn = new List<AnalogSignal>();

            var modules = new List<IPeripheral>();

            var attachedPeripherals = motherboard.Peripherals;

            foreach (var peripheral in attachedPeripherals)
            {
                if (peripheral is Doa40Do)
                {              
                    var test = new HSMDoa40DO();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }
                else if (peripheral is Doa7Seg)
                {              
                    var test = new HSMDoa7Seg();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }
                else if (peripheral is Doa8Servo)
                {            
                    var test = new HSMDoa8Servo();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }
                else if (peripheral is DoaAirCore)
                {     
                    var test = new HSMDoaAirCore();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }
                else if (peripheral is DoaAnOut1)
                {                  
                    var test = new HSMDoaAnOut1();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }
                else if (peripheral is DoaStepper)
                {
                    var test = new HSMDoaStepper();
                    test.DigitalInputs = _digitalInputSignals.ToList();
                    test.AnalogInputs = _analogInputSignals.ToList();
                    test.InitializeSignals(peripheral, device);
                    modules.Add(test);
                }

            }

            foreach (var peripheral in modules)
            {
               
                if (peripheral.AnalogOutputs != null)
                {
                    analogSignalsToReturn.AddRange(peripheral.AnalogOutputs);
                }
                if (peripheral.DigitalOutputs != null)
                {
                    digitalSignalsToReturn.AddRange(peripheral.DigitalOutputs);
                }

              
                
               
            }

            analogSignals = analogSignalsToReturn.ToArray();
            digitalSignals = digitalSignalsToReturn.ToArray();
        }

         

        #endregion

        #region Event Handlers

        private void CreateInputEventHandlers()
        {
 
            _i2cDataReceivedEventHandler = device_I2CDataReceived;
        }

        private void AbandonInputEventHandlers()
        {

            _i2cDataReceivedEventHandler = null;
        }
 
  




        private static ConfigurationManager LoadConfiguration(string phccConfigFile)
        {
            var toReturn = ConfigurationManager.Load(phccConfigFile);
            return toReturn;
        }

        private static void RegisterForInputEvents(p.Device device,
           
            p.I2CDataReceivedEventHandler i2cDataReceivedEventHandler)
        {
            if (device == null) return;

          
            if (i2cDataReceivedEventHandler != null)
            {
                device.I2CDataReceived += i2cDataReceivedEventHandler;
            }
        }

        private void device_AnalogInputChanged(object sender, p.AnalogInputChangedEventArgs e)
        {
            if (_analogInputSignals == null || _analogInputSignals.Length <= e.Index) return;
            var signal = _analogInputSignals[e.Index];
            signal.State = e.NewValue;
        }

        private void device_DigitalInputChanged(object sender, p.DigitalInputChangedEventArgs e)
        {
            if (_digitalInputSignals == null || _digitalInputSignals.Length <= e.Index) return;
            var signal = _digitalInputSignals[e.Index];
            signal.State = e.NewValue;
        }

        private static void device_I2CDataReceived(object sender, p.I2CDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region Disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PhccHardwareSupportModule()
        {
            Dispose();
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        StopTalking(_device);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }
                    try
                    {
                        UnregisterForInputEvents(_device, _analogInputChangedEventHandler,
                            _digitalInputChangedEventHandler, _i2cDataReceivedEventHandler);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                    try
                    {
                        AbandonInputEventHandlers();
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                    try
                    {
                        Common.Util.DisposeObject(_device); //disconnect from the PHCC
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }
                    _device = null;
                }
            }
            _isDisposed = true;
        }

        private static void UnregisterForInputEvents(p.Device device,
            p.AnalogInputChangedEventHandler analogInputChangedEventHandler,
            p.DigitalInputChangedEventHandler digitalInputChangedEventHandler,
            p.I2CDataReceivedEventHandler i2cDataReceivedEventHandler)
        {
            if (device == null) return;
            if (analogInputChangedEventHandler != null)
            {
                try
                {
                    device.AnalogInputChanged -= analogInputChangedEventHandler;
                }
                catch (RemotingException)
                {
                }

            }
            if (digitalInputChangedEventHandler != null)
            {
                try
                {
                    device.DigitalInputChanged -= digitalInputChangedEventHandler;
                }
                catch (RemotingException)
                {
                }
            }
            if (i2cDataReceivedEventHandler != null)
            {
                try
                {
                    device.I2CDataReceived -= i2cDataReceivedEventHandler;
                }
                catch (RemotingException)
                {
                }
            }
        }

        #endregion

        #region Device


        private static p.Device CreateDevice(string comPort)
        {
            var device = new p.Device(comPort, false);
            return device;
        }



        private static void StartTalking(p.Device device)
        {
            try
            {
                device.StartTalking();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private static void StopTalking(p.Device device)
        {
            try
            {
                device.StopTalking();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

        #endregion

    }
}