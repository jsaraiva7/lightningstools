using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using log4net;
using Phcc;

namespace PhccHsmSupport.Phcc.Peripherals
{
    [Serializable]
    public class HsmDoa8Servo : BasePeripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HsmDoa8Servo));


        private Device _device;


        private Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private string _portName;

        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();

        [XmlArray("Calibrations")]
        [XmlArrayItem("Calibration")]
        public List<ServoCalibration> ServoCalibrations { get; set; }


        public override void InitializeSignals(object device)
        {
            _device = device as Device;
            if (_device != null) _portName = _device.PortName;
            try
            {
                if (device == null) throw new ArgumentNullException(nameof(device));

                SendServoCalibrations(_device);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

            var analogSignalsToReturn = new List<AnalogSignal>();


            var baseAddress = "0x" + Address.ToString("X4");
            for (var i = 0; i < 8; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "PHCC Outputs",
                    CollectionName = "Motor Outputs - " + FriendlyName + " " + "@" + baseAddress,
                    FriendlyName = $"Motor {i + 1}",
                    Id = $"DOA_8SERVO[{_portName}][{baseAddress}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_8SERVO @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_8SERVO @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    State = 0,
                    IsAngle = true,
                    MinValue = -90,
                    MaxValue = 90
                };
                thisSignal.CalibrationData = OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                thisSignal.SignalChanged += DOA8ServoOutputSignalChanged;
                analogSignalsToReturn.Add(thisSignal);
            }

            _peripheralFloatStates[baseAddress] = new double[8];
            AnalogOutputs.AddRange(analogSignalsToReturn);
        }


        private void SendServoCalibrations(Device device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            if (ServoCalibrations == null) return;
            foreach (var calibration in ServoCalibrations)
            {
                device.DoaSend8ServoCalibration((byte) Address, (byte) calibration.ServoNum,
                    calibration.CalibrationOffset);
                device.DoaSend8ServoGain((byte) Address, (byte) calibration.ServoNum, calibration.Gain);
            }
        }


        private void DOA8ServoOutputSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var servoNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var servoNum = servoNumZeroBase + 1;
            var newPosition = (byte) (Math.Abs(args.CurrentState + 90.00) / 180.00 * 255);

            try
            {
                device.DoaSend8ServoPosition(baseAddressByte, (byte) servoNum, newPosition);
                _peripheralFloatStates[baseAddress][servoNum] = newPosition;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
    }

    [Serializable]
    public class ServoCalibration
    {
        public short ServoNum { get; set; }
        public byte Gain { get; set; }
        public short CalibrationOffset { get; set; }
    }
}