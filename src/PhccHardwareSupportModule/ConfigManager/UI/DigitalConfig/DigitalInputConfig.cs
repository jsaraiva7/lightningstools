using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccHardwareSupportModule.ConfigManager.UI.DigitalConfig
{
    public partial class DigitalInputConfig : Form
    {
        public List<DigitalOutputConfig> Configuration { get; set; }


        public DigitalInputConfig(List<DigitalOutputConfig> config)
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
            DigitalModeSelector m = new DigitalModeSelector();
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

        //private bool ConfigValidator(Doa7SegConfiguration config, SegmentDisplayConfig segConfig, DigitalConfig digitConfig )
        //{

       
        //    foreach (var display in config.DisplayModeConfiguration)
        //    {
        //        var fp = display.FirstModule;
        //        var lastPin = display.FirstModule + display.TotalPins;

        //        if (config.DisplayModeConfiguration.Any(x => x.FirstModule >= fp && (x.FirstModule + x.TotalPins) <= lastPin))
        //        {
        //            MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");
                   
        //            return false;
        //        }
        //        if(config.OutputConfig.Any(x => x.PinNumber >= fp && (fp + lastPin) <= x.PinNumber))
        //        {
        //            MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");

        //            return false;
        //        }
        //    }




        //    return true;
        //}
        private void dataViewOutput_SelectionChanged(object sender, EventArgs e)
        {
           
    
        }

    
    }

 
}
