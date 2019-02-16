using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Motherboard : PhccConfigElement
    {
        public Motherboard()
        {
            Peripherals = new List<Peripheral>();
        }

        public string ComPort { get; set; }
        public string Name { get; set; }
        public List<DigitalOutputConfig> InputConfig { get; set; } = new List<DigitalOutputConfig>();

        [XmlArray("Peripherals")]
        [XmlArrayItem("Peripheral")]
        public List<Peripheral> Peripherals { get; set; }

        public string FriendlyName { get; set; }
    }
}