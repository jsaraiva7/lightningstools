using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.HardwareSupport;
using Common.MacroProgramming;
using log4net;

namespace VjoyHardwareSupportModule.Device
{
    public class VjoyHardwareSupportModule : HardwareSupportModuleBase, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(VjoyHardwareSupportModule));
    
        private bool _isDisposed;
        private vJoyDevice _vJoyDevice;

        private VjoyHardwareSupportModule()
        {  
            _vJoyDevice = new vJoyDevice();
        }

        public override void Configure()
        {
            MessageBox.Show("Please use vJoy Configuration app. \nRefer to vJoy Documentation for reference.");
        }


        public override AnalogSignal[] AnalogInputs => _vJoyDevice.AxesOutput.ToArray();


        public override DigitalSignal[] DigitalInputs => _vJoyDevice.ButtonsOutput.ToArray();


        public override string FriendlyName
        {
            get
            {
                return "vJoy vJoyDevice emulator";
            }
        }

        public void Dispose()
        {
            Dispose(true);
            _vJoyDevice = null;
            GC.SuppressFinalize(this);
        }

        ~VjoyHardwareSupportModule()
        {
            Dispose();
        }

        public static IHardwareSupportModule[] GetInstances()
        {
            var toReturn = new List<IHardwareSupportModule>();
            try
            {
             
                toReturn.Add(new VjoyHardwareSupportModule());
                return toReturn.ToArray();
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

            return toReturn.ToArray();
        }




        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }
            }
            _isDisposed = true;
        }
    }
}