using Common.MacroProgramming;

namespace PhccHardwareSupportModule.Phcc.Interfaces
{
    public interface IPeripheral
    {

        AnalogSignal[] AnalogOutputs { get; set; }
        AnalogSignal[] AnalogInputs { get; set; }
        DigitalSignal[] DigitalOutputs { get; set; }
        DigitalSignal[] DigitalInputs { get; set; }
 

 
        void InitializeSignals();

    


    }

    



}