using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Media.Media3D;
using Common.HardwareSupport;
using Common.MacroProgramming;
using log4net;
using PhccHsm.ConfigUi;
using PhccHsm.HsmSupport;
using PhccHsmSupport.Phcc;
using p = Phcc;
 

namespace PhccHsm.Phcc
{
    public class PhccHardwareSupportModule : HardwareSupportModuleBase, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PhccHardwareSupportModule));
 

      
       
        private p.I2CDataReceivedEventHandler _i2cDataReceivedEventHandler;
        private bool _isDisposed;
        private Motherboard _motherboard;
        private string _moduleName = "";
        private List<Motherboard> _cfg;

        private PhccHardwareSupportModule()
        {
        }

        

        ///Configuration GUI callBack!
        ///
        ///
        public override void Configure()
        {
            try
            {
                if (_cfg != null)
                {
                    Window cfg = new Window();
                    var p = new PhccConfigMain(cfg, _cfg);
                    cfg.Content = p;
                    cfg.ShowDialog();
                    p.Save();
                }
                else
                {
                    Window cfg = new Window();
                    var p = new PhccConfigMain(cfg, null);
                    cfg.Content = p;
                    cfg.ShowDialog();
                   
                  
                }
            }
            catch (Exception e)
            {
               _log.Error(e);
            }
        }



        /// <summary>
        /// static method added to allow module initialization from other contexts. Returns an HSM object, that can be used to calibrate the outputs. (by actually moving the needles!)
        /// the returned HSM ignores any previous calibrations!
        /// </summary>
        /// <param name="motherboard"></param>
        /// <returns></returns>
        //public static PhccHardwareSupportModule GetModuleForCalibration(Motherboard motherboard)
        //{
        //    var hsm = new PhccHardwareSupportModule(motherboard);
        //    foreach (var calOutput in hsm._analogOutputSignals)
        //    {
        //        if (calOutput is CalibratedAnalogSignal)
        //        {
        //            var cal = calOutput as CalibratedAnalogSignal;
        //            cal.CalibrationData = null;
        //        }
                
        //    }
        //    return hsm;
        //}

       

        private PhccHardwareSupportModule(Motherboard motherboard, List<Motherboard> cfg) : this()
        {
            _cfg = cfg;
            if (motherboard != null)
            {
                try
                {
                    if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
                    _motherboard = motherboard;
                }
                catch (Exception e)
                {
                    _log.Error(e.Message, e);
                }
                if (_motherboard != null)
                {
                    _moduleName = _motherboard.FriendlyName;
                }
                else
                {
                    _moduleName = "PHCC - Not Connected";
                }
            }
            else
            {
                _motherboard = new Motherboard();
                _moduleName = "PHCC - No config found";
            }
         

        }

 

        public override AnalogSignal[] AnalogInputs => _motherboard.AnalogInputs.ToArray();

        public override AnalogSignal[] AnalogOutputs => _motherboard.AnalogOutputs.ToArray();

        public override DigitalSignal[] DigitalInputs => _motherboard.DigitalInputs.ToArray();

        public override DigitalSignal[] DigitalOutputs => _motherboard.DigitalOutputs.ToArray();
        public override TextSignal[] TextOutputs => _motherboard.TextOutputs.ToArray();
        public override TextSignal[] TextInputs => _motherboard.TextInputs.ToArray();

        public override string FriendlyName => _moduleName;


        public  static IHardwareSupportModule[] GetInstances()
        {
            var toReturn = new List<IHardwareSupportModule>();
            try
            {
                var motherboards = Motherboard.GetDevices();
                if (motherboards != null)
                {
                    foreach (var m in motherboards)
                    {
                        IHardwareSupportModule thisHsm = new PhccHardwareSupportModule(m, motherboards);
                        toReturn.Add(thisHsm);
                    }
                }
                else
                {
                    if (!toReturn.Any())
                    {
                        IHardwareSupportModule thisHsm = new PhccHardwareSupportModule(null, null);
                        toReturn.Add(thisHsm);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }

            try
            {
              
                return toReturn.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return toReturn.ToArray();
            }
           
        }

        

      

    

        #region Disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PhccHardwareSupportModule()
        {
            Dispose();
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _motherboard.Dispose();
                }
            }
            _isDisposed = true;
        }

      

        #endregion

   

    }
}