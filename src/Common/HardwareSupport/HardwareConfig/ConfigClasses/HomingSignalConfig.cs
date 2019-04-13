namespace Common.HardwareSupport.HardwareConfig.ConfigClasses
{
    public class HomingSignalConfig
    {
        /// <summary>
        /// The Motor ID 
        /// </summary>
        public string MotorSignalId { get; set; }
        /// <summary>
        /// The Input Signal ID
        /// </summary>
        public string ControlSignalId { get; set; }

        /// <summary>
        /// State when the motor comands should stop
        /// TODO: implement this on code. As is the motor stop on the false to true transition. The current GUI does not support it. 
        /// </summary>
        public bool State { get; set; } = true;

    }
}