using System;
using System.Collections.Generic;
using System.Linq;
using Common.MacroProgramming;
using log4net;
using Phcc;
using PhccConfiguration.Config;
using PhccHardwareSupportModule.Phcc.Interfaces;

namespace PhccHardwareSupportModule.Phcc.Peripherals.Classes
{
    public class HSMDoaAnOut1 : IPeripheral
    {

        public AnalogSignal[] AnalogOutputs { get; set; }
        public AnalogSignal[] AnalogInputs { get; set; }
        public DigitalSignal[] DigitalOutputs { get; set; }
        public DigitalSignal[] DigitalInputs { get; set; }
        Dictionary<string, byte[]> _peripheralByteStates = new Dictionary<string, byte[]>();
        Dictionary<string, double[]> _peripheralFloatStates = new Dictionary<string, double[]>();
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));



        private Peripheral _peripheral;
        private Device _device;
        private string _portName;

        public HSMDoaAnOut1(Peripheral peripheral, Device device, string portName)
        {
            _peripheral = peripheral;
            _device = device;
            _portName = portName;
            try
            {
                if (device == null) throw new ArgumentNullException(nameof(device));
                if (peripheral == null) throw new ArgumentNullException(nameof(peripheral));
                SendAnOut1Calibrations(device, peripheral);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }
        public void InitializeSignals()
        {
            List<AnalogSignal> analogSignalsToReturn = new List<AnalogSignal>();
            var typedPeripheral = _peripheral as PhccConfiguration.Config.DoaAnOut1;
            var baseAddress = "0x" + typedPeripheral.Address.ToString("X4");
            for (var i = 0; i < 16; i++)
            {
                var thisSignal = new CalibratedAnalogSignal
                {
                    Category = "Outputs",
                    CollectionName = "Analog Outputs - " + _peripheral.FriendlyName,
                    FriendlyName = $"Analog Output {string.Format($"{i + 1:0}", i + 1)}",
                    Id = $"DOA_ANOUT1[{baseAddress}][{_portName}][{i}]",
                    Index = i,
                    PublisherObject = this,
                    Source = _device,
                    SourceFriendlyName = $"PHCC Device on {_portName}",
                    SourceAddress = _portName,
                    SubSource = $"DOA_ANOUT1 @ {baseAddress}",
                    SubSourceFriendlyName = $"DOA_ANOUT1 @ {baseAddress}",
                    SubSourceAddress = baseAddress,
                    MinValue = 0,
                    MaxValue = 5,
                    IsVoltage = true,
                    State = 0
                };
                thisSignal.SignalChanged += DOAAnOut1SignalChanged;
                thisSignal.CalibrationData = typedPeripheral.OutputConfigs
                    .FirstOrDefault(x => x.SignalId.Equals(thisSignal.Id))?.CalibrationData;
                analogSignalsToReturn.Add(thisSignal);
            }
            _peripheralFloatStates[baseAddress] = new double[16];
            AnalogOutputs = analogSignalsToReturn.ToArray();
        }




        private static void SendAnOut1Calibrations(Device device, Peripheral peripheral)
        {
            var anOut1Config = peripheral as DoaAnOut1;
            if (device == null) throw new ArgumentNullException(nameof(device));
            if (anOut1Config == null) throw new ArgumentNullException(nameof(anOut1Config));

            try
            {
                device.DoaSendAnOut1GainAllChannels(anOut1Config.Address, anOut1Config.GainAllChannels);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }

        private void DOAAnOut1SignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            var source = sender as CalibratedAnalogSignal;
            if (source?.Index == null) return;
            var channelNumZeroBase = source.Index.Value;
            var baseAddress = source.SubSourceAddress;
            var baseAddressByte = Convert.ToByte(baseAddress.Replace("0x", ""), 16);
            var device = source.Source as Device;
            if (device == null) return;
            var channelNum = channelNumZeroBase + 1;
            var newVal = (byte)(Math.Abs(args.CurrentState) / 5.00 * 255);
            try
            {
                device.DoaSendAnOut1(baseAddressByte, (byte)channelNum, newVal);
                _peripheralFloatStates[baseAddress][channelNum] = newVal;
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

        }
    }
}
