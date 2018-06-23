using System.Collections.Generic;
using System.Xml.Serialization;
using Common.MacroProgramming;

namespace PhccConfiguration.Config
{
    /// <summary>
    /// Class to store the calibration data for this signal. 
    /// SignalId is the Id of the signal as SimLinkup sees it, Calibration data is the list of calibration points. 
    /// </summary>
    public class OutputConfig
    {
        
        [XmlArray("CalibrationData")]
        [XmlArrayItem("CalibrationPoint")]
        public List<CalibrationPoint> CalibrationData { get; set; } = new List<CalibrationPoint>();
    
        public string SignalId { get; set; }
    }
}