using System;
using System.Collections.Generic;
using System.Linq;
using Common.MacroProgramming;
using log4net;
using vJoyInterfaceWrap;

namespace VjoyHardwareSupportModule.Device
{
    public class vJoyDevice
    {
        public List<AnalogSignal> AxesOutput { get; set; } = new List<AnalogSignal>();
        public List<DigitalSignal> ButtonsOutput { get; set; } = new List<DigitalSignal>();


        private vJoy _joystick = new vJoy();
        private vJoy.JoystickState _iReport = new vJoy.JoystickState();
        private static readonly ILog _log = LogManager.GetLogger(typeof(vJoyDevice));

        public vJoyDevice()
        {
            try
            {
                GetSignals();
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }
        private void GetSignals()
        {

            if (!_joystick.vJoyEnabled())
            {
                return;
            }

            for (uint i = 0; i < 16; i++)
            {
                VjdStat status = _joystick.GetVJDStatus(i);
                if (status == VjdStat.VJD_STAT_FREE)
                {
                    try
                    {
                        _joystick.AcquireVJD(i);
                        if (_joystick.GetVJDStatus(i) == VjdStat.VJD_STAT_OWN)
                        {
                            AxesOutput.AddRange(BuildAxes(i));
                            ButtonsOutput.AddRange(BuildButtons(i));
                        }
                        
                    }
                    catch (Exception e)
                    {
                        _log.Error(e);
                    }
                 
                }
            }

        }


        private List<AnalogSignal> BuildAxes(uint id)
        {
            var toRet = new List<AnalogSignal>();
            toRet.Add(GetaxisSignal(id, "X", HID_USAGES.HID_USAGE_X));
            toRet.Add(GetaxisSignal(id, "Y", HID_USAGES.HID_USAGE_Y));
            toRet.Add(GetaxisSignal(id, "Z", HID_USAGES.HID_USAGE_Z));
            toRet.Add(GetaxisSignal(id, "RX", HID_USAGES.HID_USAGE_RX));
            toRet.Add(GetaxisSignal(id, "RY", HID_USAGES.HID_USAGE_RY));
            toRet.Add(GetaxisSignal(id, "RZ", HID_USAGES.HID_USAGE_RZ));
            toRet.Add(GetaxisSignal(id, "SLIDER 0", HID_USAGES.HID_USAGE_SL0));
            toRet.Add(GetaxisSignal(id, "SLIDER 1", HID_USAGES.HID_USAGE_SL1));
            toRet.Add(GetaxisSignal(id, "WHEEL", HID_USAGES.HID_USAGE_WHL));
            return toRet;
        }

        private List<DigitalSignal> BuildButtons(uint id)
        {
            var toRet = new List<DigitalSignal>();
            var count = _joystick.GetVJDButtonNumber(id);
            for (int i = 1; i <= count; i++)
            {
                var thisSignal = new DigitalSignal
                {
                    Category = "Buttons",
                    CollectionName = "vJoy Id: " + id + "ButtonsOutput",
                    FriendlyName = "vJoy vJoyDevice Id: " + id + " Button: " + i,
                    Id = "[vJoy]" + "[" + id + "]" + "[Button" +i+ "]",
                    PublisherObject = this,
                    Source = _joystick,
                    SourceFriendlyName = "vJoy Feeder",
                    SourceAddress = id.ToString(),
                    Index = i,
                    SubSourceFriendlyName = null,
                };

                thisSignal.SignalChanged += device_DigitalInputChanged;
                toRet.Add(thisSignal);
            }

            return toRet;
        }


        private AnalogSignal GetaxisSignal(uint id, string name,  HID_USAGES usages)
        {
            long maxval = 0;
            long minval = 0;
            _joystick.GetVJDAxisMax(id, HID_USAGES.HID_USAGE_X, ref maxval);
            _joystick.GetVJDAxisMin(id, HID_USAGES.HID_USAGE_X, ref minval);
            var thisSignal = new CalibratedAnalogSignal()
            {
                Category = "Axis",
                
                CollectionName = "vJoy Id: " + id + "AxesOutput",
                FriendlyName = "vJoyDevice Id: " + id + " " + name  ,
                Id = "[vJoy]" + "[" + id + "]" + "[" + name + "]",
                Precision = 1,
                PublisherObject = this,
                Source = _joystick,
                SourceFriendlyName = "vJoy Feeder",
                SourceAddress = id.ToString(),
                SubSource = (int) usages,
                SubSourceFriendlyName = "0",
                MinValue = minval,
                MaxValue = maxval,
                SubSourceAddress = name,
               

            };         
            thisSignal.SignalChanged += AxisChanged;
            return thisSignal;
        }

        private void AxisChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            try
            {
                var source = sender as CalibratedAnalogSignal;
                if (source != null)
                {

                    var min = 0;
                    var max = 32765;
                    var sourceMax = int.Parse(source.SubSourceFriendlyName);
                    var stateInt = (int)source.State;
                    if (source.State > sourceMax)
                    {

                        source.SubSourceFriendlyName = stateInt.ToString();
                        sourceMax = stateInt;
                    }


                    var fromAbs = source.State - 0;
                    var fromMaxAbs = sourceMax - 0;

                    var normal = fromAbs / fromMaxAbs;

                    var toMaxAbs = max - min;
                    var toAbs = toMaxAbs * normal;

                    int to = (int)(toAbs + min);

                    var id = uint.Parse(source.SourceAddress);
                    var intUsages = source.SubSource;
                    var usage = (HID_USAGES) intUsages;
               
                    var res = _joystick.SetAxis(to, id, usage);
                    if (!res)
                    {
                        _log.Error("unable to set axis" + source.Id);
                    }
                }

            }
            catch (Exception e)
            {
               _log.Error(e);
            }
          



        }


        private void device_DigitalInputChanged(object sender, DigitalSignalChangedEventArgs e)
        {
            try
            {
                var signal = sender as DigitalSignal;
                var deviceId = uint.Parse(signal.SourceAddress);
                var button = signal.Index;
                if (button != null)
                {
                    if (_joystick.SetBtn(signal.State, deviceId, (uint)button))
                    {
                        _log.Error("unable to set button" + button + " at device " + deviceId);
                    }
                }

            }
            catch (Exception exception)
            {
                _log.Error(e);
            }
            
        }
    }
}