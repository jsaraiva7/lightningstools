using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.HardwareSupport.HardwareConfig.ConfigClasses;
using Common.MacroProgramming;
using PhccHsm.ConfigUi.OldForms.AnalogSignal;
using PhccHsm.HsmSupport.Peripherals;

namespace PhccHsm.ConfigUi.OldForms
{
    public partial class CalibrationList : Form
    {
        private Common.MacroProgramming.AnalogSignal SignalSelected;
        private OutputConfig config;
        private List<DigitalSignal> _digitalInputs;
        private BasePeripheral _peripheral;

        private CalibrationPoint CalibrationSelected;
        public CalibrationList(Signal signal, OutputConfig config, List<DigitalSignal> digitalIns, BasePeripheral peripheral)
        {

            InitializeComponent();
            _peripheral = peripheral;
            if (_peripheral is HsmDoaStepper)
            {
                btnHomingSignal.Visible = true;
                button1.Visible = true;
                if (peripheral is HsmDoaStepper per)
                {
                    var homeIn = per.HomingSignals.FirstOrDefault();
                    if (homeIn != null)
                    {
                        var homeInSignal = digitalIns.FirstOrDefault(c => c.Id == homeIn.ControlSignalId);
                        if (homeInSignal != null)
                        {
                            lblHomeInSignal.Text = homeInSignal.FriendlyName + " - " + homeInSignal.SourceFriendlyName;
                        }
                    }
                    

                }

            

                lblHomeInSignal.Visible = true;
            }
            else
            {
                btnHomingSignal.Visible = false;
                button1.Visible = false;
                lblHomeInSignal.Visible = false;
            }
            
            SignalSelected = signal as Common.MacroProgramming.AnalogSignal;
            _digitalInputs = digitalIns;
            this.config = config;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = config.CalibrationData.OrderBy(x=> x.Input).ToList();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Refresh();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = (dataGridView1.SelectedRows[0].DataBoundItem as CalibrationPoint);
                CalibrationSelected = config.CalibrationData.FirstOrDefault(x => x.Input == row.Input);
            }
            catch (Exception exception)
            {
                CalibrationSelected = null;
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

        private void btnDeleteCalibrationPoint_Click(object sender, EventArgs e)
        {
            if (CalibrationSelected != null)
            {
                config.CalibrationData.Remove(CalibrationSelected);
            }
            RefreshGrid();
        }

        private void btnAddCalibrationPoint_Click(object sender, EventArgs e)
        {
           

            if (CalibrationSelected != null)
            {
                frmCalibrateSignal s = new frmCalibrateSignal(SignalSelected, CalibrationSelected);
                s.ShowDialog();
                var point = s.Point;
                CalibrationSelected = point;
                RefreshGrid();
                s.Dispose();
            }
            else
            {
                frmCalibrateSignal s = new frmCalibrateSignal(SignalSelected, new CalibrationPoint());
                s.ShowDialog();
                var point = new CalibrationPoint() { Input = s.InputValue, Output = s.CalibrationOffset };
                if (config.CalibrationData.Any(x => x.Input == point.Input))
                {
                    var p = config.CalibrationData.FirstOrDefault(x => x.Input == point.Input);
                    p.Output = s.CalibrationOffset;
                }
                else
                {
                    config.CalibrationData.Add(point);
                }
                RefreshGrid();
                s.Dispose();
            }
          
        }

        private void btnHomingSignal_Click(object sender, EventArgs e)
        {
            var digitals = _digitalInputs.Select(x => x as Signal).ToList();

            CalibrationSelect p = new CalibrationSelect(digitals, null, null);
            p.btnCalibrate.Visible = false;
            p.button4.Visible = false;
            p.ShowDialog();
            var r = p.SignalSelected;


            var stepper = _peripheral as HsmDoaStepper;
            stepper.HomingSignals.Add(new HomingSignalConfig(){MotorSignalId = this.SignalSelected.Id, ControlSignalId = r.Id, State = true});
            lblHomeInSignal.Text = p.SignalSelected.FriendlyName;
            p.Dispose();
            MessageBox.Show("Home In Signal Configured");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var stepper = _peripheral as HsmDoaStepper;


                var signal = stepper.HomingSignals.FirstOrDefault(x => x.MotorSignalId == this.SignalSelected.Id);
                stepper.HomingSignals.Remove(signal);
                MessageBox.Show("Home In Signal removed");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
        }
    }
}
