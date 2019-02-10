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
        public AnalogSignal[] AnalogOutputs { get; set; }
        public AnalogSignal[] AnalogInputs { get; set; }
        public DigitalSignal[] DigitalOutputs { get; set; }
        public DigitalSignal[] DigitalInputs { get; set; }

        private AnalogInputChangedEventHandler _analogInputChangedEventHandler;


        Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public PhccAnalogInputs(Device device, string portName)
        {
            _device = device;
            _portName = portName;
            InitializeSignals();
            _analogInputChangedEventHandler = device_AnalogInputChanged;
            if (_analogInputChangedEventHandler != null)
            {
                device.AnalogInputChanged += _analogInputChangedEventHandler;
            }
        }

      

        public void InitializeSignals()
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
            AnalogInputs = toReturn.ToArray();


        }

        private void device_AnalogInputChanged(object sender, AnalogInputChangedEventArgs e)
        {
            if (AnalogInputs == null || AnalogInputs.Length <= e.Index) return;
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