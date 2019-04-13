using System.Collections.Generic;
using Common.HardwareSupport.HardwareConfig;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;

namespace PhccHsmSupport.Phcc
{
    public interface IMasterDevice
    {
        string ComPort { get; set; }
        string Name { get; set; }
        List<DigitalConfig> InputConfig { get; set; }
       // List<ISlaveDevice> Peripherals { get; set; }
        string FriendlyName { get; set; }

        void Initiate(object device);

    }
}