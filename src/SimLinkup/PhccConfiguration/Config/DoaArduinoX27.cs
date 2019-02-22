using System;
using System.Collections.Generic;
using log4net;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaArduinoX27 : Peripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DoaArduinoX27));
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public List<HomingSignalConfig> HomingSignals { get; set; } = new List<HomingSignalConfig>();
        public override string ToString()
        {
            return "DoaArduinoX27 - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}