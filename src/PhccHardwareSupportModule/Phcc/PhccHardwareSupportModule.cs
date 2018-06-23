using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using Common.HardwareSupport;
using Common.HardwareSupport.MotorControl;
using Common.MacroProgramming;
using log4net;
using PhccConfiguration.Config;
using p = Phcc;

namespace PhccHardwareSupportModule.Phcc
{
    public class PhccHardwareSupportModule : HardwareSupportModuleBase, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));

        private readonly AnalogSignal[] _analogInputSignals;
        private readonly CalibratedAnalogSignal[] _analogOutputSignals;
        private readonly DigitalSignal[] _digitalInputSignals;
        private readonly DigitalSignal[] _digitalOutputSignals;
        private readonly Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        private readonly Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private p.AnalogInputChangedEventHandler _analogInputChangedEventHandler;
        private p.Device _device;
        private p.DigitalInputChangedEventHandler _digitalInputChangedEventHandler;
        private p.I2CDataReceivedEventHandler _i2cDataReceivedEventHandler;
        private bool _isDisposed;

        private PhccHardwareSupportModule()
        {
        }


        /// <summary>
        /// static method added to allow module initialization from other contexts. Returns an HSM object, that can be used to calibrate the outputs. (by actually moving the needles!)
        /// the returned HSM ignores any previous calibrations!
        /// </summary>
        /// <param name="motherboard"></param>
        /// <returns></returns>
        public static PhccHardwareSupportModule GetModuleForCalibration(Motherboard motherboard)
        {
            var hsm = new PhccHardwareSupportModule(motherboard);
            foreach (var calOutput in hsm._analogOutputSignals)
            {
                calOutput.CalibrationData = null;
            }
            return hsm;
        }

        
        private PhccHardwareSupportModule(Motherboard motherboard) : this()
        {
            if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
            _device = CreateDevice(motherboard.ComPort);
            _analogInputSignals = CreateAnalogInputSignals(_device, motherboard.ComPort);
            _digitalInputSignals = CreateDigitalInputSignals(_device, motherboard.ComPort);
            CreateOutputSignals(_device, motherboard, out _digitalOutputSignals, out _analogOutputSignals,
                out _peripheralByteStates, out _peripheralFloatStates);

            CreateInputEventHandlers();
            RegisterForInputEvents(_device, _analogInputChangedEventHandler, _digitalInputChangedEventHandler,
                _i2cDataReceivedEventHandler);
            try { SendCalibrations(_device, motherboard); }
            catch (Exception e) { _log.Error(e.Message, e); }

            try { StartTalking(_device); }
            catch (Exception e) { _log.Error(e.Message, e); }
            try
            {
                HomeInSteppers(motherboard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
            }
        }

        public override AnalogSignal[] AnalogInputs => _analogInputSignals;

        public override AnalogSignal[] AnalogOutputs => _analogOutputSignals;

        public override DigitalSignal[] DigitalInputs => _digitalInputSignals;

        public override DigitalSignal[] DigitalOutputs => _digitalOutputSignals;

        public override string FriendlyName => "PHCC";

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PhccHardwareSupportModule()
        {
            Dispose();
        }

        public static IHardwareSupportModule[] GetInstances()
        {
            var toReturn = new List<IHardwareSupportModule>();
            try
            {
                var hsmConfigFilePath = Path.Combine(Util.CurrentMappingProfileDirectory,
                    "PhccHardwareSupportModule.config");
                var hsmConfig = PhccHardwareSupportModuleConfig.Load(hsmConfigFilePath);
                var phccDeviceManagerConfigFilePath = hsmConfig.PhccDeviceManagerConfigFilePath;
                var phccConfigManager = LoadConfiguration(phccDeviceManagerConfigFilePath) ?? LoadConfiguration(Path.Combine(Util.CurrentMappingProfileDirectory, phccDeviceManagerConfigFilePath));

                foreach (var m in phccConfigManager.Motherboards)
                {
                    IHardwareSupportModule thisHsm = new PhccHardwareSupportModule(m);
                    toReturn.Add(thisHsm);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
            return toReturn.ToArray();
        }

        private void AbandonInputEventHandlers()
        {
            _analogInputChangedEventHandler = null;
            _digitalInputChangedEventHandler = null;
            _i2cDataReceivedEventHandler = null;
        }

        private AnalogSignal[] CreateAnalogInputSignals(p.Device device, string portName)
        {
            var toReturn = new List<AnalogSignal>();
            for (var i = 0; i < 35; i++)
            {
                var thisSignal = new AnalogSignal
                {
                    Category = "Inputs",
                    CollectionName = "Analog Inputs",
                    FriendlyName = $"Analog Input {string.Format($"{i + 1:0}", i + 1)}",
                    Id = $"PhccAnalogInput[{portName}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = device,
                    SourceAddress = portName,
                    SourceFriendlyName = $"PHCC Device on {portName}",
                    State = 0,
                    MinValue = 0,
                    MaxValue = 1023
                };
                toReturn.Add(thisSignal);
            }
            return toReturn.ToArray();
        }

        private static p.Device CreateDevice(string comPort)
        {
            var device = new p.Device(comPort, false);
            return device;
        }

        private DigitalSignal[] CreateDigitalInputSignals(p.Device device, string portName)
        {
            var toReturn = new List<DigitalSignal>();
            for (var i = 0; i < 1024; i++)
            {
                var thisSignal = new DigitalSignal
                {
                    Category = "Inputs",
                    CollectionName = "Digital Inputs",
                    FriendlyName = $"Digital Input {string.Format($"{i + 1:0}", i + 1)}",
                    Id = $"PhccDigitalInput[{portName}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = device,
                    SourceAddress = portName,
                    SourceFriendlyName = $"PHCC Device on {portName}",
                    State = false
                };
                toReturn.Add(thisSignal);
            }
            return toReturn.ToArray();
        }

        private void CreateInputEventHandlers()
        {
            _analogInputChangedEventHandler = device_AnalogInputChanged;
            _digitalInputChangedEventHandler = device_DigitalInputChanged;
            _i2cDataReceivedEventHandler = device_I2CDataReceived;
        }

        private void CreateOutputSignals(p.Device device, Motherboard motherboard, out DigitalSignal[] digitalSignals,
            out CalibratedAnalogSignal[] analogSignals,
            out Dictionary<string, byte[]> peripheralByteStates,
            out Dictionary<string, double[]> peripheralFloatStates)
        {
            if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
            var portName = motherboard.ComPort;
            var digitalSignalsToReturn = new List<DigitalSignal>();
            var analogSignalsToReturn = new List<CalibratedAnalogSignal>();
            var attachedPeripherals = motherboard.Peripherals;
            peripheralByteStates = new Dictionary<string, byte[]>();
            peripheralFloatStates = new Dictionary<string, double[]>();
            foreach (var peripheral in attachedPeripherals)
                if (peripheral is Doa40Do)
                {
                    var typedPeripheral = peripheral as Doa40Do;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 40; i++)
                    {
                        var thisSignal = new DigitalSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Digital Outputs",
                            FriendlyName = $"Digital Output {string.Format($"{i + 1:0}", i + 1)}",
                            Id = $"DOA_40DO[{portName}][{baseAddress}][{i}]",
                            Index = i,
                            PublisherObject = this,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
                            SubSource = $"DOA_40DO @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_40DO @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            State = false
                        };
                        thisSignal.SignalChanged += DOA40DOOutputSignalChanged;
                        digitalSignalsToReturn.Add(thisSignal);
                    }
                    peripheralByteStates[baseAddress] = new byte[5];
                }
                else if (peripheral is Doa7Seg)
                {
                    var typedPeripheral = peripheral as Doa7Seg;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 32; i++)
                    for (var j = 0; j < 8; j++)
                    {
                        var thisSignal = new DigitalSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Digital Outputs",
                            FriendlyName =
                                $"Display {string.Format($"{j + 1:0}", j + 1)}, Output Line {string.Format($"{i + 1:0}", i + 1)}",
                            Id = $"DOA_7SEG[{portName}][{baseAddress}][{j}][{i}]",
                            Index = i * 8 + j,
                            PublisherObject = this,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
                            SubSource = $"DOA_7SEG @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_7SEG @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            State = false
                        };
                        thisSignal.SignalChanged += DOA7SegOutputSignalChanged;
                        digitalSignalsToReturn.Add(thisSignal);
                    }
                    peripheralByteStates[baseAddress] = new byte[32];
                }
                else if (peripheral is Doa8Servo)
                {
                    var typedPeripheral = peripheral as Doa8Servo;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 8; i++)
                    {
                        var thisSignal = new CalibratedAnalogSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Motor Outputs",
                            FriendlyName = $"Motor {i + 1}",
                            Id = $"DOA_8SERVO[{portName}][{baseAddress}][{i}]",
                            Index = i,
                            PublisherObject = this,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
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
                    peripheralFloatStates[baseAddress] = new double[8];
                }
                else if (peripheral is DoaAirCore)
                {
                    var typedPeripheral = peripheral as DoaAirCore;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 4; i++)
                    {
                        var thisSignal = new CalibratedAnalogSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Motor Outputs",
                            FriendlyName = $"Motor {i + 1}",
                            Id = $"DOA_AIRCORE[{portName}][{baseAddress}][{i}]",
                            Index = i,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
                            SubSource = $"DOA_AIRCORE @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_AIRCORE @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            State = 0
                        };
                        thisSignal.SignalChanged += DOAAircoreOutputSignalChanged;
                        thisSignal.CalibrationData = typedPeripheral.OutputConfigs
                            .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                        thisSignal.MinValue = 0;
                        thisSignal.MaxValue = 360;
                        thisSignal.IsAngle = true;
                        analogSignalsToReturn.Add(thisSignal);
                    }
                    peripheralFloatStates[baseAddress] = new double[4];
                }
                else if (peripheral is DoaAnOut1)
                {
                    var typedPeripheral = peripheral as DoaAnOut1;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 16; i++)
                    {
                        var thisSignal = new CalibratedAnalogSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Analog Outputs",
                            FriendlyName = $"Analog Output {string.Format($"{i + 1:0}", i + 1)}",
                            Id = $"DOA_ANOUT1[{baseAddress}][{portName}][{i}]",
                            Index = i,
                            PublisherObject = this,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
                            SubSource = $"DOA_ANOUT1 @ {baseAddress}",
                            SubSourceFriendlyName = $"DOA_ANOUT1 @ {baseAddress}",
                            SubSourceAddress = baseAddress,
                            MinValue = 0,
                            MaxValue = 5,
                            IsVoltage = true,
                            State = 0
                        };
                        thisSignal.SignalChanged += DOAAnOut1SignalChanged;
                        thisSignal.CalibrationData = typedPeripheral.OutputConfigs
                            .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                        analogSignalsToReturn.Add(thisSignal);
                    }
                    peripheralFloatStates[baseAddress] = new double[16];
                }
                else if (peripheral is DoaStepper)
                {
                    var typedPeripheral = peripheral as DoaStepper;
                    var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
                    for (var i = 0; i < 4; i++)
                    {
                        var thisSignal = new CalibratedAnalogSignal
                        {
                            Category = "Outputs",
                            CollectionName = "Motor Outputs",
                            FriendlyName = $"Motor {i + 1}",
                            Id = $"DOA_STEPPER[{portName}][{baseAddress}][{i}]",
                            Index = i,
                            PublisherObject = this,
                            Source = device,
                            SourceFriendlyName = $"PHCC Device on {portName}",
                            SourceAddress = portName,
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
                    peripheralFloatStates[baseAddress] = new double[4];
                }
            analogSignals = analogSignalsToReturn.ToArray();
            digitalSignals = digitalSignalsToReturn.ToArray();
        }

        private void device_AnalogInputChanged(object sender, p.AnalogInputChangedEventArgs e)
        {
            if (_analogInputSignals == null || _analogInputSignals.Length <= e.Index) return;
            var signal = _analogInputSignals[e.Index];
            signal.State = e.NewValue;
        }

        private void device_DigitalInputChanged(object sender, p.DigitalInputChangedEventArgs e)
        {
            if (_digitalInputSignals == null || _digitalInputSignals.Length <= e.Index) return;
            var signal = _digitalInputSignals[e.Index];
            signal.State = e.NewValue;
        }

        private static void device_I2CDataReceived(object sender, p.I2CDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        StopTalking(_device);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }
                    try
                    {
                        UnregisterForInputEvents(_device, _analogInputChangedEventHandler, _digitalInputChangedEventHandler, _i2cDataReceivedEventHandler);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                    try
                    {
                        AbandonInputEventHandlers();
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                    try
                    {
                        Common.Util.DisposeObject(_device); //disconnect from the PHCC
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }
                    _device = null;
                }
            }
            _isDisposed = true;
        }

        private void DOA40DOOutputSignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            var source = sender as DigitalSignal;
            if (source?.Index == null) return;
            var outputNum = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = byte.Parse(baseAddress);
            var device = source.Source as p.Device;
            if (device == null) return;
            var newBitVal = args.CurrentState;
            var peripheralState = _peripheralByteStates[baseAddress];
            var connectorNumZeroBase = outputNum / 8;
            var thisBitIndex = outputNum % 8;
            var connectorNum = connectorNumZeroBase + 3;
            var currentBitsThisConnector = peripheralState[connectorNumZeroBase];
            var newBits = Common.Util.SetBit(currentBitsThisConnector, thisBitIndex, newBitVal);
            try
            {
                device.DoaSend40DO(baseAddressByte, (byte)connectorNum, newBits);
                peripheralState[connectorNumZeroBase] = newBits;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

}

