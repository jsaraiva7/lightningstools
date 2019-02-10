using System;
using System.Collections.Generic;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaAnOut1 : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public byte GainAllChannels { get; set; }
        public override string ToString()
        {
            return "DoaAnOut1 - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}