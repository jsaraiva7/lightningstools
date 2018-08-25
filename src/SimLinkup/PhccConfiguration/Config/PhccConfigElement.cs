using System;
using System.Xml.Serialization;

namespace PhccConfiguration.Config
{
    [Serializable]
    [XmlInclude(typeof (Motherboard))]
    [XmlInclude(typeof (Peripheral))]
    public abstract class PhccConfigElement
    {
    }
}