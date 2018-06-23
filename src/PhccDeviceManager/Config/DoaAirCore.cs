using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phcc.DeviceManager.Config
{
    [Serializable]
    public class DoaAirCore : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();
    }
}