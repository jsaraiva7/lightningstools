using System.Collections.Generic;
using Common.MacroProgramming;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Peripherals.Classes
{
    public abstract class HSMPeripheral : IPeripheral
    {
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();


        public abstract void InitializeSignals(object peripheral, object device);

    }
}