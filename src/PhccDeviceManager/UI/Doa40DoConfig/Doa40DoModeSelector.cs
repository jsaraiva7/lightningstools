using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhccConfiguration.Config.ConfigClasses;

namespace Phcc.DeviceManager.UI.Doa7SegConfig
{
    public partial class Doa40DoModeSelector : Form
    {
 
        public DigitalOutputConfig DigitalOutputConfig { get; set; }


        public Doa40DoModeSelector()
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
