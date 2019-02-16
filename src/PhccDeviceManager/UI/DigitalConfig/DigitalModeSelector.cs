using System;
using System.Windows.Forms;
using PhccConfiguration.Config.ConfigClasses;

namespace Phcc.DeviceManager.UI.DigitalConfig
{
    public partial class DigitalModeSelector : Form
    {
 
        public DigitalOutputConfig DigitalOutputConfig { get; set; }


        public DigitalModeSelector()
        {
            InitializeComponent();
            gb14Segment.Visible = true;
           
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          

            
                var test = new DigitalOutputConfig();
                test.Inverted = rbReversed.Checked;
                if (rbReversed.Checked)
                {
                    DigitalOutputConfig = new DigitalOutputConfig()
                        {Inverted = true, PinNumber = (int)numOutput.Value};
                }
                this.Close();
           
            

        }
   
    }
}
