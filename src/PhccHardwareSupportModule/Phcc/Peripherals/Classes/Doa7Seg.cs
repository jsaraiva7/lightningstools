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
    public class HSMDoa7Seg : IPeripheral
    {
        public AnalogSignal[] AnalogOutputs { get; set; }
        public AnalogSignal[] AnalogInputs { get; set; }
        public DigitalSignal[] DigitalOutputs { get; set; }
        public DigitalSignal[] DigitalInputs { get; set; }




        Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();

        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public HSMDoa7Seg(Peripheral peripheral, Device device, string portName)
        {
            _peripheral = peripheral;
            _device = device;
            _portName = portName;
        }
        public void InitializeSignals()
        {

            List<AnalogSignal> analogSignalsToReturn = new List<AnalogSignal>();
            List<DigitalSignal> digitalSignalsToReturn = new List<DigitalSignal>();


            var typedPeripheral = _peripheral as PhccConfiguration.Config.Doa7Seg;
            var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");

            for (var i = 0; i < 32; i++)
            {
                if (typedPeripheral.Configuration.DisplayModeConfiguration.Any(x => x.FirstPin == i))
                {
                    var outputConfig =
                        typedPeripheral.Configuration.DisplayModeConfiguration.FirstOrDefault(x =>
                            x.FirstPin == i);
                    string s = "9";
                    for (int j = 1; j < outputConfig.NumDisplays; j++)
                    {
                        s = s + "9";
                    }
                    if (outputConfig != null)
                    {
                        var nDisp = (i + 1).ToString();
                        var j = outputConfig.NumDisplays;
                        var maxSize = int.Parse(s);
                        var thisSignal = new CalibratedAnalogSignal()
                        {
                            Category = "Outputs",
                            CollectionName = "7 Segment Display Outputs - " + _peripheral.FriendlyName,
                            FriendlyName =
                                $"Display Group " + nDisp + ",  Number of digits " + j,
                            Id = $"DOA_7SEG[{_portName}][{baseAddress}][{i}]",
                            Index = outputConfig.FirstPin,
                            PublisherObject = this,
                            Source = _device,
                            SourceFriendlyName = $"PHCC Device on {_portName}",
                            SourceAddress = _portName,
                            SubSource = $"DOA_7SEG Display @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_7SEG Display @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            MinValue = -1,
                            MaxValue = maxSize,
                            State = -1,
                        };


                        thisSignal.SignalChanged += DOA7SegDisplayOutputSignalChanged;
                        analogSignalsToReturn.Add(thisSignal);

                    }
                    i = i + outputConfig.NumDisplays;

                }
                //needs adjusting
                else
                {
                    for (var j = 0; j < 8; j++)
                    {
                        var thisSignal = new DigitalSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Digital Outputs",
                            FriendlyName =
                                $"Display Group  {string.Format($"{i + 1:0}", j + 1)}, Output Line {string.Format($"{j + 1:0}", j + 1)}",
                            Id = $"DOA_7SEG[{_portName}][{baseAddress}][{i}][{j}]",
                            Index = i * 8 + j,
                            PublisherObject = this,
                            Source = _device,
                            SourceFriendlyName = $"PHCC Device on {_portName}",
                            SourceAddress = _portName,
                            SubSource = $"DOA_7SEG @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_7SEG @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            State = false
                        };


                        thisSignal.SignalChanged += DOA7SegBitOutputSignalChanged;
                        digitalSignalsToReturn.Add(thisSignal);
                    }
                }

            }
            _peripheralByteStates[baseAddress] = new byte[32];
            AnalogOutputs = analogSignalsToReturn.ToArray();
            DigitalOutputs = digitalSignalsToReturn.ToArray();
        }

        private void DOA7SegBitOutputSignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            var source = sender as DigitalSignal;
            if (source?.Index == null) return;
            var outputLineNum = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var newBitVal = args.CurrentState;
            var peripheralState = _peripheralByteStates[baseAddress];
            var displayNumZeroBase = outputLineNum / 8;
            var thisBitIndex = outputLineNum % 8;
            var displayNum = displayNumZeroBase + 1;
            var currentBitsThisDisplay = peripheralState[displayNumZeroBase];
            var newBits = Common.Util.SetBit(currentBitsThisDisplay, thisBitIndex, newBitVal);

            try
            {
                device.DoaSend7Seg(baseAddressByte, (byte)displayNum, newBits);
                peripheralState[displayNumZeroBase] = newBits;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        //todo finish this implementation
        private void DOA7SegDisplayOutputSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as AnalogSignal;
            if (source?.Index == null) return;
            var outputLineNum = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var newBitVal = args.CurrentState;
            var peripheralState = _peripheralByteStates[baseAddress];
            var displayNumZeroBase = outputLineNum;
            var thisBitIndex = outputLineNum % 8;
            var displayNum = displayNumZeroBase + 1;
            var currentBitsThisDisplay = peripheralState[displayNumZeroBase];
            var tosend = newBitVal.ToString().ToCharArray();
            for (int i = outputLineNum; i < tosend.Length; i++)
            {
                var newBits = device.CharTo7Seg(tosend[i]);
                try
                {
                    int r = newBits;
                    device.DoaSend7Seg(baseAddressByte, (byte)displayNum, newBits);
                    peripheralState[i] = newBits;
                }
                catch (Exception e)
                {
                    _log.Error(e.Message, e);
                }
            }



        }
    }
}
