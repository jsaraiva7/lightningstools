using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Xml.Serialization;
using Common.HardwareSupport.HardwareConfig;
using Common.MacroProgramming;
using log4net;
using Phcc;

namespace PhccHsm.HsmSupport.Inputs
{
    public class PhccAnalogInputs : ISlaveDevice, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccAnalogInputs));


        private AnalogInputChangedEventHandler _analogInputChangedEventHandler;


        private readonly Device _device;
        private readonly string _portName;

        public PhccAnalogInputs(Device device)
        {
            _device = device;
            if (_device != null) _portName = _device.PortName;
            InitializeSignals(device);
            _analogInputChangedEventHandler = device_AnalogInputChanged;
            if (_analogInputChangedEventHandler != null) device.AnalogInputChanged += _analogInputChangedEventHandler;
        }


        public void Dispose()
        {
            if (_device == null) return;
            if (_analogInputChangedEventHandler != null)
                try
                {
                    _device.AnalogInputChanged -= _analogInputChangedEventHandler;
                    _analogInputChangedEventHandler = null;
                }
                catch (RemotingException)
                {
                }
        }
        [XmlIgnore]
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<TextSignal> TextInputs { get; } = new List<TextSignal>();
        [XmlIgnore]
        public List<TextSignal> TextOutputs { get; } = new List<TextSignal>();

        public int Address { get; set; }
        public string FriendlyName { get; set; }

        public void InitializeSignals(object device)
        {
            var toReturn = new List<AnalogSignal>();
            for (var i = 0; i < 35; i++)
            {
                var thisSignal = new AnalogSignal
                {
                    Category = "PHCC Inputs",
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
    }
}