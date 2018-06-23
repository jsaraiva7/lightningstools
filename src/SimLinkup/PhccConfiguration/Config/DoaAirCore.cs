using System;
using System.Collections.Generic;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaAirCore : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
    }
}