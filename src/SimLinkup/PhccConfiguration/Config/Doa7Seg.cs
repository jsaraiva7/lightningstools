using System;
using log4net;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa7Seg : Peripheral
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Doa7Seg));
        public Doa7SegConfiguration Configuration { get; set; } = new Doa7SegConfiguration();

        public override string ToString()
        {
            return "Doa7Seg - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}