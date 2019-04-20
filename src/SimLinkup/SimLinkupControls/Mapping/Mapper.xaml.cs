using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Common.Excel;
using Common.SimLinkup.Configuration;
using Common.SimLinkup.Runtime;
using OfficeOpenXml;
using SimLinkupControls.Mapping.Models;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace SimLinkupControls.Mapping
{
    /// <summary>
    /// Interação lógica para Mapper.xam
    /// </summary>
    public partial class Mapper : UserControl
    {

        private MappingProfile _profile { get; set; }
        private SignalMapping _selectedMapping;
        public Runtime SharedRuntime { get; set; }
        public Window _window { get; private set; }
        private SignalList _signalList;

        private System.Windows.Forms.SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private System.Windows.Forms.OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private System.Windows.Forms.SaveFileDialog dlgSaveExcel = new SaveFileDialog();

        public Mapper(Window w, Runtime runtime, SignalList lst)
        {
            _signalList = lst;
            SharedRuntime = runtime;
            _window = w;
            _window.Loaded += Form1_Load;
            _window.Closing += Window_Closing;
            InitializeComponent();
            saveFileDialog1.Filter = "SimLinkup Mapping files (*.mapping)|*.mapping";
            saveFileDialog1.DefaultExt = "mapping";
            saveFileDialog1.AddExtension = true;

            openFileDialog1.Filter = "SimLinkup Mapping files (*.mapping)|*.mapping";
            openFileDialog1.DefaultExt = "mapping";
            openFileDialog1.AddExtension = true;
            _profile = new MappingProfile();
            BuildTreeView();
        }

        #region uiMnuHandlers

        private void Form1_Load(object sender, EventArgs e)
        {


            //foreach (var map in SharedRuntime.Mappings)
            //{
            //    SignalMapping m = new SignalMapping();
            //    if (map.Source is Common.MacroProgramming.AnalogSignal)
            //    {
            //        m.Source = new AnalogSignal() { Id = map.Source.Id };
            //    }
            //    else if (map.Source is Common.MacroProgramming.DigitalSignal)
            //    {
            //        m.Source = new DigitalSignal() { Id = map.Source.Id };
            //    }
            //    else if (map.Source is Common.MacroProgramming.TextSignal)
            //    {
            //        m.Source = new TextSignal() { Id = map.Source.Id };
            //    }
            //    if (map.Destination is Common.MacroProgramming.AnalogSignal)
            //    {
            //        m.Destination = new AnalogSignal() { Id = map.Destination.Id };
            //    }
            //    else if (map.Destination is Common.MacroProgramming.DigitalSignal)
            //    {
            //        m.Destination = new DigitalSignal() { Id = map.Destination.Id };
            //    }
            //    else if (map.Destination is Common.MacroProgramming.TextSignal)
            //    {
            //        m.Destination = new TextSignal() { Id = map.Destination.Id };
            //    }
            //    _profile.SignalMappings.Add(m);
            //    RefreshMappings();
            //}

        }
        private void Window_Closing(object sender, EventArgs e)
        {
            if (SharedRuntime == null)
                SharedRuntime = new Runtime();
            _signalList.btnSelectSignal.Visibility = Visibility.Hidden;

        }

        private void MnuNew_OnClick(object sender, RoutedEventArgs e)
        {
            if (_profile.SignalMappings.Any())
            {
                var result =
                    MessageBox.Show("This will delete your current Mapping. Are you sure? ",
                        "", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    SharedRuntime = new Runtime();
                    _profile.SignalMappings = new List<SignalMapping>();
                    RefreshMappings();
                }
            }
        }

        private void MnuOpen_OnClick(object sender, RoutedEventArgs e)
        {
            if (_profile.SignalMappings.Any())
            {
                var result2 =
                    MessageBox.Show("This will delete your current Mapping. Are you sure? ",
                        "", MessageBoxButtons.OKCancel);
                if (result2 == DialogResult.OK)
                {
                    if (SharedRuntime != null)
                        SharedRuntime = new Runtime();
                    DialogResult result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK) // Test result.
                    {
                        LoadMapping(openFileDialog1.FileName);
                    }
                }

            }
            else
            {
                if (SharedRuntime != null)
                    SharedRuntime = new Runtime();
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    LoadMapping(openFileDialog1.FileName);
                }
            }


        }


        private void MnuSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_profile.SignalMappings.Count > 0)
                {

                    DialogResult result = saveFileDialog1.ShowDialog();
                    if (result == DialogResult.OK) // Test result.
                    {
                        //ValidateProfile();
                        _profile.Save(saveFileDialog1.FileName);
                    }
                }
                else
                {
                    MessageBox.Show("Current Mapping is Empty!");
                }


            }
            catch (Exception exception)
            {
                // _log.Error(exception);
            }

            BuildTreeView();
        }

        private void MnuClose_OnClick(object sender, RoutedEventArgs e)
        {
            var result2 =
                MessageBox.Show("Are you sure you saved your mapping? ",
                    "", MessageBoxButtons.OKCancel);
            if (result2 == DialogResult.OK)
            {
                _window.Close();
            }
        }

        private void MnuAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var w = new Window();
            var p = new AddMapping(w, SharedRuntime.ScriptingContext, _signalList);
            w.Content = p;
            w.Title = "Mapping Creation";
            w.ShowDialog();




            if (_selectedMapping != null)
            {
                try
                {
                    _profile.SignalMappings.Remove(_selectedMapping);
                }
                catch (Exception exception)
                {
                    //_log.Error(exception);
                }

            }
            _profile.SignalMappings.Add(p.Mapping);

            RefreshMappings();
        }

        private void MnuRemove_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedMapping != null)
            {
                try
                {
                    _profile.SignalMappings.Remove(_selectedMapping);
                }
                catch (Exception exception)
                {
                    //_log.Error(exception);
                }

            }
            RefreshMappings();
        }

        private void MnuExportMapping_OnClick(object sender, RoutedEventArgs e)
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

        private void MnuExportSignals_OnClick(object sender, RoutedEventArgs e)
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

        #endregion







        private void BuildTreeView()
        {
            tvMapFiles.Items.Clear();
            var itemc = new TreeViewItem() { Header = "Mapping Files Present:" };
            var itemd = new TreeViewItem() {   Header = "Add New Mapping" };
            tvMapFiles.Items.Add(itemd);
            var cfg = SimLinkupConfig.GetConfig();
            if (cfg == null)
                return;
            var mappingFiles = new DirectoryInfo(cfg.MappingDir).GetFiles("*.mapping", SearchOption.AllDirectories);
            foreach (var mappingFile in mappingFiles)
            {
                var profileToLoad = mappingFile.FullName;
                if (!string.IsNullOrEmpty(profileToLoad) && !string.IsNullOrEmpty(profileToLoad.Trim()))
                {
                    var profile = MappingProfile.Load(profileToLoad);
                    if (profile != null)
                    {
                        var fileName = new FileInfo(profileToLoad);
                        if (fileName.Exists)
                        {
                            var mdl = new MapProfileModel()
                            {
                                Count = profile.SignalMappings.Count, Name = fileName.Name.Replace(".mapping", ""),
                                File = profileToLoad
                            };
                            var item = new TreeViewItem() {Name = mdl.Name, DataContext = mdl, Header = mdl.ToString()};
                            tvMapFiles.Items.Add(item);
                        }

                       
                    }
                    
                }
            }
            
        }

        ///RefreshUi

        private void RefreshMappings()
        {
            _mappingDisplay.ItemsSource = MappingModel.GetModel(_profile);
            _mappingDisplay.AutoGenerateColumns = true;
        }

        private void LoadMapping(string file)
        {
            try
            {
                _profile = MappingProfile.Load(file);
                var prof = Common.SimLinkup.Signals.MappingProfile.Load(file);
                foreach (var profile in prof.SignalMappings)
                {
                    SharedRuntime.Mappings.ToList().Remove(profile);
                }
                RefreshMappings();
                _window.Title = new FileInfo(openFileDialog1.FileName)?.Name;
            }
            catch (Exception exception)
            {
                //_log.Error(exception);
            }
        }

        private void _mappingDisplay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_mappingDisplay.SelectedItem is MappingModel row)
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
        private void ValidateProfile()
        {
            var error = false;
            var validation = ValidateMappingList(_profile.SignalMappings, out error);

            if (error)
            {
                MessageBox.Show(validation);
            }
            else
            {
                MessageBox.Show("Mapping Configuration appears to be OK!");
            }
        }

        private bool ValidateMapping(SignalMapping mapping)
        {
            if (mapping.Source.SignalType == mapping.Destination.SignalType)
            {
                return true;
            }


            return false;
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
                        x.Source.Id.Equals(mapping.Source.Id) && x.Destination.Id.Equals(mapping.Destination.Id)) > 1)
                {
                    sb.Append("ERROR!!  Profile aleready defines a mapping: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                }
                else if (SharedRuntime.Mappings.Any(x =>
                    x.Source.Id.Equals(mapping.Source.Id)) || _profile.SignalMappings.Count(x =>
                    x.Source.Id.Equals(mapping.Source.Id)) > 1)
                {
                    sb.Append("WARNING!!  Profile aleready defines a mapping with the same source: " + mapping.Source.Id + " ---" + mapping.Destination.Id + "\n");
                    error = true;
                }
                else if (SharedRuntime.Mappings.Any(x =>
                      x.Destination.Id.Equals(mapping.Destination.Id)) || _profile.SignalMappings.Count(x =>
                      x.Destination.Id.Equals(mapping.Destination.Id)) > 1)
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

        private void TvMapFiles_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = tvMapFiles.SelectedItem as TreeViewItem;
            if (item != null)
            {
                if (item.Header != null && (string) item.Header == $"Add New Mapping")
                {
                    var w = new Window();
                    var p = new AddMapping(w, SharedRuntime.ScriptingContext, _signalList);
                    w.Content = p;
                    w.Title = "Mapping Creation";
                    w.ShowDialog();




                    if (_selectedMapping != null)
                    {
                        try
                        {
                            _profile.SignalMappings.Remove(_selectedMapping);
                        }
                        catch (Exception exception)
                        {
                            //_log.Error(exception);
                        }

                    }
                    _profile.SignalMappings.Add(p.Mapping);

                    RefreshMappings();
                    return;
                }

                else if (item.DataContext != null && item.DataContext is MapProfileModel)
                {
                    var ctx = item.DataContext as MapProfileModel;
                    if (ctx != null)
                    {
                        LoadMapping(ctx.File);
                    }
                }
                
            }
        }
    }
    class MapExportModel
    {

        public string SourceId { get; set; }

        public string DestinationId { get; set; }
    }

    class MapProfileModel
    {
        public string File { get; set; } = "";
        public string Name { get; set; } = "";
        public int Count { get; set; }

        public override string ToString()
        {
            return Name.Replace(".mapping", "" ) + "  - Items: " + Count;
        }
    }
}
