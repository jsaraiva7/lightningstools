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
    public class HSMDoa8Servo : IPeripheral
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

        public HSMDoa8Servo(Peripheral peripheral, Device device, string portName)
        {
            _peripheral = peripheral;
            _device = device;
            _portName = portName;
            try
            {
                if (device == null) throw new ArgumentNullException(nameof(device));
                if (peripheral == null) throw new ArgumentNullException(nameof(peripheral));
                SendServoCalibrations(_device, _peripheral);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
            
        }
        public void InitializeSignals( )
        {

            List<AnalogSignal> analogSignalsToReturn = new List<AnalogSignal>();

            var typedPeripheral = _peripheral as PhccConfiguration.Config.Doa8Servo;
            var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
            for (var i = 0; i < 8; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "Outputs",
                    CollectionName = "Motor Outputs - " + _peripheral.FriendlyName,
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
                thisSignal.CalibrationData = typedPeripheral.OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                thisSignal.SignalChanged += DOA8ServoOutputSignalChanged;
                analogSignalsToReturn.Add(thisSignal);
            }
            _peripheralFloatStates[baseAddress] = new double[8];
            AnalogOutputs = analogSignalsToReturn.ToArray();
        }


        private static void SendServoCalibrations(Device device, Peripheral peripheral)
        {

            var servoConfig = peripheral as Doa8Servo;
            if (device == null) throw new ArgumentNullException(nameof(device));
            if (servoConfig == null) throw new ArgumentNullException(nameof(servoConfig));
            if (servoConfig.ServoCalibrations == null)
            {
                return;
            }
            foreach (var calibration in servoConfig.ServoCalibrations)
            {
                device.DoaSend8ServoCalibration(servoConfig.Address, (byte)calibration.ServoNum,
                    calibration.CalibrationOffset);
                device.DoaSend8ServoGain(servoConfig.Address, (byte)calibration.ServoNum, calibration.Gain);
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
            var newPosition = (byte)(Math.Abs(args.CurrentState + 90.00) / 180.00 * 255);

            try
            {
                device.DoaSend8ServoPosition(baseAddressByte, (byte)servoNum, newPosition);
                _peripheralFloatStates[baseAddress][servoNum] = newPosition;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }
    }
}