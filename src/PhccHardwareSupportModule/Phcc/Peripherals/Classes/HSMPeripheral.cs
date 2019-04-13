using System.Collections.Generic;
using Common.HardwareSupport.HardwareConfig;
using Common.MacroProgramming;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Peripherals.Classes
{
    public abstract class HSMPeripheral : ISlaveDevice
    {
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();
        public int Address { get; set; }
        public string FriendlyName { get; set; }
        public abstract void InitializeSignals( object device);

    }
}