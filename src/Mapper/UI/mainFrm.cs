using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Excel;
using log4net;
using Mapper.Models.Mapping;
using Mapper.UI.AddMapping;
using OfficeOpenXml;
using SignalMapping = Mapper.Models.Mapping.SignalMapping;

namespace Mapper.UI
{
    public partial class Mapper : Form
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(Form));
        private Models.Mapping.MappingProfile _profile { get; set; }
        private SignalMapping _selectedMapping;
       
        //f
        public SimLinkup.Runtime.Runtime SharedRuntime { get; set; }

        public Mapper()
        {
            InitializeComponent();

#if DEBUG
            testToolStripMenuItem.Visible = true;
#else
            testToolStripMenuItem.Visible = true;
#endif
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
            if(SharedRuntime != null)
            SharedRuntime = new SimLinkup.Runtime.Runtime();
 
        }

        private void newMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_profile.SignalMappings.Any())
            {
                var result =
                    MessageBox.Show("This will delete your current Mapping. Are you sure? ",
                        "", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    SharedRuntime = new SimLinkup.Runtime.Runtime();
                    _profile.SignalMappings = new List<SignalMapping>();
                    RefreshMappings();
                }
            }
        }

        private void RefreshMappings()
        {
            _mappingDisplay.DataSource = MappingModel.GetModel(_profile);
            _mappingDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            _mappingDisplay.Refresh();
        }
        private void loadMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_profile.SignalMappings.Any())
            {
                var result2 =
                    MessageBox.Show("This will delete your current Mapping. Are you sure? ",
                        "", MessageBoxButtons.OKCancel);
                if (result2 == DialogResult.OK)
                {
                    if (SharedRuntime != null)
                        SharedRuntime = new SimLinkup.Runtime.Runtime();
                    DialogResult result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK) // Test result.
                    {
                        LoadMapping();
                    }
                }
                
            }
            else
            {
                if (SharedRuntime != null)
                    SharedRuntime = new SimLinkup.Runtime.Runtime();
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    LoadMapping();
                }
            }
            
           
        }

        private void LoadMapping()
        {
            try
            {
                _profile = global::Mapper.Models.Mapping.MappingProfile.Load(openFileDialog1.FileName);
                var prof = SimLinkup.Signals.MappingProfile.Load(openFileDialog1.FileName);
                foreach (var profile in prof.SignalMappings)
                {
                    SharedRuntime.Mappings.ToList().Remove(profile);
                }
                RefreshMappings();
                this.Text = new FileInfo(openFileDialog1.FileName)?.Name;
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }
        private void saveMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_profile.SignalMappings.Count > 0)
            {
                
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    SaveMapping();
                }
            }
            else
            {
                MessageBox.Show("Current Mapping is Empty!");
            }
            if (SharedRuntime != null)
                SharedRuntime = new SimLinkup.Runtime.Runtime();
        }

        private void SaveMapping()
        {
            try
            {
                ValidateProfile();
                _profile.Save(saveFileDialog1.FileName);
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }

        private void _mappingDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addMappingForm f = new addMappingForm(SharedRuntime, _selectedMapping);
            f.ShowDialog();
            if (_selectedMapping != null)
            {
                try
                {
                    _profile.SignalMappings.Remove(_selectedMapping);
                }
                catch (Exception exception)
                {
                    _log.Error(exception);
                }
                
            }
            _profile.SignalMappings.Add(f.Mapping);
            f.Dispose();
            RefreshMappings();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // SignalExporter.Export(SharedRuntime.ScriptingContext.AllSignals);

            var falconSignals =
                SharedRuntime.ScriptingContext.AnalogSignals.Where(X =>
                    X.SourceFriendlyName.ToLower().Contains("falcon")).ToList();

            var otherSignals = SharedRuntime.ScriptingContext.AnalogSignals.Where(X =>
                !X.SourceFriendlyName.ToLower().Contains("falcon")).Where(x=> !x.Id.ToLower().Contains("inputs")).ToList();

            if (falconSignals.Count > otherSignals.Count)
            {
                int i = 0;
                foreach (var analogSignal in otherSignals)
                {
                    var m = new SignalMapping();
                    var source = new AnalogSignal();
                    var f4 = falconSignals[i];
                    source.Id = f4.Id;
                    var dest = new AnalogSignal();
                    dest.Id = analogSignal.Id;
                    m.Source = source;
                    m.Destination = dest;
                    _profile.SignalMappings.Add(m);
                    i++;

                }

            }
            else
            {
                int i = 0;
                foreach (var analogSignal in falconSignals)
                {
                    var m = new SignalMapping();
                    var source = new AnalogSignal();
                    var f4 = otherSignals[i];
                    source.Id = analogSignal.Id;
                    var dest = new AnalogSignal();
                    dest.Id =  f4.Id;
                    m.Source = source;
                    m.Destination = dest;
                    _profile.SignalMappings.Add(m);
                    i++;

                }
            }

            RefreshMappings();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedMapping != null)
            {
                try
                {
                    _profile.SignalMappings.Remove(_selectedMapping);
                }
                catch (Exception exception)
                {
                    _log.Error(exception);
                }
              
            }
            RefreshMappings();
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

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidateProfile();
        }

        private void ValidateProfile()
        {
            var error = false;
            var validation = ValidateMappingList(_profile.SignalMappings, out error);

            if (error)
            {
                new MappingError(validation).ShowDialog();
            }
            else
            {
                new MappingError("Mapping Configuration appears to be OK!").ShowDialog();
            }
        }

        private string ValidateMappingList(List<SignalMapping> mapList, out bool error) 
        {

            error = false;
            StringBuilder sb = new StringBuilder();
            foreach (var mapping in mapList)
            {
                
                if (!ValidateMapping(mapping))
                {
                    sb.Append("ERROR!!  Invald Signal Type on: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                }
                else if (SharedRuntime.Mappings.Any(x =>
                    x.Source.Id.Equals(mapping.Source.Id) && x.Destination.Id.Equals(mapping.Destination.Id)) || _profile.SignalMappings.Count(x =>
                        x.Source.Id.Equals(mapping.Source.Id) && x.Destination.Id.Equals(mapping.Destination.Id)) >1 )
                {
                    sb.Append("ERROR!!  Profile aleready defines a mapping: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                }
                else if (SharedRuntime.Mappings.Any(x =>
                    x.Source.Id.Equals(mapping.Source.Id)) || _profile.SignalMappings.Count(x =>
                    x.Source.Id.Equals(mapping.Source.Id) ) >1 )
                {
                    sb.Append("WARNING!!  Profile aleready defines a mapping with the same source: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                }
                else if (SharedRuntime.Mappings.Any(x =>
                      x.Destination.Id.Equals(mapping.Destination.Id)) || _profile.SignalMappings.Count(x =>
                      x.Destination.Id.Equals(mapping.Destination.Id))>1)
                {
                    sb.Append("ERROR!!  Profile aleready defines a mapping with the same destination: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                } 
                
            }
            if (_profile.SignalMappings.Count == 0)
            {
                sb.Append("No Mapping was defined. Please create somme mappings and then run the validation tool. \n");
                error = true;
            }

            return sb.ToString();
        }


        /// <summary>
        /// validates a mapping
        /// </summary>
        /// <param name="mapping"> Mapping to be validated</param>
        /// <returns>bool mapping valid. </returns>
        private bool ValidateMapping(SignalMapping mapping)
        {
            if (mapping.Source.SignalType == mapping.Destination.SignalType)
            {
                return true;
            }
           

            return false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result2 =
                MessageBox.Show("Are you sure you saved your mapping? ",
                    "", MessageBoxButtons.OKCancel);
            if (result2 == DialogResult.OK)
            {
                Application.Exit();
            }
         }

        private void exportMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSaveExcel.Title = "Select a filename";

            dlgSaveExcel.DefaultExt = "xlsx";
            dlgSaveExcel.AddExtension = true;

            if (dlgSaveExcel.ShowDialog() == DialogResult.OK)
            {


                var mapModel = _profile.SignalMappings.Select(c => new MapExportModel
                {
                    SourceId = c.Source.Id,
                    DestinationId = c.Destination.Id
                }).ToList();
                FileInfo f = new FileInfo(dlgSaveExcel.FileName);

                if (File.Exists(dlgSaveExcel.FileName))
                {
                    File.Delete(dlgSaveExcel.FileName);
                }


                ExcelPackage pkg = new ExcelPackage(f);

                Export.ListToExcel(mapModel, pkg,
                    "Signal Mappings");
                pkg.Save();
            }
        }

        private void exportAllSignalsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dlgSaveExcel.Title = "Select a filename";

            dlgSaveExcel.DefaultExt = "xlsx";
            dlgSaveExcel.AddExtension = true;



            if (dlgSaveExcel.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(dlgSaveExcel.FileName);

                if (File.Exists(dlgSaveExcel.FileName))
                {
                    File.Delete(dlgSaveExcel.FileName);
                }


                ExcelPackage pkg = new ExcelPackage(f);

                Export.ListToExcel(SharedRuntime.ScriptingContext.AllSignals, pkg,
                    "All Signals");
                pkg.Save();
            }
        }
    }

    class MapExportModel
    {
   
        public string SourceId { get; set; }
    
        public string DestinationId { get; set; }
    }
}
