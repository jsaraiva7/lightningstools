using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.MacroProgramming;
using log4net;
using Mapper.Models.Mapping;
using Phcc.DeviceManager.UI;
using SimLinkup.Runtime;
using SimLinkup.Signals;
using SignalMapping = Mapper.Models.Mapping.SignalMapping;

namespace Mapper
{
    public partial class Mapper : Form
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(Form));
        private Models.Mapping.MappingProfile _profile { get; set; }
        private SignalMapping _selectedMapping;
      
        
        public static SimLinkup.Runtime.Runtime SharedRuntime { get; set; }

        public Mapper()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "SimLinkup Mapping files (*.mapping)|*.mapping";
            saveFileDialog1.DefaultExt = "mapping";
            saveFileDialog1.AddExtension = true;

            openFileDialog1.Filter = "SimLinkup Mapping files (*.mapping)|*.mapping";
            openFileDialog1.DefaultExt = "mapping";
            openFileDialog1.AddExtension = true;
            _profile = new global::Mapper.Models.Mapping.MappingProfile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SharedRuntime = new SimLinkup.Runtime.Runtime();
 
        }

        private void newMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _profile.SignalMappings = new List<SignalMapping>();
            RefreshMappings();
        }

        private void RefreshMappings()
        {
            _mappingDisplay.DataSource = MappingModel.GetModel(_profile);
            _mappingDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            _mappingDisplay.Refresh();
        }
        private void loadMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    _profile = global::Mapper.Models.Mapping.MappingProfile.Load(openFileDialog1.FileName);
                    RefreshMappings();
                }
                catch (Exception exception)
                {
                    _log.Error(exception);
                }
            }
        }

        private void saveMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_profile.SignalMappings.Count > 0)
            {
                
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    try
                    {
                        _profile.Save(saveFileDialog1.FileName);
                    }
                    catch (Exception exception)
                    {
                        _log.Error(exception);
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Current Mapping is Empty!");
            }
            
        }

        private void _mappingDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addMappingForm f = new addMappingForm(SharedRuntime, _selectedMapping);
            f.ShowDialog();
            _profile.SignalMappings.Add(f.Mapping);
            f.Dispose();
            RefreshMappings();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignalExporter.Export(SharedRuntime.ScriptingContext.AllSignals);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void _mappingDisplay_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = (_mappingDisplay.SelectedRows[0].DataBoundItem as MappingModel);
                if (row != null)
                {
                    _selectedMapping = _profile.SignalMappings.FirstOrDefault(x =>
                        x.Source.Id == row.FriendlySourceName && x.Destination.Id == row.FriendlyDestinationName);

                }

            }
            catch (Exception exception)
            {
                _selectedMapping = null;
            }

        }
    }
}
