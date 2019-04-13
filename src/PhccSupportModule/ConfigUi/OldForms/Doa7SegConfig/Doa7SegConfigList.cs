using System;
using System.Collections.Generic;
using System.Linq;

namespace PhccSupportModule.ConfigUi.OldForms.Doa7SegConfig
{
    public partial class Doa7SegConfigList : Form
    {
        public Doa7SegConfiguration Configuration { get; set; }

        public List<DisplayConfigModel> _gridModel = new List<DisplayConfigModel>();

     
        public Doa7SegConfigList(Doa7SegConfiguration config)
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

            dataViewOutput.DataSource = RefreshGridModel(Configuration).ToList();
        }
        private void btnAddMode_Click(object sender, EventArgs e)
        {
            Doa7SegmentModeSelector m = new Doa7SegmentModeSelector();
            m.ShowDialog();
            if (Configuration == null)
            {
                Configuration = new Doa7SegConfiguration();
               
            }

            if (m.ModeConfiguration != null)
            {
                Configuration.DisplayModeConfiguration.Add(m.ModeConfiguration);
                ConfigValidator(Configuration, m.ModeConfiguration, null);
            }

            if (m.DigitalOutputConfig != null)
            {
                Configuration.OutputConfig.Add(m.DigitalOutputConfig);
                ConfigValidator(Configuration, null, m.DigitalOutputConfig);
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
                var row = (dataViewOutput.SelectedRows[0].DataBoundItem as DisplayConfigModel);
                if (row != null)
                {
                    if (row.Mode.Contains("Digital"))
                    {
                        var pin = int.Parse(row.FirstPin);
                        var data = Configuration.OutputConfig.FirstOrDefault(c => c.PinNumber == pin);
                        if (data != null)
                        {
                            Configuration.OutputConfig.Remove(data);
                        }
                    }
                    else
                    {
                        var pin = int.Parse(row.FirstPin);
                        var data = Configuration.DisplayModeConfiguration.FirstOrDefault(c => c.FirstModule == pin);
                        if (data != null)
                        {
                            Configuration.DisplayModeConfiguration.Remove(data);
                        }
                    }

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

        private bool ConfigValidator(Doa7SegConfiguration config, SegmentDisplayConfig segConfig, Common.HardwareSupport.HardwareConfig.ConfigClasses.DigitalConfig digitConfig )
        {

       
            //foreach (var display in config.DisplayModeConfiguration)
            //{
            //    var fp = display.FirstModule;
            //    var lastPin = display.FirstModule + display.TotalPins;

            //    if (config.DisplayModeConfiguration.Any(x => x.FirstModule >= fp && (x.FirstModule + x.TotalPins) <= lastPin))
            //    {
            //        MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");
                   
            //        return false;
            //    }
            //    if(config.OutputConfig.Any(x => x.PinNumber >= fp && (fp + lastPin) <= x.PinNumber))
            //    {
            //        MessageBox.Show("there was an error on your configuration. \n Please check your pin numbers.\n Note - Your defenitions were added to the configuration. \n either delete or accept");

            //        return false;
            //    }
            //}




            return true;
        }
        private void dataViewOutput_SelectionChanged(object sender, EventArgs e)
        {
           
    
        }

        private List<DisplayConfigModel> RefreshGridModel(Doa7SegConfiguration config)
        {
            var toRet = new List<DisplayConfigModel>();

            foreach (var disp in config.DisplayModeConfiguration)
            {
                if(disp != null)
                toRet.Add(new DisplayConfigModel()
                {
                    Mode = "Display",
                    DisplayCount = disp.NumDisplays.ToString(),
                    FirstPin = disp.FirstModule.ToString(),
                    TotalPins = disp.TotalPins.ToString(),
                    Reversed = false
                });
            }

            foreach (var disp in config.OutputConfig)
            {
                if (disp != null)
                    toRet.Add(new DisplayConfigModel()
                {
                    Mode = "Digital Output",
                    FirstPin = disp.PinNumber.ToString(),
                    Reversed = disp.Inverted
                });
            }




            return toRet;
        }
    }

    public class DisplayConfigModel
    {

        public string Mode { get; set; } = "";
        public bool Reversed { get; set; } = false;
        public string DisplayCount { get; set; } = "";
        public string FirstPin { get; set; } = "";
        public string TotalPins { get; set; } = "";
    }
}
