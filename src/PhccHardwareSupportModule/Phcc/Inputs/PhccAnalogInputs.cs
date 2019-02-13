using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Inputs
{
    public class PhccAnalogInputs : IPeripheral, IDisposable
    {
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();

        private AnalogInputChangedEventHandler _analogInputChangedEventHandler;


        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public PhccAnalogInputs(Device device)
        {
            _device = device;
            if (_device != null) _portName = _device.PortName;
            InitializeSignals(null, device);
            _analogInputChangedEventHandler = device_AnalogInputChanged;
            if (_analogInputChangedEventHandler != null)
            {
                device.AnalogInputChanged += _analogInputChangedEventHandler;
            }
        }

      

        public void InitializeSignals(object peripheral, object device)
        {
            List<AnalogSignal> digitalSignalsToReturn = new List<AnalogSignal>();
            var toReturn = new List<AnalogSignal>();
            for (var i = 0; i < 35; i++)
            {
                var thisSignal = new AnalogSignal
                {
                    Category = "Inputs",
                    CollectionName = "Analog Inputs",
                    FriendlyName = $"Analog Input {string.Format($"{i + 1:0}", i + 1)}",
                    Id = $"PhccAnalogInput[{_portName}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceAddress = _portName,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    State = 0,
                    MinValue = 0,
                    MaxValue = 1023
                };
                toReturn.Add(thisSignal);
            }
            AnalogInputs.AddRange(toReturn);


        }

        private void device_AnalogInputChanged(object sender, AnalogInputChangedEventArgs e)
        {
            if (AnalogInputs == null || AnalogInputs.Count <= e.Index) return;
            var signal = AnalogInputs[e.Index];
            signal.State = e.NewValue;
        }


        public void Dispose()
        {
            if (_device == null) return;
            if (_analogInputChangedEventHandler != null)
            {
                try
                {
                    _device.AnalogInputChanged -= _analogInputChangedEventHandler;
                    _analogInputChangedEventHandler = null;
                }
                catch (RemotingException)
                {
                }

            }
        }
    }
}