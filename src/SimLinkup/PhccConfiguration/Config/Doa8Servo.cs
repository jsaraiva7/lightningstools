using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa8Servo : Peripheral
    {
       
        public List<OutputConfig> OutputConfigs { get; set; } = new List<OutputConfig>();

        [XmlArray("Calibrations")]
        [XmlArrayItem("Calibration")]
        public List<ServoCalibration> ServoCalibrations { get; set; }
    }
}