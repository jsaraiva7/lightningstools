using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PhccSupportModule.HsmSupport.Peripherals
{
    [Serializable]
    [XmlInclude(typeof(HsmDoa40Do))]
    [XmlInclude(typeof(HsmDoa7Seg))]
    [XmlInclude(typeof(HsmDoa8Servo))]
    [XmlInclude(typeof(HsmDoaAirCore))]
    [XmlInclude(typeof(HsmDoaAnOut1))]
    [XmlInclude(typeof(HsmDoaStepper))]
    [XmlInclude(typeof(HsmDoaArduinoX27))]
    public abstract class BasePeripheral : ISlaveDevice
    {
        [XmlIgnore]
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<TextSignal> TextInputs { get; } = new List<TextSignal>();
        [XmlIgnore]
        public List<TextSignal> TextOutputs { get; } = new List<TextSignal>();

        public int Address { get; set; }
        public string FriendlyName { get; set; } = "";

        public abstract void InitializeSignals(object SourceDevice);
    }
}