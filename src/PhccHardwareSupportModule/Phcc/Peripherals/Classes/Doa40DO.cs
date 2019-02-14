using System;
using System.Collections.Generic;
using System.Linq;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Peripherals.Classes
{
    public class HSMDoa40DO : HSMPeripheral
    {

        Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public override void InitializeSignals(object peripheral, object device)
        {
            _peripheral = peripheral as Peripheral;
            _device = device as Device;
            if (_device != null) _portName = _device.PortName;

            List<DigitalSignal> digitalSignalsToReturn = new List<DigitalSignal>();

            var typedPeripheral = _peripheral as Doa40Do;
            var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
            for (var i = 0; i < 40; i++)
            {
                if (typedPeripheral.OutputConfig.Any(x => x.PinNumber == i + 1))
                {
                    var thisSignal = new DigitalSignal
                    {
                        Category = "Outputs",
                        CollectionName = "Digital Outputs - " + _peripheral.FriendlyName,
                        FriendlyName = $"Digital Output {string.Format($"{i + 1:0}", i + 1)} (Inverted)",
                        Id = $"DOA_40DO[{_portName}][{baseAddress}][{i}]",
                        Index = i,
                        PublisherObject = this,
                        Source = _device,
                        SourceFriendlyName = $"PHCC Device on {_portName}",
                        SourceAddress = _portName,
                        SubSource = $"DOA_40DO @ {baseAddress}",
                        SubSourceFriendlyName = $"DOA_40DO @ {baseAddress}",
                        SubSourceAddress = baseAddress,
                        State = false
                    };
                    thisSignal.SignalChanged += SignalChanged;
                    digitalSignalsToReturn.Add(thisSignal);

                }
                else
                {
                    var thisSignal = new DigitalSignal
                    {
                        Category = "Outputs",
                        CollectionName = "Digital Outputs - " + _peripheral.FriendlyName,
                        FriendlyName = $"Digital Output {string.Format($"{i + 1:0}", i + 1)}",
                        Id = $"DOA_40DO[{_portName}][{baseAddress}][{i}]",
                        Index = i,
                        PublisherObject = this,
                        Source = _device,
                        SourceFriendlyName = $"PHCC Device on {_portName}",
                        SourceAddress = _portName,
                        SubSource = $"DOA_40DO @ {baseAddress}",
                        SubSourceFriendlyName = $"DOA_40DO @ {baseAddress}",
                        SubSourceAddress = baseAddress,
                        State = false
                    };
                    thisSignal.SignalChanged += SignalChanged;
                    digitalSignalsToReturn.Add(thisSignal);
                }
               
            }

            _peripheralByteStates[baseAddress] = new byte[5];

            DigitalOutputs.AddRange(digitalSignalsToReturn); 
        }


        private void SignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            var source = sender as DigitalSignal;
            if (source?.Index == null) return;
            var outputNum = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var newBitVal = args.CurrentState;
            if (source.FriendlyName.Contains("(Inverted)"))
            {
                newBitVal = !args.CurrentState;
            }
            var peripheralState = _peripheralByteStates[baseAddress];
            var connectorNumZeroBase = outputNum / 8;
            var thisBitIndex = outputNum % 8;
            var connectorNum = connectorNumZeroBase + 3;
            var currentBitsThisConnector = peripheralState[connectorNumZeroBase];
            var newBits = Common.Util.SetBit(currentBitsThisConnector, thisBitIndex, newBitVal);
            try
            {
                device.DoaSend40DO(baseAddressByte, (byte) connectorNum, newBits);
                peripheralState[connectorNumZeroBase] = newBits;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

    }
}