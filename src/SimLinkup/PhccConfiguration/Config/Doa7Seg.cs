using System;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa7Seg : Peripheral
    { 
        public Doa7SegConfiguration Configuration { get; set; } = new Doa7SegConfiguration();
        public override string ToString()
        {
            return "Doa7Seg - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}