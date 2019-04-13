using System;
using System.Windows.Forms;

namespace PhccHsm.ConfigUi.OldForms.DigitalConfig
{
    public partial class DigitalModeSelector : Form
    {
 
        public Common.HardwareSupport.HardwareConfig.ConfigClasses.DigitalConfig DigitalOutputConfig { get; set; }


        public DigitalModeSelector()
        {
            InitializeComponent();
            gb14Segment.Visible = true;
           
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
}
