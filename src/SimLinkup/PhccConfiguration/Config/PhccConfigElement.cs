using System;
using System.Xml.Serialization;
using Common.MacroProgramming;

namespace PhccConfiguration.Config
{
    [Serializable]
    [XmlInclude(typeof (Motherboard))]
    [XmlInclude(typeof (Peripheral))]
    public abstract class PhccConfigElement
    {


    }
}