private void DOA7SegOutputSignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            var source = sender as DigitalSignal;
            if (source?.Index == null) return;
            var outputLineNum = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = byte.Parse(baseAddress);
            var device = source.Source as p.Device;
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

        private void DOA8ServoOutputSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var servoNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as p.Device;
            if (device == null) return;
            var servoNum = servoNumZeroBase + 1;
            var newPosition = (byte) (Math.Abs(args.CurrentState + 90.00) / 180.00 * 255);

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

        private void DOAAircoreOutputSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var motorNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as p.Device;
            if (device == null) return;
            var motorNum = motorNumZeroBase + 1;
            var newPosition = (int) (Math.Abs(args.CurrentState) / 360.00 * 1023);
            try
            {
                device.DoaSendAirCoreMotor(baseAddressByte, (byte)motorNum, newPosition);
                _peripheralFloatStates[baseAddress][motorNum] = newPosition;
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
            var device = source.Source as p.Device;
            if (device == null) return;
            var channelNum = channelNumZeroBase + 1;
            var newVal = (byte) (Math.Abs(args.CurrentState) / 5.00 * 255);
            try
            {
                device.DoaSendAnOut1(baseAddressByte, (byte)channelNum, newVal);
                _peripheralFloatStates[baseAddress][channelNum] = newVal;
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
            var motorNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as p.Device;
            if (device == null) return;
            var motorNum = motorNumZeroBase + 1;
            var newPosition = args.CurrentState;
            var oldPosition = _peripheralFloatStates[baseAddress][motorNumZeroBase];
            var numSteps = (int) Math.Abs(newPosition - oldPosition);
            var direction = p.MotorDirections.Clockwise;
            if (oldPosition < newPosition)
            {
                direction = p.MotorDirections.Counterclockwise;
            }
            try
            {
                device.DoaSendStepperMotor(baseAddressByte, (byte)motorNum, direction, (byte)numSteps,
                    p.MotorStepTypes.FullStep);
                _peripheralFloatStates[baseAddress][motorNumZeroBase] = newPosition;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

        private static ConfigurationManager LoadConfiguration(string phccConfigFile)
        {
            var toReturn = ConfigurationManager.Load(phccConfigFile);
            return toReturn;
        }

        private static void RegisterForInputEvents(p.Device device,
            p.AnalogInputChangedEventHandler analogInputChangedEventHandler,
            p.DigitalInputChangedEventHandler digitalInputChangedEventHandler,
            p.I2CDataReceivedEventHandler i2cDataReceivedEventHandler)
        {
            if (device == null) return;

            if (analogInputChangedEventHandler != null)
            {
                device.AnalogInputChanged += analogInputChangedEventHandler;
            }
            if (digitalInputChangedEventHandler != null)
            {
                device.DigitalInputChanged += digitalInputChangedEventHandler;
            }
            if (i2cDataReceivedEventHandler != null)
            {
                device.I2CDataReceived += i2cDataReceivedEventHandler;
            }
        }

        private static void SendAnOut1Calibrations(p.Device device, DoaAnOut1 anOut1Config)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            if (anOut1Config == null) throw new ArgumentNullException(nameof(anOut1Config));

            try
            {
                device.DoaSendAnOut1GainAllChannels(anOut1Config.Address, anOut1Config.GainAllChannels);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

        private static void SendCalibrations(p.Device device, Motherboard motherboard)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
            foreach (var peripheral in motherboard.Peripherals)
                if (peripheral is Doa8Servo)
                {
                    var servoConfig = peripheral as Doa8Servo;
                    try
                    {
                        SendServoCalibrations(device, servoConfig);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                }
                else if (peripheral is DoaAnOut1)
                {
                    var anOut1Config = peripheral as DoaAnOut1;
                    try
                    {
                        SendAnOut1Calibrations(device, anOut1Config);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }

                }
        }

        private static void SendServoCalibrations(p.Device device, Doa8Servo servoConfig)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            if (servoConfig == null) throw new ArgumentNullException(nameof(servoConfig));
            if (servoConfig.ServoCalibrations == null)
            {
                return;
            }
            foreach (var calibration in servoConfig.ServoCalibrations)
            {
                device.DoaSend8ServoCalibration(servoConfig.Address, (byte) calibration.ServoNum,
                    calibration.CalibrationOffset);
                device.DoaSend8ServoGain(servoConfig.Address, (byte) calibration.ServoNum, calibration.Gain);
            }
        }

        private static void StartTalking(p.Device device)
        {
            try
            {
                device.StartTalking();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

        private static void StopTalking(p.Device device)
        {
            try
            {
                device.StopTalking();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

        private static void UnregisterForInputEvents(p.Device device,
            p.AnalogInputChangedEventHandler analogInputChangedEventHandler,
            p.DigitalInputChangedEventHandler digitalInputChangedEventHandler,
            p.I2CDataReceivedEventHandler i2cDataReceivedEventHandler)
        {
            if (device == null) return;
            if (analogInputChangedEventHandler != null)
            {
                try
                {
                    device.AnalogInputChanged -= analogInputChangedEventHandler;
                }
                catch (RemotingException)
                {
                }

            }
            if (digitalInputChangedEventHandler != null)
            {
                try
                {
                    device.DigitalInputChanged -= digitalInputChangedEventHandler;
                }
                catch (RemotingException)
                {
                }
            }
            if (i2cDataReceivedEventHandler != null)
            {
                try
                {
                    device.I2CDataReceived -= i2cDataReceivedEventHandler;
                }
                catch (RemotingException)
                {
                }
            }
        }

        private void HomeInSteppers(Motherboard motherboard)
        {
            List<HomingSignalConfig> HomingSignals = new List<HomingSignalConfig>();
            foreach (var board in motherboard.Peripherals)
            {
                var b = board as DoaStepper;
                if (b?.HomingSignals.Count > 0)
                {
                    HomingSignals.AddRange(b.HomingSignals);
                }
            }

            var analogSteppers = AnalogOutputs.Where(x => x.Id.Contains("DOA_STEPPER"))
                .Where(x => HomingSignals.Any(y => y.MotorSignalId.Equals(x.Id))).ToList();

            Parallel.ForEach(analogSteppers, (stepper) =>
            {
                var homingSignal = HomingSignals.FirstOrDefault(x => x.MotorSignalId == stepper.Id);
                var digitalSignal = DigitalInputs.FirstOrDefault(x => x.Id == homingSignal.ControlSignalId);

                while (!digitalSignal.State)
                {
                    var source = stepper as CalibratedAnalogSignal;
                    if (source?.Index == null) return;
                    var motorNumZeroBase = source.Index.Value;
                    var baseAddress = source.SubSourceAddress;
                    var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
                    var device = source.Source as p.Device;
                    if (device == null) return;
                    var motorNum = motorNumZeroBase + 1;
                    var direction = p.MotorDirections.Counterclockwise;

                    try
                    {
                        device.DoaSendStepperMotor(baseAddressByte, (byte) motorNum, direction, (byte) 1,
                            p.MotorStepTypes.FullStep);
                        Thread.Sleep(20);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e.Message, e);
                    }
#if DEBUG
                    digitalSignal.State = !digitalSignal.State;
#endif
                }
            });
           

        }

    }
}