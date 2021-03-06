﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PhccConfiguration.Config.ConfigClasses
{
    /// <summary>
    /// This class is intended to contain the config settings for the 7 segments outputs.
    /// This will allow user to define digital outputs and 7 segments outputs. 
    /// PHCC HSM will need an update to use this configuration!
    /// </summary>
    public sealed class Doa7SegConfiguration
    {
        public List<SegmentDisplayConfig> DisplayModeConfiguration = new List<SegmentDisplayConfig>();
        public List<DigitalOutputConfig> OutputConfig { get; set; } = new List<DigitalOutputConfig>();

    }

    /// <summary>
    /// this config assumes that all pin connections are connected sequentially on the 7 segment board! Assumes a "g f e d c b a" - 8 pins display. it dosent take the "dot"!
    /// It will be difficult to allow the user to select individual pins for each 7 segment output, and a nightmare to configure/manage!
    /// </summary>
    public sealed class SegmentDisplayConfig
    {
        public int NumDisplays { get; set; }
        public DisplayType DisplayType { get; set; }
        public int FirstModule { get; set; }

        public int TotalPins
        {
            get
            {
                if (DisplayType == DisplayType.SevenSegment)
                {
                    return NumDisplays * 8;
                }
                else if (DisplayType == DisplayType.FourteenSegment)
                {
                    return NumDisplays * 15;
                }
                else
                {
                    return NumDisplays * 8;
                }
            }
        }

    }

    public enum DisplayType
    {
        SevenSegment,
        FourteenSegment,

    }
}
