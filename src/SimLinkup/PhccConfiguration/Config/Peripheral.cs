using System;
using System.Xml.Serialization;

namespace PhccConfiguration.Config
{
    public interface IPeripheral
    {
        byte Address { get; set; }
        string FriendlyName { get; set; }
    }

    [Serializable]
    [XmlInclude(typeof (Doa40Do))]
    [XmlInclude(typeof (Doa7Seg))]
    [XmlInclude(typeof (Doa8Servo))]
    [XmlInclude(typeof (DoaAirCore))]
    [XmlInclude(typeof (DoaAnOut1))]
    [XmlInclude(typeof (DoaStepper))]
    [XmlInclude(typeof(DoaArduinoX27))]
    public abstract class Peripheral : PhccConfigElement, IPeripheral
    {
        public byte Address { get; set; }
        //Henk Suggestion
        public string FriendlyName { get; set; }
       
    }

     
}