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
    public partial class Doa40DoConfig : Form
    {
        public List<DigitalOutputConfig> Configuration { get; set; }


        public Doa40DoConfig(List<DigitalOutputConfig> config)
        {
            InitializeComponent();
            Configuration = config;
           
        }

        private void Doa7SegConfigList_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {

            dataViewOutput.DataSource = Configuration.ToList();
        }
        private void btnAddMode_Click(object sender, EventArgs e)
        {
            Doa40DoModeSelector m = new Doa40DoModeSelector();
            m.ShowDialog();
            if (Configuration == null)
            {
                Configuration = new List<DigitalOutputConfig>();             
            }

            if (m.DigitalOutputConfig != null)
            {
                Configuration.Add(m.DigitalOutputConfig);
             
            }

            RefreshGrid();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var row = (dataViewOutput.SelectedRows[0].DataBoundItem as DigitalOutputConfig);
                if (row != null)
                {
                    Configuration.Remove(row);
                }
                else
                {
                    MessageBox.Show("Now Row selected! \n Please select a row and try again.");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Now Row selected! \n Please select a row and try again.");
            }

            RefreshGrid();
        }

        private bool ConfigValidator(Doa7SegConfiguration config, SegmentDisplayConfig segConfig, DigitalOutputConfig digitConfig )
        {

       
            foreach (var display in config.DisplayModeConfiguration)
            {
                var fp = display.FirstPin;
                var lastPin = display.FirstPin + display.TotalPins;

                if (config.DisplayModeConfiguration.Any(x => x.FirstPin >= fp && (x.FirstPin + x.TotalPins) <= lastPin))
                {
                    MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");
                   
                    return false;
                }
                if(config.OutputConfig.Any(x => x.PinNumber >= fp && (fp + lastPin) <= x.PinNumber))
                {
                    MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");

                    return false;
                }
            }




            return true;
        }
        private void dataViewOutput_SelectionChanged(object sender, EventArgs e)
        {
           
    
        }

    
    }

 
}
