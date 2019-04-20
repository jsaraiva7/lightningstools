using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using F4Utils.Campaign;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace TestsApp
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected byte[] _rawBytes;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var file = GetFolderDir();
            try
            {
                var fi = new FileInfo(file);
                if (!fi.Exists) throw new FileNotFoundException(file);
                _rawBytes = new byte[fi.Length];
                var data = new F4CampaignFileBundleReader(file);
                var ssv = data.GetEmbeddedFileDirectory();
                foreach (var itm in ssv)
                {
                    if (itm.FileName.Contains("tea"))
                    {
                        using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                           TeaFile f = new TeaFile(fs, 68);
                           var zz = f.numTeams;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
        }


        private string GetFolderDir()
        {

            var dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dlg.Title = "Select File";
            dlg.IsFolderPicker = false;
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
