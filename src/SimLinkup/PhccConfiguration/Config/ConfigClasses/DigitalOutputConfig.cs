using System.Windows.Media.Converters;

namespace PhccConfiguration.Config.ConfigClasses
{
    public class DigitalOutputConfig
    {
        public int PinNumber { get; set; }
        public bool Inverted { get; set; } = false;
    }
}