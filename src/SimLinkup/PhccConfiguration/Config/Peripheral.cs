using System;
using System.Xml.Serialization;

namespace PhccConfiguration.Config
{
    [Serializable]
    [XmlInclude(typeof (Doa40Do))]
    [XmlInclude(typeof (Doa7SegBitMode))]
    [XmlInclude(typeof(Doa7SegDisplayMode))]
    [XmlInclude(typeof (Doa8Servo))]
    [XmlInclude(typeof (DoaAirCore))]
    [XmlInclude(typeof (DoaAnOut1))]
    [XmlInclude(typeof (DoaStepper))]
    public abstract class Peripheral : PhccConfigElement
    {
        public byte Address { get; set; }
    }
}