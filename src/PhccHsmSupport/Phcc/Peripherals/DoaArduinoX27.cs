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
    public class HsmDoaArduinoX27 : BasePeripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HsmDoaArduinoX27));

        private Device _device;

        private Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private string _portName;


        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        //  private Arduino _testDevice;

        public override void InitializeSignals(object device)
        {
            try
            {
                //   _testDevice = new Arduino("COM7", 57600, true);
                // _testDevice.Open();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                // throw;
            }


            _device = device as Device;
            if (_device != null) _portName = _device.PortName;
            var analogSignalsToReturn = new List<AnalogSignal>();
            var digitalSignalsToReturn = new List<DigitalSignal>();

            var baseAddress = "0x" + Address.ToString("X4");
            for (var i = 0; i < 4; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "PHCC Outputs",
                    CollectionName = "X27 Stepper Outputs - " + FriendlyName + " " + "@" + baseAddress,
                    FriendlyName = $"Motor {i + 1}",
                    Id = $"DOA_X27_STEPPER[{_portName}][{baseAddress}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_X27_STEPPER @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_SX27_TEPPER @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    MinValue = 0,
                    MaxValue = 315 * 12,

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

            var thisSignal2 = new DigitalSignal
            {
                Category = "PHCC Resets",
                CollectionName = "X27 Stepper Outputs - " + FriendlyName,
                FriendlyName = "X27 Stepper Reset ",
                Id = $"X27 Stepper Reset [{_portName}][{baseAddress}][{1}]",
                Index = 1,
                PublisherObject = this,
                Source = _device,
                SourceFriendlyName = $"PHCC Device on {_portName}",
                SourceAddress = _portName,
                SubSource = $"X27 Stepper @ {baseAddress}",
                SubSourceFriendlyName = $"X27 Stepper @ {baseAddress}",
                SubSourceAddress = baseAddress,
                State = false
            };
            thisSignal2.SignalChanged += SignalChanged;
            digitalSignalsToReturn.Add(thisSignal2);
            _peripheralFloatStates[baseAddress] = new double[4];
            AnalogOutputs.AddRange(analogSignalsToReturn);
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
            try
            {
                if (source.State)
                    device.DoaSendArduinoX27Reset(baseAddressByte);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private void DOAStepperSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            var motorNumZeroBase = source.Index.Value;
            if (device == null) return;

            try
            {
                if ((int) source.State != (int) source.CorrelatedState)
                {
                    if (source.State < 8001)
                        device.DoaSendArduinoX27(baseAddressByte, motorNumZeroBase, (short) source.State);
                    try
                    {
                        //   _testDevice.analogWrite(motorNumZeroBase, (int)source.State);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // throw;
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
    }
}