using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Xml.Serialization;
using Common.HardwareSupport.HardwareConfig;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using log4net;
using Phcc;

namespace PhccHsmSupport.Phcc.Inputs
{
    public class PhccDigitalImputs
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccDigitalImputs));

        private readonly Device _device;

        private DigitalInputChangedEventHandler _digitalInputChangedEventHandler;
        private readonly string _portName;

        public PhccDigitalImputs()
        {
        }


        public PhccDigitalImputs(Device device, List<DigitalConfig> inputConfig)
        {
            _device = device;
            if (device.PortName != null) _portName = device.PortName;
            InitializeSignals(device, inputConfig);


            _digitalInputChangedEventHandler = device_DigitalInputChanged;
            if (_digitalInputChangedEventHandler != null)
                device.DigitalInputChanged += _digitalInputChangedEventHandler;
        }

        [XmlIgnore] public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore] public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore] public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore] public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore] public List<TextSignal> TextInputs { get; } = new List<TextSignal>();
        [XmlIgnore] public List<TextSignal> TextOutputs { get; } = new List<TextSignal>();

        public int Address { get; set; }
        public string FriendlyName { get; set; }

        private List<DigitalConfig> _inputConfig { get; set; } = new List<DigitalConfig>();

        public void InitializeSignals(object device, List<DigitalConfig> inputConfig)
        {
            var toReturn = new List<DigitalSignal>();




            if (inputConfig != null && inputConfig.Any())
                _inputConfig = inputConfig;
            for (var i = 0; i < 1024; i++)
                if (_inputConfig.Any(x => x.PinNumber == i + 1))
                {
                    var thisSignal = new DigitalSignal
                    {
                        Category = "PHCC Inversed Inputs",
                        CollectionName = "Digital Inputs",
                        FriendlyName = $"Digital Input (inverted) {string.Format($"{i + 1:0}", i + 1)}",
                        Id = $"PhccDigitalInput[{_portName}][{i}]",
                        Index = i,
                        PublisherObject = this,
                        Source = _device,

                        SourceAddress = _portName,
                        SourceFriendlyName = $"PHCC Device on {_portName}",
                        State = false
                    };
                    toReturn.Add(thisSignal);
                }
                else
                {
                    var thisSignal = new DigitalSignal
                    {
                        Category = "PHCC Inputs",
                        CollectionName = "Digital Inputs",
                        FriendlyName = $"Digital Input {string.Format($"{i + 1:0}", i + 1)}",
                        Id = $"PhccDigitalInput[{_portName}][{i}]",
                        Index = i,
                        PublisherObject = this,
                        Source = _device,

                        SourceAddress = _portName,
                        SourceFriendlyName = $"PHCC Device on {_portName}",
                        State = false
                    };
                    toReturn.Add(thisSignal);
                }

            DigitalInputs.AddRange(toReturn);
        }


        private void device_DigitalInputChanged(object sender, DigitalInputChangedEventArgs e)
        {
            if (DigitalInputs == null || DigitalInputs.Count <= e.Index) return;
            var signal = DigitalInputs[e.Index];
            if (signal.FriendlyName.Contains("(inverted)"))
                signal.State = !e.NewValue;
            else
                signal.State = e.NewValue;
        }

        public void Dispose()
        {
            if (_digitalInputChangedEventHandler != null)
                try
                {
                    _device.DigitalInputChanged -= _digitalInputChangedEventHandler;
                    _digitalInputChangedEventHandler = null;
                }
                catch (RemotingException)
                {
                }
        }
    }
}