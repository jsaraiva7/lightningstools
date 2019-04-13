namespace Common.HardwareSupport.HardwareConfig.ConfigClasses
{
    public class DigitalConfig
    {
        public int PinNumber { get; set; }
        public bool Inverted { get; set; } = false;
    }
}