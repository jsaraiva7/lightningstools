using System;
using System.Linq;
using System.Windows.Forms;
using Common.MacroProgramming;
using Mapper.Models.Mapping;
using Mapper.UI.AddMapping;
using Phcc.DeviceManager.UI;

namespace Mapper
{
    public partial class addMappingForm : Form
    {
        public static SimLinkup.Runtime.Runtime SharedRuntime { get; set; }
        private Signal Input { get; set; }
        private Signal Output { get; set; }
        public SignalMapping Mapping { get; set; }


        public addMappingForm(SimLinkup.Runtime.Runtime runtime, SignalMapping mapping)
        {
            InitializeComponent();
            SharedRuntime = runtime;
            if (mapping != null)
            {
                Mapping = mapping;
                PopulateMapping(mapping);
            }
            
        }

        private void PopulateMapping(SignalMapping mapping)
        {
            lblIn.Text = SharedRuntime.ScriptingContext.AllSignals.FirstOrDefault(x => x.Id.Equals(mapping.Source.Id))
                ?.Id;
            lblOut.Text = SharedRuntime.ScriptingContext.AllSignals.FirstOrDefault(x => x.Id.Equals(mapping.Destination.Id))
                ?.Id;
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
