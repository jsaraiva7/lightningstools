using System;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa7Seg : Peripheral
    { 
        public Doa7SegConfiguration Configuration { get; set; } = new Doa7SegConfiguration();
    }
}