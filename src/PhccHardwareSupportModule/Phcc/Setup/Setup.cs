using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.MacroProgramming;
using log4net;
using PhccConfiguration.Config;
using PhccConfiguration.Config.ConfigClasses;
using p = Phcc;

namespace PhccHardwareSupportModule.Phcc.Setup
{
    public static class Setup
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));
        public static void SetupDevice(Motherboard motherboard, CalibratedAnalogSignal[] analogOutputs, DigitalSignal[] digitalInputs, p.Device device)
        {
            try
            {
                if (motherboard != null)
                {
                    SendCalibrations(device, motherboard);
                    HomeInSteppers(motherboard, analogOutputs, digitalInputs);
                }

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
                device.DoaSend8ServoCalibration(servoConfig.Address, (byte)calibration.ServoNum,
                    calibration.CalibrationOffset);
                device.DoaSend8ServoGain(servoConfig.Address, (byte)calibration.ServoNum, calibration.Gain);
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
        private static void HomeInSteppers(Motherboard motherboard, CalibratedAnalogSignal[] analogOutputs, DigitalSignal[] digitalInputs)
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

            var analogSteppers = analogOutputs.Where(x => x.Id.Contains("DOA_STEPPER"))
                .Where(x => HomingSignals.Any(y => y.MotorSignalId.Equals(x.Id))).ToList();

            Parallel.ForEach(analogSteppers, (stepper) =>
            {
                var homingSignal = HomingSignals.FirstOrDefault(x => x.MotorSignalId == stepper.Id);
                var digitalSignal = digitalInputs.FirstOrDefault(x => x.Id == homingSignal.ControlSignalId);

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
                        device.DoaSendStepperMotor(baseAddressByte, (byte)motorNum, direction, (byte)1,
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