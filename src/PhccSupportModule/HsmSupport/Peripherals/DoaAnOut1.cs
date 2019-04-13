using System;
using System.Collections.Generic;
using System.Linq;

namespace PhccSupportModule.HsmSupport.Peripherals
{
    [Serializable]
    public class HsmDoaAnOut1 : BasePeripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HsmDoaAnOut1));


        private Device _device;


        private Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private string _portName;
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public byte GainAllChannels { get; set; }

        public override void InitializeSignals(object device)
        {
            _device = device as Device;
            if (_device != null) _portName = _device.PortName;
            try
            {
                if (device == null) throw new ArgumentNullException(nameof(device));

                SendAnOut1Calibrations(_device);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

            var analogSignalsToReturn = new List<AnalogSignal>();

            var baseAddress = "0x" + Address.ToString("X4");
            for (var i = 0; i < 16; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "PHCC Outputs",
                    CollectionName = "Analog Outputs - " + FriendlyName + " " + "@" + baseAddress,
                    FriendlyName = $"Analog Output {string.Format($"{i + 1:0}", i + 1)}",
                    Id = $"DOA_ANOUT1[{baseAddress}][{_portName}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_ANOUT1 @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_ANOUT1 @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    MinValue = 0,
                    MaxValue = 254,
                    IsVoltage = true,
                    State = 0
                };
                thisSignal.SignalChanged += DOAAnOut1SignalChanged;
                thisSignal.CalibrationData = OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                analogSignalsToReturn.Add(thisSignal);
            }

            _peripheralFloatStates[baseAddress] = new double[16];
            AnalogOutputs.AddRange(analogSignalsToReturn);
        }


        private void SendAnOut1Calibrations(Device device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            try
            {
                device.DoaSendAnOut1GainAllChannels((byte) Address, GainAllChannels);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private void DOAAnOut1SignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var channelNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var channelNum = channelNumZeroBase + 1;
            var newVal = (byte) (Math.Abs(args.CurrentState) / 5.00 * 255);
            try
            {
                device.DoaSendAnOut1(baseAddressByte, (byte) channelNum, newVal);
                _peripheralFloatStates[baseAddress][channelNum] = newVal;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
    }
}