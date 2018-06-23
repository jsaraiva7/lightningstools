using System;
using System.Collections.Generic;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaStepper : Peripheral
    {
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public List<HomingSignalConfig> HomingSignals { get; set; } = new List<HomingSignalConfig>();
    }
}