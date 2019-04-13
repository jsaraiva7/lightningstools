using Common.MacroProgramming;
using System.Collections.Generic;
using Solid.Arduino;
using Solid.Arduino.Firmata;


namespace ArduinoSupportModule.HSM.SignalHandler
{
    public interface IPinSignal
    {
        List<AnalogSignal> AnalogOutputs { get; set; }
        List<AnalogSignal> AnalogInputs { get; set; }
        List<DigitalSignal> DigitalOutputs { get; set; }
        List<DigitalSignal> DigitalInputs { get; set; }



        void InitializeSignals(PinState pinState, object device);
    }
}