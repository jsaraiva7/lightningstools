﻿using System;
using System.Collections.Generic;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaAnOut1 : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public byte GainAllChannels { get; set; }
    }
}