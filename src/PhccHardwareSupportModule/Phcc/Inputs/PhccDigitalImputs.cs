using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccConfiguration.Config.ConfigClasses;
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

        public PhccDigitalImputs(Motherboard motherboard, Device device)
        {
            _device = device;
            if (device.PortName != null) _portName = device.PortName;
            InitializeSignals(motherboard, device);
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

            List<DigitalOutputConfig> inputConfig = new List<DigitalOutputConfig>();

            var motherboard = peripheral as Motherboard;
            if (motherboard != null)
            {
                if (motherboard.InputConfig != null && motherboard.InputConfig.Any())
                {
                    inputConfig = motherboard.InputConfig;
                }
            }
            for (var i = 0; i < 1024; i++)
            {
                if (inputConfig.Any(x => x.PinNumber == i + 1))
                {
                    var thisSignal = new DigitalSignal
                    {
                        Category = "Inversed Inputs",
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
                
            }
            DigitalInputs.AddRange(toReturn);
        }


        private void device_DigitalInputChanged(object sender, DigitalInputChangedEventArgs e)
        {
            if (DigitalInputs == null || DigitalInputs.Count <= e.Index) return;
            var signal = DigitalInputs[e.Index];
            if (signal.FriendlyName.Contains("(inverted)"))
            {
                signal.State = !e.NewValue;
            }
            else
            {
                signal.State = e.NewValue;
            }
           
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