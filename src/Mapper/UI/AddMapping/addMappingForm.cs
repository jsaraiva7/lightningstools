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
using Mapper.Models.Mapping;
using Mapper.UI.AddMapping;
using SimLinkup.UI;
using SimLinkup.UI.UserControls;

namespace Mapper
{
    public partial class addMappingForm : Form
    {
        public static SimLinkup.Runtime.Runtime SharedRuntime { get; set; }
        private Signal Input { get; set; }
        private Signal Output { get; set; }
        public SignalMapping Mapping { get; set; }
        public addMappingForm(SimLinkup.Runtime.Runtime runtime)
        {
            InitializeComponent();
            SharedRuntime = runtime;
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            SignalSelect s = new SignalSelect(SharedRuntime);
            s.ShowDialog();
            Input = s.SelectedSignal;
            if (Input != null)
            {
                lblIn.Text = Input.Id;
            }
            s.Dispose();

        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            SignalSelect s = new SignalSelect(SharedRuntime);
            s.ShowDialog();
            Output = s.SelectedSignal;
            if (Output != null)
            {
                lblOut.Text = Output.Id;
            }
            s.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Input == null || Output == null)
            {
                MessageBox.Show("Signal mapping is invalid. Please correct.");
            }


            SignalMapping m = new SignalMapping();
            if (Input is AnalogSignal)
            {
                m.Source = new Models.AnalogSignal() {Id = Input.Id};
            }
            else if (Input is DigitalSignal)
            {
                m.Source = new Models.DigitalSignal() { Id = Input.Id };
            }
            else if (Input is TextSignal)
            {
                m.Source = new Models.TextSignal() { Id = Input.Id };
            }
            if (Output is AnalogSignal)
            {
                m.Destination = new Models.AnalogSignal() { Id = Output.Id };
            }
            else if (Output is DigitalSignal)
            {
                m.Destination = new Models.DigitalSignal() { Id = Output.Id };
            }
            else if (Output is TextSignal)
            {
                m.Destination = new Models.TextSignal() { Id = Output.Id };
            }
            Mapping = m;
            this.Close();
        }
    }
}
