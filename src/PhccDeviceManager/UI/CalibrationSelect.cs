using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.MacroProgramming;
using PhccConfiguration.Config;

namespace Phcc.DeviceManager.UI
{
    public partial class CalibrationSelect : Form
    {
        private List<AnalogSignal> _signals;

        private AnalogSignal SignalSelected;
        
        public List<OutputConfig> OutputConfigs { get; set; }= new List<OutputConfig>();


        public CalibrationSelect(List<AnalogSignal> signals)
        {
            InitializeComponent();
            _signals = signals;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CalibrationSelect_Load(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var signalList = _signals.Select(x => new GridDisplayModel()
            {
                SubSource = x.SubSourceFriendlyName,
                FriendlyName = x.FriendlyName,
                Id = x.Id,
                SourceDevice = x.SourceFriendlyName
            }).ToList();
            dataGridView1.DataSource = signalList;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Refresh();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = (dataGridView1.SelectedRows[0].DataBoundItem as GridDisplayModel);
                SignalSelected = _signals.FirstOrDefault(x => x.Id == row.Id);
                var tst = SignalSelected as CalibratedAnalogSignal;
            }
            catch (Exception exception)
            {
                SignalSelected = null;
            }
            
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            if (SignalSelected != null)
            {
                var cfg = OutputConfigs.FirstOrDefault(x => x.SignalId == SignalSelected.Id);
                if (cfg == null)
                {
                    cfg = new OutputConfig() {SignalId = SignalSelected.Id};
                }


              
                CalibrationList l = new CalibrationList(SignalSelected, cfg);
                l.ShowDialog();
                OutputConfigs.Add(cfg);
                l.Dispose();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (SignalSelected != null)
            {
                
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class GridDisplayModel
    {
        public string SourceDevice { get; set; }
        public string SubSource { get; set; }
        public string FriendlyName { get; set; }
        public string Id { get; set; }
    }
}
