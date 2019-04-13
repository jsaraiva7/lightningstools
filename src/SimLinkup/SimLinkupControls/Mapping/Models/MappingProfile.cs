using System;
using System.Collections.Generic;

namespace SimLinkupControls.Mapping.Models
{
    /// <summary>
    /// Copy of original SimLinkup mapping classes, but with modded Signal classes. This was duplicated to avoid changing Simlinkup core in order to use the original Signal classes. 
    /// This modded classes are needed in order to generate the "correct" XML mapping code. 
    /// On future a better alternative can be looked at. 
    /// </summary>
    public class MappingProfile
    {
        private List<SignalMapping> _signalMappings = new List<SignalMapping>();

        public List<SignalMapping> SignalMappings
        {
            get => _signalMappings;
            set => _signalMappings = value;
        }

        public static MappingProfile Load(string filePath)
        {
            return Common.Serialization.Util.DeserializeFromXmlFile<MappingProfile>(filePath);
        }

        public void Save(string filePath)
        {
            Common.Serialization.Util.SerializeToXmlFile(this, filePath);
        }

    }

    [Serializable]
    public class SignalMapping
    {
        public  Signal Destination { get; set; }
        public Signal Source { get; set; }
    }
}