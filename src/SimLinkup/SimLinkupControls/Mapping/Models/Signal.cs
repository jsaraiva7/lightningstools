using System;
using System.Xml.Serialization;

namespace SimLinkupControls.Mapping.Models
{
    /// <summary>
    /// Mockup classes to generate correct XML. please refer to MappingProfile class on this project. 
    /// </summary>
    [Serializable]
    public class AnalogSignal : Signal
    {
        [XmlIgnore]
        public override string SignalType => "Analog / Numeric";
    }

    /// <summary>
    /// Mockup classes to generate correct XML. please refer to MappingProfile class on this project. 
    /// </summary>
    [Serializable]
    public class DigitalSignal : Signal
    {
        [XmlIgnore]
        public override string SignalType => "Digital / Boolean";

    }
    /// <summary>
    /// Mockup classes to generate correct XML. please refer to MappingProfile class on this project. 
    /// </summary>
    [Serializable]
    public class TextSignal : Signal
    {
        [XmlIgnore]
        public override string SignalType => "Text / Characters";

    }
    /// <summary>
    /// Mockup classes to generate correct XML. please refer to MappingProfile class on this project. 
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(AnalogSignal))]
    [XmlInclude(typeof(DigitalSignal))]
    [XmlInclude(typeof(TextSignal))]
    public abstract class Signal
    {
        public string Id { get; set; }
        public abstract string SignalType { get; }
    }
}