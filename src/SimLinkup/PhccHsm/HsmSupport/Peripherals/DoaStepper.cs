using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using log4net;
using Phcc;

namespace PhccHsm.HsmSupport.Peripherals
{
    [Serializable]
    public class HsmDoaStepper : BasePeripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HsmDoaStepper));

        private Device _device;

        private Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private string _portName;


        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public List<HomingSignalConfig> HomingSignals { get; set; } = new List<HomingSignalConfig>();


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
                    Id = $"DOA_STEPPER[{_portName}][{baseAddress}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_STEPPER @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_STEPPER @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    MinValue = 0,
                    MaxValue = int.MaxValue,
                    State = 0
                };
                thisSignal.SignalChanged += DOAStepperSignalChanged;
                thisSignal.CalibrationData = OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                if (thisSignal.CalibrationData != null && thisSignal.CalibrationData.Any())
                {
                    var max = thisSignal.CalibrationData.OrderByDescending(x => x.Output)
                        .FirstOrDefault();
                    if (max != null) thisSignal.MaxValue = max.Output;
                }

                analogSignalsToReturn.Add(thisSignal);
            }

            _peripheralFloatStates[baseAddress] = new double[4];
            AnalogOutputs.AddRange(analogSignalsToReturn);


            var t = Task.Run(async () => { HomeIn(); });
        }

        private async void HomeIn()
        {
            if (HomingSignals.Any())
                Parallel.ForEach(HomingSignals, home =>
                {
                    var homing = DigitalInputs.FirstOrDefault(c => c.Id == home.ControlSignalId);
                    var motor = AnalogOutputs.FirstOrDefault(c => c.Id == home.MotorSignalId);
                    if (homing != null && motor != null)
                    {
                        var baseAddress = motor.SubSourceAddress;
                        var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
                        var motorNumZeroBase = motor.Index.Value;
                        var device = motor.Source as Device;
                        var motorNum = motorNumZeroBase + 1;
                        while (homing.State != true)
                            if (device != null)
                            {
                                device.DoaSendStepperMotor(baseAddressByte, (byte) motorNum,
                                    MotorDirections.Counterclockwise, 1,
                                    MotorStepTypes.FullStep);
                                Thread.Sleep(10);
                            }
                    }
                });
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
            var numSteps = (int) Math.Abs(newPosition - oldPosition);
            var direction = MotorDirections.Counterclockwise;
            if (oldPosition < newPosition) direction = MotorDirections.Clockwise;

            try
            {
                if (numSteps >= 1)
                {
                    while (numSteps > 126)
                    {
                        device.DoaSendStepperMotor(baseAddressByte, (byte) motorNum, direction, 126,
                            MotorStepTypes.FullStep);
                        numSteps = numSteps - 126;
                    }

                    device.DoaSendStepperMotor(baseAddressByte, (byte) motorNum, direction, (byte) numSteps,
                        MotorStepTypes.FullStep);
                    _peripheralFloatStates[baseAddress][motorNumZeroBase] = newPosition;
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
    }
}