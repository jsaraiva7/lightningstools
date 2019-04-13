using System;
using System.Collections.Generic;
using System.Linq;
using PhccSupportModule.HsmSupport.Peripherals;

namespace PhccSupportModule.ConfigUi.OldForms
{
    public partial class CalibrationSelect : Form
    {
        private List<Signal> _signals;
        private List<DigitalSignal> _digitalInputs;
        private BasePeripheral _peripheral;

        public Signal SignalSelected { get; set; }
        
        public List<OutputConfig> OutputConfigs { get; set; }= new List<OutputConfig>();


        public CalibrationSelect(List<Signal> signals, List<DigitalSignal> digitalIns, BasePeripheral peripheral)
        {
            InitializeComponent();
            _signals = signals.Distinct().ToList();
            _digitalInputs = digitalIns;
            _peripheral = peripheral;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CalibrationSelect_Load(object sender, EventArgs e)
        {
            if (_peripheral is HsmDoaStepper)
            {
                var r = _peripheral as HsmDoaStepper;
                OutputConfigs = r.OutputConfigs;
                
            }
            else if (_peripheral is HsmDoa8Servo)
            {
                var r = _peripheral as HsmDoa8Servo;
                OutputConfigs = r.OutputConfigs;
            }
            else if (_peripheral is HsmDoaAirCore)
            {
                var r = _peripheral as HsmDoaAirCore;
                OutputConfigs = r.OutputConfigs;
            }
            else if (_peripheral is HsmDoaAnOut1)
            {
                var r = _peripheral as HsmDoaAnOut1;
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
