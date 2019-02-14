using System;
using System.Collections.Generic;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa40Do : Peripheral
    {
        public List<DigitalOutputConfig> OutputConfig { get; set; } = new List<DigitalOutputConfig>();
        public override string ToString()
        {
            return "Doa40Do - " + FriendlyName + " - " + Address.ToString("X");

        }
    }


}