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
using SimLinkup.UI.UserControls;

namespace Mapper.UI.AddMapping
{
    public partial class SignalSelect : Form
    {
        
        public static SimLinkup.Runtime.Runtime SharedRuntime { get; set; }
        public Signal SelectedSignal { get; set; }
        private SignalsView signalsView1;
        public SignalSelect(SimLinkup.Runtime.Runtime runtime)
        {
            InitializeComponent();

            //SignalsView UI from Simlinkup
            signalsView1 = new SignalsView();
            this.Controls.Add(signalsView1);



            SharedRuntime = runtime;
            signalsView1.ScriptingContext = SharedRuntime.ScriptingContext;
        }

       

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectedSignal = signalsView1.SelectedSignal;
            this.Close();
        }

        private void signalsView1_Load(object sender, EventArgs e)
        {

        }

        private void SignalSelect_Load(object sender, EventArgs e)
        {
            PopulateSignalsView();
        }
        private void PopulateSignalsView()
        {
            signalsView1.ScriptingContext = SharedRuntime.ScriptingContext;
            signalsView1.Signals = SharedRuntime.ScriptingContext.AllSignals;
            signalsView1.UpdateContents();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectedSignal = signalsView1.SelectedSignal;
            if (SelectedSignal != null && SelectedSignal is DigitalSignal)
            {
                var digi = SelectedSignal as DigitalSignal;
                digi.State = !digi.State;
            }
        }
    }
}
