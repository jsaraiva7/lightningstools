using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Common.HardwareSupport.HardwareConfig;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using Common.SimLinkup.Configuration;
using Phcc;
using PhccHsmSupport.Phcc.Inputs;
using PhccHsmSupport.Phcc.Peripherals;

namespace PhccHsmSupport.Phcc
{
    [Serializable]
    public class Motherboard : IMasterDevice, IDisposable
    {
        public Motherboard()
        {
            Peripherals = new List<BasePeripheral>();

        }

        public string ComPort { get; set; }
        public string Name { get; set; }
        public List<DigitalConfig> InputConfig { get; set; } = new List<DigitalConfig>();
        private Device _device { get; set; }
    
        public List<BasePeripheral> Peripherals { get; set; }
        //{
        //    get
        //    {
                
        //        //var tst = Peripherals.ConvertAll(o => (BasePeripheral)o);
        //        return Peripherals.ConvertAll(o => (BasePeripheral)o);
        //    }
        //    set
        //    {
        //        if (value is List<BasePeripheral> tst) Peripherals = tst.ConvertAll(o => (ISlaveDevice) o);
        //    }
        //}

        //[XmlIgnore]
        //public List<ISlaveDevice> Peripherals { get; set; }
       


        public string FriendlyName { get; set; }
        [XmlIgnore]
        public List<AnalogSignal> AnalogOutputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<AnalogSignal> AnalogInputs { get; set; } = new List<AnalogSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalOutputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<DigitalSignal> DigitalInputs { get; set; } = new List<DigitalSignal>();
        [XmlIgnore]
        public List<TextSignal> TextInputs { get; } = new List<TextSignal>();
        [XmlIgnore]
        public List<TextSignal> TextOutputs { get; } = new List<TextSignal>();

        public void Initiate(object device)
        {
            try
            {
                if (device is Device)
                {
                    _device = device as Device;
                    var inDigi = new PhccDigitalImputs(device as Device, InputConfig);
                    DigitalInputs.AddRange(inDigi.DigitalInputs);
                    var inAnalog = new PhccAnalogInputs(device as Device);
                    AnalogInputs.AddRange(inAnalog.AnalogInputs);
                    foreach (var slaveDevice in Peripherals)
                    {
                        slaveDevice.InitializeSignals(device);
                        TextOutputs.Add(GetDeviceFwSignal(device as Device));

                        AnalogInputs.AddRange(slaveDevice.AnalogInputs);
                        AnalogOutputs.AddRange(slaveDevice.AnalogOutputs);
                        DigitalInputs.AddRange(slaveDevice.DigitalInputs);
                        DigitalOutputs.AddRange(slaveDevice.DigitalOutputs);
                        TextInputs.AddRange(slaveDevice.TextInputs);
                        TextOutputs.AddRange(slaveDevice.TextOutputs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        private TextSignal GetDeviceFwSignal(Device device)
        {
            var firmware = "PHCC not Connected";
            try
            {
                firmware = device.FirmwareVersion;
            }
            catch
            {
                // ignored
            }

            var txtS = new List<TextSignal>();
            var thisSignal = new TextSignal
            {
                Category = "Firmware Version",
                CollectionName = "PHCC Firmware Version",
                FriendlyName = firmware,
                Id = $"PhccFwVersion[{device.PortName}]",
                Index = 1,
                PublisherObject = null,
                Source = device,
                SourceAddress = device.PortName,
                SourceFriendlyName = $"PHCC Device on {device.PortName}",
                State = firmware
            };

            return thisSignal;

        }
        private static Device CreateDevice(string comPort)
        {
            var device = new Device(comPort, false);
            return device;
        }

        private static Motherboard Initialize(Motherboard motherboard)
        {
            var _device = CreateDevice(motherboard.ComPort);
            foreach (var peripheral in motherboard.Peripherals)
            {
                peripheral.InitializeSignals(_device);
                motherboard.AnalogInputs.AddRange(peripheral.AnalogInputs);
                motherboard.AnalogOutputs.AddRange(peripheral.AnalogOutputs);
                motherboard.DigitalInputs.AddRange(peripheral.DigitalInputs);
                motherboard.DigitalOutputs.AddRange(peripheral.DigitalOutputs);
                motherboard.TextInputs.AddRange(peripheral.TextInputs);
                motherboard.TextOutputs.AddRange(peripheral.TextOutputs);
                _device.StartTalking();
            }

            return motherboard;
        }

        public static List<Motherboard> GetDevices()
        {
            try
            {
                var hsmPath = SimLinkupConfig.GetConfig().HsmDir;

                var cfg = Directory.GetFiles(hsmPath, "phccHsm.config", SearchOption.AllDirectories).FirstOrDefault();
                if (cfg == null)
                    return null;
                var cf = Common.Serialization.Util.DeserializeFromXmlFile<List<Motherboard>>(cfg);
                var toRet = new List<Motherboard>();
                foreach (var motherboard in cf)
                {
                    var mb = Initialize(motherboard);
                    toRet.Add(mb);
                }
                return toRet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               // throw;
               return null;
            }
        }

        public static void SaveDeviceConfig(List<Motherboard> toSave)
        {

            try
            {
                var hsmPath = SimLinkupConfig.GetConfig().HsmDir;


                var cfg =  Directory.GetFiles(hsmPath, "phccHsm.config", SearchOption.AllDirectories).FirstOrDefault();
                if (cfg != null)
                {

                    Common.Serialization.Util.SerializeToXmlFile(toSave, cfg);
                    return;
                }

                var path = Directory.GetFiles(hsmPath, "Phcc*", SearchOption.AllDirectories).FirstOrDefault();
                FileInfo f = new FileInfo(path);
                path = f.Directory.FullName;
                path = path + "\\Configuration";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var cfgFileName = "phccHsm.config";

                path += "\\" + cfgFileName;

                Common.Serialization.Util.SerializeToXmlFile(toSave, path);
           

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Dispose()
        {
            if (_device != null)
            {
                _device.StopTalking();
                try
                {
                    Common.Util.DisposeObject(_device); //disconnect from the PHCC
                }
                catch (Exception e)
                {
                     
                }
                _device = null;
            }
            GC.SuppressFinalize(this);
        }

    }
}