using System.Collections.Generic;

namespace SimLinkupControls.Mapping.Models
{
    /// <summary>
    /// Display Model for signal mappings List. 
    /// </summary>
    public class MappingModel
    {
        public string FriendlySourceType { get; set; }
        public string FriendlySourceName { get; set; }
        public string FriendlyDestinationType { get; set; }
        public string FriendlyDestinationName { get; set; }

        public static List<MappingModel> GetModel(MappingProfile profile)
        {
            List<MappingModel> toReturn = new List<MappingModel>();

            foreach (var map in profile.SignalMappings)
            {
                if(map != null)
                if (map.Source != null && map.Destination != null)
                {
                    toReturn.Add(new MappingModel()
                    {
                        FriendlySourceType = map.Source.SignalType,
                        FriendlySourceName = map.Source.Id,
                        FriendlyDestinationName = map.Destination.Id,
                        FriendlyDestinationType = map.Destination.SignalType
                    });
                }
            }
            return toReturn;
        }

    }
}