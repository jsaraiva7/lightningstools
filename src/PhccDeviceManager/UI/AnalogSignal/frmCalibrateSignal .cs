using System;
using System.Windows.Forms;
using Common.MacroProgramming;

namespace Phcc.DeviceManager.UI
{
    public partial class frmCalibrateSignal : Form
    {
        private AnalogSignal _signal;

        public CalibrationPoint Point { get; set; }= new CalibrationPoint(0,0);

        public frmCalibrateSignal(AnalogSignal signal, CalibrationPoint point)
        {
            InitializeComponent();
            _signal = signal;
            Point = point;
            trkCalibrateServoOffset.Minimum = (int)_signal.MinValue;
            trkCalibrateServoOffset.Maximum = (int) _signal.MaxValue;
            trkCalibrateServoOffset.Value = (int) Point.Output;
            nudCalibrateOffset.Minimum = (int)_signal.MinValue;
            nudCalibrateOffset.Maximum = (int)_signal.MaxValue;
            nudCalibrateOffset.Value = (int)Point.Output;
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
                trkCalibrateServoOffset.Value = (int)nudCalibrateOffset.Value; ;
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
