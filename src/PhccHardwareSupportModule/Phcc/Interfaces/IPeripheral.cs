using System.Collections.Generic;
using Common.MacroProgramming;

namespace PhccHardwareSupportModule.Phcc.Interfaces
{
    public interface IPeripheral
    {

        List<AnalogSignal> AnalogOutputs { get; set; }
        List<AnalogSignal> AnalogInputs { get; set; }
        List<DigitalSignal> DigitalOutputs { get; set; }
        List<DigitalSignal> DigitalInputs { get; set; }
 

 
        void InitializeSignals(object peripheral, object device);

    


    }

    



}