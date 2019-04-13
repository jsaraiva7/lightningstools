using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.MacroProgramming;
using PhccConfiguration.Config;
using PhccConfiguration.Config.ConfigClasses;

namespace PhccHardwareSupportModule.ConfigManager.UI
{
    public partial class CalibrationSelect : Form
    {
        private List<Signal> _signals;
        private List<DigitalSignal> _digitalInputs;
        private Peripheral _peripheral;

        public Signal SignalSelected { get; set; }
        
        public List<OutputConfig> OutputConfigs { get; set; }= new List<OutputConfig>();


        public CalibrationSelect(List<Signal> signals, List<DigitalSignal> digitalIns, Peripheral peripheral)
        {
            InitializeComponent();
            _signals = signals;
            _digitalInputs = digitalIns;
            _peripheral = peripheral;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CalibrationSelect_Load(object sender, EventArgs e)
        {
            if (_peripheral is DoaStepper)
            {
                var r = _peripheral as DoaStepper;
                OutputConfigs = r.OutputConfigs;
                
            }
            else if (_peripheral is Doa8Servo)
            {
                var r = _peripheral as Doa8Servo;
                OutputConfigs = r.OutputConfigs;
            }
            else if (_peripheral is DoaAirCore)
            {
                var r = _peripheral as DoaAirCore;
                OutputConfigs = r.OutputConfigs;
            }
            else if (_peripheral is DoaAnOut1)
            {
                var r = _peripheral as DoaAnOut1;
                OutputConfigs = r.OutputConfigs;
            }
            Update();
        }

        private void Update()
        {
            
            var signalList = _signals.Select(x => new GridDisplayModel()
            {
                SubSource = x.CollectionName,
                FriendlyName = x.FriendlyName,
                Id = x.Id,
                SourceDevice = x.SourceFriendlyName
            }).ToList();
            dataGridView1.DataSource = signalList;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Refresh();
            dataGridView1.AllowUserToResizeColumns = true;
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


              
                CalibrationList l = new CalibrationList(SignalSelected, cfg, _digitalInputs, _peripheral);
                l.ShowDialog();
                OutputConfigs.Add(cfg);
                l.Dispose();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (SignalSelected != null)
            {
                OutputConfigs = new List<OutputConfig>();
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
