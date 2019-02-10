using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.MacroProgramming;
using log4net;
using PhccConfiguration.Config.ConfigClasses;
using p = Phcc;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class DoaStepper : Peripheral
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(DoaStepper));
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
        public List<HomingSignalConfig> HomingSignals { get; set; } = new List<HomingSignalConfig>();


        public override string ToString()
        {
            return "DOA Stepper - " + FriendlyName + " - " + Address.ToString("X");

        }

    }
}