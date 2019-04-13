using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ArduinoSupportModule.HSM.SignalHandler;
using Common.HardwareSupport;
using Common.InputSupport;
using Common.MacroProgramming;
using log4net;
using Solid.Arduino;
using Solid.Arduino.Firmata;
using System.Threading.Tasks;
using Lavspent.DaisyChain.Adc;
using Lavspent.DaisyChain.Firmata;
using Lavspent.DaisyChain.Firmata.Firmata;
using Lavspent.DaisyChain.Misc;
using Lavspent.DaisyChain.Serial;
using PinMode = Solid.Arduino.Firmata.PinMode;

namespace ArduinoSupportModule
{
    public class ArduinoHardwareSupportModule : HardwareSupportModuleBase, IDisposable
    {


        private static readonly ILog _log = LogManager.GetLogger(typeof(ArduinoHardwareSupportModule));
        private string _firmware = "";
        private string _comPort;
        private ArduinoSession _device;



        private List<AnalogSignal> _analogOutputs { get; set; } = new List<AnalogSignal>();
        private List<AnalogSignal> _analogInputs { get; set; } = new List<AnalogSignal>();
        private List<DigitalSignal> _digitalOutputs { get; set; } = new List<DigitalSignal>();
        private List<DigitalSignal> _digitalInputs { get; set; } = new List<DigitalSignal>();

        public ArduinoHardwareSupportModule(ISerialConnection connection)
        {
            _device = new ArduinoSession(connection);
            this.GetCapabilities();
        }


        private void GetCapabilities()
        {
            try
            {
                // open serial (port)
           

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _firmware = _device.FirmwareVersion;
            _comPort = _device.ComPort;

            var pinCount = _device.GetBoardCapability();
            var states = new List<PinState>(); 
            for (int i = 0; i < pinCount.Pins.Length; i++)
            {
               states.Add(_device.GetPinState(i + 1));
            }

            foreach (var pinState in states)
            {
                var test = pinState.Mode;
                if (pinState.Mode == PinMode.AnalogInput)
                {
                    var input = new AnalogInput();
                    input.InitializeSignals(pinState, _device);
                    _analogInputs.AddRange(input.AnalogInputs);
                }
                else if (pinState.Mode == PinMode.PwmOutput)
                {
                    var input = new AnalogOutput();
                    input.InitializeSignals(pinState, _device);
                    _analogOutputs.AddRange(input.AnalogOutputs);
                }
                else if(pinState.Mode == PinMode.DigitalInput)
                {
                    var input = new DigitalInput();
                    input.InitializeSignals(pinState, _device);
                    _digitalInputs.AddRange(input.DigitalInputs);
                }
                else if(pinState.Mode == PinMode.DigitalOutput)
                {
                    var input = new DigitalOutput();
                    input.InitializeSignals(pinState, _device);
                    _digitalOutputs.AddRange(input.DigitalOutputs);
                }
            }

        }


        public override AnalogSignal[] AnalogInputs => _analogInputs.ToArray();

        public override DigitalSignal[] DigitalInputs => _digitalInputs.ToArray();

        public override AnalogSignal[] AnalogOutputs => _analogOutputs.ToArray();

        public override DigitalSignal[] DigitalOutputs => _digitalOutputs.ToArray();

        static async Task MainAsyggnc()
        {

            try
            {



                CancellationToken cancellationToken = new CancellationToken(false);
                // open serial (port)
                var serial = await SerialPortController.OpenSerialAsync("COM4", 115400, Parity.None, 8, StopBits.One);

                // Initialize a firmata client. We assume there is a ConfigurableFirmata/or other firmata with support for Encoders on the other side.
                var firmataClient = await FirmataClient.OpenAsync(serial);



                var analogGpioController = await firmataClient.GetAdcControllerAsync();


                IAdcChannel analogPin = await analogGpioController.OpenAdcChannelAsync(0);
                var count = analogGpioController.ChannelCount;

                analogPin.Subscribe(
                    new Observer<IAdcChannelValue>(
                        (analogGpioValue) =>
                        {
                            // and write any updates to the console
                            Console.WriteLine($"Pin value: {analogGpioValue.Value}");
                        },
                        null,
                        null
                    ));

                //firmataClient.Dispose();
                //serial.CloseAsync();
                //serial.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static IHardwareSupportModule[] GetInstances()
        {
            MainAsyggnc();

            var toReturn = new List<IHardwareSupportModule>();

            var connection = GetConnections();

            try
            {
                foreach (var serialConnection in connection)
                {
                    //IHardwareSupportModule thisHsm =
                    //    new ArduinoHardwareSupportModule(serialConnection);
                 //   toReturn.Add(thisHsm);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
            return toReturn.ToArray();
        }


        private static List<ISerialConnection> GetConnections()
        {
             
            var connection = EnhancedSerialConnection.FindAll();

            if (connection.Count == 0)
                _log.Error("No connection found. Make shure your Arduino board is attached to a USB port.");
                //Console.WriteLine("No connection found. Make shure your Arduino board is attached to a USB port.");
             

            return connection;
        }


        public override string FriendlyName
        {
            get
            {

                return "Arduino Device";
            }
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}