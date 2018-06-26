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
    public partial class Doa7SegConfigList : Form
    {
        public Doa7SegConfiguration Configuration { get; set; }

        private SegmentDisplayConfig _selectedConfig;
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
            dataViewOutput.DataSource = Configuration.DisplayModeConfiguration.ToList();
        }
        private void btnAddMode_Click(object sender, EventArgs e)
        {
            Doa7SegmentModeSelector m = new Doa7SegmentModeSelector();
            m.ShowDialog();
            if (Configuration == null)
            {
                Configuration = new Doa7SegConfiguration();
               
            }
            Configuration.DisplayModeConfiguration.Add(m.ModeConfiguration);
            RefreshGrid();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_selectedConfig != null && Configuration.DisplayModeConfiguration.Contains(_selectedConfig))
            {
                Configuration.DisplayModeConfiguration.Remove(_selectedConfig);

            }

            RefreshGrid();
        }
       

        private void dataViewOutput_SelectionChanged(object sender, EventArgs e)
        {
           
            try
            {
                var row = (dataViewOutput.SelectedRows[0].DataBoundItem as SegmentDisplayConfig);
                if (row != null)
                {
                    _selectedConfig = row;

                }

            }
            catch (Exception exception)
            {
                _selectedConfig = null;
            }
        }
    }
}
