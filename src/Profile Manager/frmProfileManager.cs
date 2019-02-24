using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.HardwareSupport;
using Common.SimSupport;

namespace Profile_Manager
{
    public partial class frmProfileManager : Form
    {
        public frmProfileManager()
        {
            InitializeComponent();
            dataGridView1.DataSource = HSM;
        }

        private string _profileFile { get; set; }

        private List<IHardwareSupportModule> HSM { get; set; } = new List<IHardwareSupportModule>();

        public static IHardwareSupportModule[] GetRegisteredHardwareSupportModules(string profile)
        {
            //get a list of hardware support modules that are currently registered
            var hsmRegistry =
                HardwareSupportModuleRegistry.Load(
                    Path.Combine(Util.CurrentMappingProfileDirectoryFromFilename(profile), "HardwareSupportModule.registry"));

            var modules = hsmRegistry.GetInstances();
            return modules?.ToArray();
        }

        public static SimSupportModule[] GetRegisteredSimSupportModules(string profile)
        {
            //get a list of sim support modules that are currently registered
            var ssmRegistry =
                SimSupportModuleRegistry.Load(Path.Combine(Util.CurrentMappingProfileDirectoryFromFilename(profile),
                    "SimSupportModule.registry"));
            var modules = ssmRegistry.GetInstances();
            return modules?.ToArray();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".profile";
            dialog.DereferenceLinks = true;
            dialog.Filter = "SimLinkup Profile Files(*.profile)|*.profile";
            dialog.FilterIndex = 0;
            dialog.InitialDirectory = new FileInfo(Application.ExecutablePath).Directory.FullName;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.ShowHelp = false;
            dialog.ShowReadOnly = false;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Open Profile File";
            dialog.ValidateNames = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                try
                {
                    _profileFile = dialog.SafeFileName;
                    HSM = GetRegisteredHardwareSupportModules(_profileFile).ToList();

                }
                catch (Exception exception)
                {
                     
                    throw;
                }
            }
            dataGridView1.Refresh();
            dataGridView1.DataSource = HSM;
        }

        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".dll";
            dialog.DereferenceLinks = true;
            dialog.Filter = "DLL Files(*.dll)|*.dll";
            dialog.FilterIndex = 0;
            dialog.InitialDirectory = new FileInfo(Application.ExecutablePath).Directory.FullName;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.ShowHelp = false;
            dialog.ShowReadOnly = false;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Open SimLinkup Module Files";
            dialog.ValidateNames = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                try
                {

                    Assembly assembly = Assembly.LoadFrom(dialog.FileName);
                    var tst = assembly.ToString();
                    var hsmType = Type.GetType(dialog.FileName);
                }
                catch (Exception exception)
                {

                    throw;
                }
            }
            dataGridView1.Refresh();
            dataGridView1.DataSource = HSM;
        }

        private void removeModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
