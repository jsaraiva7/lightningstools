using System.Collections.Generic;
using System.Runtime.Remoting;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Inputs
{
    public class PhccDigitalImputs : IPeripheral
    {
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();

        private DigitalInputChangedEventHandler _digitalInputChangedEventHandler;



        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public PhccDigitalImputs(Device device)
        {
            _device = device;
            if (device.PortName != null) _portName = device.PortName;
            InitializeSignals(null, device);
            if (device == null) return;
         
            _digitalInputChangedEventHandler = device_DigitalInputChanged;
            if (_digitalInputChangedEventHandler != null)
            {
                device.DigitalInputChanged += _digitalInputChangedEventHandler;
            }

        }

      
        public void InitializeSignals(object peripheral, object device)
        {
            var toReturn = new List<DigitalSignal>();
            for (var i = 0; i < 1024; i++)
            {
                var thisSignal = new DigitalSignal
                {
                    Category = "Inputs",
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
            signal.State = e.NewValue;
        }

        public void Dispose()
        {
            if (_digitalInputChangedEventHandler != null)
            {
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

}