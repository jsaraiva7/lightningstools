using System.Collections.Generic;
using Common.MacroProgramming;

namespace Common.HardwareSupport.HardwareConfig
{
    public interface ISlaveDevice
    {
        int Address { get; set; }
        string FriendlyName { get; set; }

        List<AnalogSignal> AnalogInputs { get; }
        List<AnalogSignal> AnalogOutputs { get; }
        List<DigitalSignal> DigitalInputs { get; }
        List<DigitalSignal> DigitalOutputs { get; }
        List<TextSignal> TextInputs { get; }
        List<TextSignal> TextOutputs { get; }

        void InitializeSignals(object SorceDevice);
    }
}