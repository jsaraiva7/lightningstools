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

        public static void SetupDevice(Motherboard motherboard, CalibratedAnalogSignal[] analogOutputs, DigitalSignal[] digitalInputs)
        {
            try
            {
                HomeInSteppers(motherboard, analogOutputs, digitalInputs);
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