using System;
using System.Collections.Generic;
using System.Linq;
using Common.HardwareSupport.MotorControl;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Peripherals.Classes
{
    public class HSMDoaStepper : IPeripheral
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

        public HSMDoaStepper(Peripheral peripheral, Device device, string portName)
        {
            _peripheral = peripheral;
            _device = device;
            _portName = portName;
        }
        public void InitializeSignals()
        {
            List<AnalogSignal> analogSignalsToReturn = new List<AnalogSignal>();
            var typedPeripheral = _peripheral as PhccConfiguration.Config.DoaStepper;
            var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
            for (var i = 0; i < 4; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "Outputs",
                    CollectionName = "Motor Outputs - " + _peripheral.FriendlyName,
                    FriendlyName = $"Motor {i + 1}",
                    Id = $"DOA_STEPPER[{_portName}][{baseAddress}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_STEPPER @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_STEPPER @ {baseAddress}",
                    SubSourceAddress = baseAddress,

                    State = 0
                };
                thisSignal.SignalChanged += DOAStepperSignalChanged;
                thisSignal.CalibrationData = typedPeripheral.OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                if (thisSignal.CalibrationData != null && thisSignal.CalibrationData.Any())
                {
                    var max = thisSignal.CalibrationData.OrderByDescending(x => x.Output)
                        .FirstOrDefault();
                    if (max != null)
                    {
                        thisSignal.MaxValue = max.Output;
                    }
                }

                analogSignalsToReturn.Add(thisSignal);
            }

            _peripheralFloatStates[baseAddress] = new double[4];
            AnalogOutputs = analogSignalsToReturn.ToArray();
        }

        private void DOAStepperSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var motorNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var motorNum = motorNumZeroBase + 1;
            var newPosition = args.CurrentState;
            var oldPosition = _peripheralFloatStates[baseAddress][motorNumZeroBase];
            var numSteps = (int)Math.Abs(newPosition - oldPosition);
            var direction = MotorDirections.Clockwise;
            if (oldPosition < newPosition)
            {
                direction = MotorDirections.Counterclockwise;
            }
            try
            {
                device.DoaSendStepperMotor(baseAddressByte, (byte)motorNum, direction, (byte)numSteps,
                    MotorStepTypes.FullStep);
                _peripheralFloatStates[baseAddress][motorNumZeroBase] = newPosition;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }
    }
}