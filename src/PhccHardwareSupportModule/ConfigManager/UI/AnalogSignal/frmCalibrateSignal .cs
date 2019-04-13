using System;
using System.Windows.Forms;
using Common.MacroProgramming;

namespace PhccHardwareSupportModule.ConfigManager.UI.AnalogSignal
{
    public partial class frmCalibrateSignal : Form
    {
        private Common.MacroProgramming.AnalogSignal _signal;

        public CalibrationPoint Point { get; set; }= new CalibrationPoint(0,0);

        public frmCalibrateSignal(Common.MacroProgramming.AnalogSignal signal, CalibrationPoint point)
        {
            InitializeComponent();
            _signal = signal;
            Point = point;
          
         
           
            if (_signal.MinValue < int.MinValue)
            {
                trkCalibrateServoOffset.Minimum = 0;
                trkCalibrateServoOffset.Visible = false;
              
            }
            else
            {
                  trkCalibrateServoOffset.Minimum = (int)_signal.MinValue;
                  trkCalibrateServoOffset.Value = (int)Point.Output;
            }

            if (_signal.MaxValue > int.MaxValue)
            {
                trkCalibrateServoOffset.Maximum = int.MaxValue;
                trkCalibrateServoOffset.Visible = false;
            }
            else
            {
                trkCalibrateServoOffset.Maximum = (int)_signal.MaxValue;
                trkCalibrateServoOffset.Value = (int)Point.Output;
            }
            nudCalibrateOffset.Minimum = (int)_signal.MinValue;
            nudCalibrateOffset.Maximum = (int)_signal.MaxValue;
           // nudCalibrateOffset.Value = (int)Point.Output;
            inputValue.Value = (int) Point.Input;

        }

        public int InputValue
        {
            get => (int) inputValue.Value;
            set => inputValue.Value = value;
        }

        public int CalibrationOffset
        {
            get =>  (int)nudCalibrateOffset.Value;
            set => nudCalibrateOffset.Value = value;
        }




        private void trkCalibrateServoOffset_Scroll(object sender, EventArgs e)
        {
            nudCalibrateOffset.Value = trkCalibrateServoOffset.Value;
            _signal.State = (double) nudCalibrateOffset.Value;
        }

        private void nudCalibrateServoOffset_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (trkCalibrateServoOffset.Visible)
                {
                    trkCalibrateServoOffset.Value = (int)nudCalibrateOffset.Value;
                }     
                _signal.State = (double)nudCalibrateOffset.Value;
            }
            catch (Exception exception)
            {
               
            }
         
        }

        private void CalibrateServoWizard_Load(object sender, EventArgs e)
        {

        }

        private void frmCalibrateSignal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Point.Input = (double)inputValue.Value;
            Point.Output = (double) nudCalibrateOffset.Value;
        }
    }
}
