using System;
using System.Windows.Forms;
using Common.MacroProgramming;

namespace Phcc.DeviceManager.UI
{
    public partial class frmCalibrateSignal : Form
    {
        private AnalogSignal _signal;

        public frmCalibrateSignal(AnalogSignal signal)
        {
            InitializeComponent();
            _signal = signal;
            if (signal.MaxValue != 0)
            {
                trkCalibrateServoOffset.Minimum = (int)signal.MinValue;
                trkCalibrateServoOffset.Maximum = (int)signal.MaxValue;
            }
          
        }

        public ushort InputValue
        {
            get => (ushort) inputValue.Value;
            set => inputValue.Value = value;
        }

        public ushort CalibrationOffset
        {
            get => (ushort) nudCalibrateOffset.Value;
            set => nudCalibrateOffset.Value = value;
        }




        private void trkCalibrateServoOffset_Scroll(object sender, EventArgs e)
        {
            nudCalibrateOffset.Value = trkCalibrateServoOffset.Value;
            _signal.State = (double) nudCalibrateOffset.Value;
        }

        private void nudCalibrateServoOffset_ValueChanged(object sender, EventArgs e)
        {
            trkCalibrateServoOffset.Value = (int) nudCalibrateOffset.Value;
            _signal.State = (double) nudCalibrateOffset.Value;
        }
    }
}
