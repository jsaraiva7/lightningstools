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
using F4Utils.Process;
using F4Utils.SimSupport.Configuration.CallBackConfig;
using GlobalConfiguration;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace SimLinkupV2.Ui.Options
{
    /// <summary>
    /// Lógica interna para Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private F4Config _cfg;
        public Options()
        {
            try
            {
               
                _cfg = F4Config.GetConfig();
                InitializeComponent();
                PopulateUi();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
       
            }
           
        }

        private void PopulateUi()
        {
           
            CallSignDir.Text = _cfg.Callsign;
            tbSsmDir.Text = _cfg.KeyFile;
            tbF4Dir.Text = _cfg.F4ExeFile;


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

        private void BtnCallSign_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(true);
            if (folder.Length > 2)
            {
                _cfg.Callsign = folder;
                CallSignDir.Text = folder;
            }
        }

        private void BtnKeyFile_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(false);
            if (folder.Length > 2)
            {
                _cfg.KeyFile = folder;
                tbSsmDir.Text = folder;
                
            }
        }
        private void Btnf4Dir_OnClick(object sender, RoutedEventArgs e)
        {
            string folder = GetFolderDir(true);
            if (folder.Length > 2)
            {
                _cfg.F4ExeFile = folder;
                tbF4Dir.Text = folder;
                try
                {
                    var file = KeyFileUtils.GetCurrentKeyFile(_cfg.F4ExeFile);
                    tbSsmDir.Text = file.FileName;
                    _cfg.KeyFile = file.FileName;
                    var call = CallsignUtils.DetectCurrentCallsign(_cfg.F4ExeFile);
                     
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show(exception.Message.ToString());

                }

            }

           
        }

        private string GetFolderDir(bool isHsm)
        { 

            var dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dlg.Title = "Select File";
            dlg.IsFolderPicker = false;
            if(isHsm)
            dlg.InitialDirectory = _cfg.F4ExeFile;
            else
                dlg.InitialDirectory = _cfg.F4ExeFile;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = true;
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

         

        
    }
}
