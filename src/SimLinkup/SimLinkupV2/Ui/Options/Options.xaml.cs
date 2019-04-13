using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common.SimLinkup.Configuration;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SimLinkupV2.Ui.Options
{
    /// <summary>
    /// Lógica interna para Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private Common.SimLinkup.Configuration.SimLinkupConfig _cfg;
        public Options()
        {
            InitializeComponent();
            _cfg = SimLinkupConfig.GetConfig();
            PopulateUi();
        }

        private void PopulateUi()
        {
            tbFrequency.Text = _cfg.DesiredFrequency.ToString();
            ckLaunchStartup.IsChecked = _cfg.LauchAtSystemStart;
            ckMinimizeTray.IsChecked = _cfg.MinimizeToTray;
            chAutoStart.IsChecked = _cfg.AutoStart;
            ckStartMinimized.IsChecked = _cfg.MinimizeStart;
            tbHsmDir.Text = _cfg.HsmDir;
            tbSsmDir.Text = _cfg.SsmDir;
            tbMappingDir.Text = _cfg.MappingDir;


        }



        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ckStartMinimized.IsChecked != null) _cfg.MinimizeStart = ckStartMinimized.IsChecked.Value;
                if (ckLaunchStartup.IsChecked != null) _cfg.LauchAtSystemStart = ckLaunchStartup.IsChecked.Value;
                if (ckMinimizeTray.IsChecked != null) _cfg.MinimizeToTray = ckMinimizeTray.IsChecked.Value;
                if (chAutoStart.IsChecked != null) _cfg.AutoStart = chAutoStart.IsChecked.Value;
                var freq = uint.Parse(tbFrequency.Text);
                _cfg.DesiredFrequency = freq;
           
                _cfg.SaveConfiguration();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error Detected!\nPlease review your configuration");
            }

            MessageBox.Show("Configuration Saved. Please restart SimLinkup!");

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLoadHsm_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(true);
            if (folder.Length > 2)
            {
                _cfg.HsmDir = folder;
                tbHsmDir.Text = folder;
            }
        }

        private void BtnLoadSsm_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(false);
            if (folder.Length > 2)
            {
                _cfg.SsmDir = folder;
                tbSsmDir.Text = folder;
                
            }
        }
        private void BtnLoadMapping_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(true);
            if (folder.Length > 2)
            {
                _cfg.MappingDir = folder;
                tbMappingDir.Text = folder;
            }
        }

        private string GetFolderDir(bool isHsm)
        { 

            var dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;
            if(isHsm)
            dlg.InitialDirectory = _cfg.HsmDir;
            else
                dlg.InitialDirectory = _cfg.SsmDir;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                .FileName);
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                return folder;
            }
            else
            {
                return string.Empty;
            }
        }

        private void TbFrequency_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsTextAllowed(tbFrequency.Text))
            {
                MessageBox.Show("Invalid Value! \nNumbers only!");
                tbFrequency.Text = "";
            }
  
 
        }

        
    }
}
