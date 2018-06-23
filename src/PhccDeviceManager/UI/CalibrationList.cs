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
    public partial class CalibrationList : Form
    {
        private AnalogSignal SignalSelected;
        private OutputConfig config;

        private CalibrationPoint CalibrationSelected;
        public CalibrationList(AnalogSignal signal, OutputConfig config)
        {
            InitializeComponent();
            SignalSelected = signal;
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
            frmCalibrateSignal s = new frmCalibrateSignal(SignalSelected);
            s.ShowDialog();
            var point = new CalibrationPoint(){Input = s.InputValue, Output = s.CalibrationOffset};
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
}
