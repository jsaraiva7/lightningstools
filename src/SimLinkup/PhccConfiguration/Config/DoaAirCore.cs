using System;
using System.Collections.Generic;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaAirCore : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public override string ToString()
        {
            return "DoaAirCore - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}