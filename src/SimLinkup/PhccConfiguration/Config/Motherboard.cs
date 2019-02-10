using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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

        [XmlArray("Peripherals")]
        [XmlArrayItem("Peripheral")]
        public List<Peripheral> Peripherals { get; set; }

        public string FriendlyName { get; set; }
    }
}