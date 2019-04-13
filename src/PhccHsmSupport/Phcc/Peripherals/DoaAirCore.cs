using System;
using System.Collections.Generic;
using System.Linq;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using log4net;
using Phcc;

namespace PhccHsmSupport.Phcc.Peripherals
{
    [Serializable]
    public class HsmDoaAirCore : BasePeripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HsmDoaAirCore));


        private Device _device;


        private Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private string _portName;


        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();

        public override void InitializeSignals(object device)
        {
            _device = device as Device;
            if (_device != null) _portName = _device.PortName;
            var analogSignalsToReturn = new List<AnalogSignal>();

            var baseAddress = "0x" + Address.ToString("X4");
            for (var i = 0; i < 4; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "PHCC Outputs",
                    CollectionName = "Motor Outputs - " + FriendlyName + " " + "@" + baseAddress,
                    FriendlyName = $"Motor {i + 1}",
                    Id = $"DOA_AIRCORE[{_portName}][{baseAddress}][{i}]",
                    Index = i,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_AIRCORE @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_AIRCORE @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    State = 0
                };
                thisSignal.SignalChanged += DOAAircoreOutputSignalChanged;
                thisSignal.CalibrationData = OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                thisSignal.MinValue = 0;
                thisSignal.MaxValue = 360;
                thisSignal.IsAngle = true;
                analogSignalsToReturn.Add(thisSignal);
            }

            _peripheralFloatStates[baseAddress] = new double[4];
            AnalogOutputs.AddRange(analogSignalsToReturn);
        }

        private void DOAAircoreOutputSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var motorNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var motorNum = motorNumZeroBase + 1;
            var newPosition = (int) (Math.Abs(args.CurrentState) / 360.00 * 1023);
            try
            {
                device.DoaSendAirCoreMotor(baseAddressByte, (byte) motorNum, newPosition);
                _peripheralFloatStates[baseAddress][motorNum] = newPosition;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
    }
}