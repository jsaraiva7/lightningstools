using System;
using System.Windows.Forms;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;

namespace PhccHsm.ConfigUi.OldForms.Doa7SegConfig
{
    public partial class Doa7SegmentModeSelector : Form
    {
        public SegmentDisplayConfig ModeConfiguration { get; set; }
        public Common.HardwareSupport.HardwareConfig.ConfigClasses.DigitalConfig DigitalOutputConfig { get; set; }


        public Doa7SegmentModeSelector()
        {
            InitializeComponent();
            gb14Segment.Visible = true;
            rb7Segment.Checked = false;
            rbDigital.Checked = true;
            gbconfig.Visible = false;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (rb7Segment.Checked)
            {
                var cfg = new SegmentDisplayConfig();
                cfg.DisplayType = DisplayType.SevenSegment;
                cfg.FirstModule = (int) nbFirstPin.Value;
                cfg.NumDisplays = (int) nbSementCount.Value;
                
                ModeConfiguration = cfg;
                
                this.Close();
            }

            if (rbDigital.Checked)
            {
                var test = new Common.HardwareSupport.HardwareConfig.ConfigClasses.DigitalConfig();
                test.Inverted = rbReversed.Checked;
                if (rbReversed.Checked)
                {
                    DigitalOutputConfig = new Common.HardwareSupport.HardwareConfig.ConfigClasses.DigitalConfig()
                        {Inverted = true, PinNumber = (int)numOutput.Value};
                }
                this.Close();
            }
            

        }

        private void rb7Segment_CheckedChanged(object sender, EventArgs e)
        {
            if (rb7Segment.Checked)
            {
                rbDigital.Checked = false;
                gbconfig.Visible = true;
                gb14Segment.Visible = false;
               
            }
          
        }

        private void rbDigital_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDigital.Checked)
            {
                gb14Segment.Visible = true;
                rb7Segment.Checked = false;
                gbconfig.Visible = false;
               
               
            }
          
        }

       

      
 

        private void nbSementCount_ValueChanged(object sender, EventArgs e)
        {
            if (nbSementCount.Value > 32)
            {
                MessageBox.Show("Maximum possible 14 segments displays is 17 (256 digital lines)");
                nbSementCount.Value = 32;
            }
        }

        private void nbFirstPin_ValueChanged(object sender, EventArgs e)
        {

            if (nbFirstPin.Value + (nbSementCount.Value * 8) > 256)
            {
                var minPin = 256 - nbSementCount.Value * 8;
                MessageBox.Show("Not enough outputs available for " + nbSementCount.Value +
                                "displays, starting at pin " + nbFirstPin.Value + "\nMinimum start pin is pin " + minPin + "\n(256 digital lines)");
                nbFirstPin.Value = minPin;
            }
        }
    }
}
