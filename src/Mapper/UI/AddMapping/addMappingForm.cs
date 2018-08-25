using System;
using System.Linq;
using System.Windows.Forms;
using Mapper.Models.Mapping;
using Signal = Common.MacroProgramming.Signal;

namespace Mapper.UI.AddMapping
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
            if (Input is Common.MacroProgramming.AnalogSignal)
            {
                m.Source = new AnalogSignal() {Id = Input.Id};
            }
            else if (Input is Common.MacroProgramming.DigitalSignal)
            {
                m.Source = new DigitalSignal() { Id = Input.Id };
            }
            else if (Input is Common.MacroProgramming.TextSignal)
            {
                m.Source = new TextSignal() { Id = Input.Id };
            }
            if (Output is Common.MacroProgramming.AnalogSignal)
            {
                m.Destination = new AnalogSignal() { Id = Output.Id };
            }
            else if (Output is Common.MacroProgramming.DigitalSignal)
            {
                m.Destination = new DigitalSignal() { Id = Output.Id };
            }
            else if (Output is Common.MacroProgramming.TextSignal)
            {
                m.Destination = new TextSignal() { Id = Output.Id };
            }
            Mapping = m;
            this.Close();
        }
    }
}
