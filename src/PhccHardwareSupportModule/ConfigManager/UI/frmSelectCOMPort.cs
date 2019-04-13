using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Strings;
using Microsoft.VisualBasic.Devices;

namespace PhccHardwareSupportModule.ConfigManager.UI
{
    public partial class frmSelectCOMPort : Form
    {
        private string _comPort;
        private List<string> _comPorts = new List<string>();
        private string _name;

        public frmSelectCOMPort()
        {
            InitializeComponent();
            _comPorts = EnumerateSerialPorts();
            txtFriendlyName.Text = Name;
        }

        public string COMPort
        {
            get => _comPort;
            set => _comPort = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                txtFriendlyName.Text = _name;
            }
        }

        public string FriendlyName { get; set; }

        public List<string> COMPorts
        {
            get => _comPorts;
            set => _comPorts = value;
        }

        private void SelectSuppliedComPortInList()
        {
            foreach (var item in cbComPort.Items)
            {
                if (item.ToString() == _comPort)
                {
                    cbComPort.SelectedItem = item;
                }
            }
        }

        private void UpdateComPort()
        {
            _comPort = cbComPort.SelectedItem != null ? cbComPort.SelectedItem.ToString() : null;
        }

        private void frmSelectCOMPort_Load(object sender, EventArgs e)
        {
            AddComPortsToList();
            EnableDisableOKButton();
        }

        private void AddComPortsToList()
        {
            if (_comPorts == null) return;
            IComparer<string> comparer = new NumericComparer();
            _comPorts.Sort(comparer);
            cbComPort.Items.Clear();
            foreach (var portName in _comPorts)
            {
                cbComPort.Items.Add(portName);
            }
            SelectSuppliedComPortInList();
        }

        private List<string> EnumerateSerialPorts()
        {
            var ports = new Ports();
            var toReturn = new List<string>();
            foreach (var portName in ports.SerialPortNames)
            {
                toReturn.Add(portName);
            }
            return toReturn;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            FriendlyName = txtFriendlyName.Text;
            DialogResult = DialogResult.OK;
            _name = txtFriendlyName.Text;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableOKButton();
            UpdateComPort();
        }

        private void EnableDisableOKButton()
        {
            var selectedText = cbComPort.SelectedItem != null ? cbComPort.SelectedItem.ToString() : null;
            cmdOk.Enabled = !(string.IsNullOrEmpty(selectedText));
        }
    }
}