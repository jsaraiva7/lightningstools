﻿using Common.MacroProgramming;
using System.Collections.Generic;
using Solid.Arduino;
using Solid.Arduino.Firmata;

namespace ArduinoSupportModule.HSM.SignalHandler
{
    public class DigitalOutput : IPinSignal
    {
        public DigitalOutput()
        {

        }



        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();


        public void InitializeSignals(PinState pinState, object device)
        {
            DigitalOutputs.AddRange(GenerateDigitalSignals(pinState, device));
        }


        private List<DigitalSignal> GenerateDigitalSignals(PinState pinState, object device)
        {
            List<DigitalSignal> digitalSignalsToReturn = new List<DigitalSignal>();

            var typedDevice = device as ArduinoSession;

            var thisSignal = new DigitalSignal
            {
                Category = "Outputs",
                CollectionName = "Digital Outputs - " + typedDevice.FirmwareVersion,
                FriendlyName = "Digital Output at Pin" + pinState.PinNumber,
                Id = "Duino" + "[" + typedDevice.FirmwareVersion + "][" + pinState.PinNumber + "][" + pinState.Mode + "]",
                Index = pinState.PinNumber,
                PublisherObject = this,
                Source = device,
                SourceFriendlyName = $"Arduino Firmata on {typedDevice.ComPort}",
                SourceAddress = typedDevice.ComPort,
                SubSource = $"Arduino Firmata @ {typedDevice.ComPort}",
                SubSourceFriendlyName = $"Arduino Firmata @ {typedDevice.ComPort}",
                State = false
            };
            thisSignal.SignalChanged += SignalChanged;
            digitalSignalsToReturn.Add(thisSignal);

            return digitalSignalsToReturn;
        }


        private void SignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            //var source = sender as DigitalSignal;
            //if (source?.Index == null) return;
            //var outputNum = source.Index.Value;
            //var baseAddress = source.SubSourceAddress;
            //var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            //var device = source.Source as Device;
            //if (device == null) return;
            //var newBitVal = args.CurrentState;
            //if (source.FriendlyName.Contains("(Inverted)"))
            //{
            //    newBitVal = !args.CurrentState;
            //}
            //var peripheralState = _peripheralByteStates[baseAddress];
            //var connectorNumZeroBase = outputNum / 8;
            //var thisBitIndex = outputNum % 8;
            //var connectorNum = connectorNumZeroBase + 3;
            //var currentBitsThisConnector = peripheralState[connectorNumZeroBase];
            //var newBits = Common.Util.SetBit(currentBitsThisConnector, thisBitIndex, newBitVal);
            //try
            //{
            //    device.DoaSend40DO(baseAddressByte, (byte)connectorNum, newBits);
            //    peripheralState[connectorNumZeroBase] = newBits;
            //}
            //catch (Exception e)
            //{
            //    _log.Error(e.Message, e);
            //}

        }
    }
}