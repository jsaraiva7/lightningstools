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
    public partial class Doa7SegmentModeSelector : Form
    {
        public SegmentDisplayConfig ModeConfiguration { get; set; }
        public Doa7SegmentModeSelector()
        {
            InitializeComponent();
            rb7Segment.Checked = false;
            rbDigital.Checked = true;
            gbconfig.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (rb7Segment.Checked)
            {
                var cfg = new SegmentDisplayConfig();
                cfg.FirstPin = (int) nbFirstPin.Value;
                cfg.NumDisplays = (int) nbSementCount.Value;
                ModeConfiguration = cfg;
                this.Close();
            }

        }

        private void rb7Segment_CheckedChanged(object sender, EventArgs e)
        {
            if (rb7Segment.Checked)
            {
                rbDigital.Checked = false;
                gbconfig.Visible = true;
            }
          
        }

        private void rbDigital_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDigital.Checked)
            {
                rb7Segment.Checked = false;
                gbconfig.Visible = false;
            }
          
        }
    }
}
