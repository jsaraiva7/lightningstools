using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.MacroProgramming;
using log4net;
using PhccConfiguration.Config.ConfigClasses;
using p = Phcc;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaStepper : Peripheral
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(DoaStepper));
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public List<HomingSignalConfig> HomingSignals { get; set; } = new List<HomingSignalConfig>();

        #region Device specific methods
 

        

//        public override void PeripheralSetup(AnalogSignal[] analogOutputs, AnalogSignal[] analogInputs, DigitalSignal[] digitalInputs, DigitalSignal[] digitalOutputs)
//        {


//            var analogSteppers = analogOutputs.Where(x => x.Id.Contains("DOA_STEPPER"))
//                .Where(x => HomingSignals.Any(y => y.MotorSignalId.Equals(x.Id))).ToList();

//            Parallel.ForEach(analogSteppers, (stepper) =>
//            {
//                var homingSignal = HomingSignals.FirstOrDefault(x => x.MotorSignalId == stepper.Id);
//                var digitalSignal = digitalInputs.FirstOrDefault(x => homingSignal != null && x.Id == homingSignal.ControlSignalId);

//                while (digitalSignal != null && digitalSignal.State)
//                {
//                    var source = stepper as CalibratedAnalogSignal;
//                    if (source?.Index == null) return;
//                    var motorNumZeroBase = source.Index.Value;
//                    var baseAddress = source.SubSourceAddress;
//                    var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
//                    var device = source.Source as p.Device;
//                    if (device == null) return;
//                    var motorNum = motorNumZeroBase + 1;
//                    var direction = p.MotorDirections.Counterclockwise;

//                    try
//                    {
//                        device.DoaSendStepperMotor(baseAddressByte, (byte)motorNum, direction, 1,
//                            p.MotorStepTypes.FullStep);
//                        Thread.Sleep(20);
//                    }
//                    catch (Exception e)
//                    {

//                        _log.Error(e.Message, e);
//                        return;
//                    }
//#if DEBUG
//                    digitalSignal.State = !digitalSignal.State;
//#endif
//                }
//            });



//        }

        #endregion


    }
}