using System.Collections.Generic;

namespace PhccSupportModule.ConfigUi
{
    /// <summary>
    /// Interação lógica para ComPort.xam
    /// </summary>
    public partial class ComPort : UserControl
    {
        private Window _parent;
        public string Port { get; set; }
        public string FriendlyName { get; set; }
        public bool Cancel { get; set; } = false;
        public ComPort(Window parent)
        {
            InitializeComponent();
            _parent = parent;
            AddComPortsToList();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            Port = cbComPort.Text;
            FriendlyName = tbName.Text;
            if (Port.Length < 2)
            {
                MessageBox.Show("Please select a valid COM Port!");
                return;
            }
            if (_parent != null)
            {
                _parent.Close();
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Cancel = true;
            if (_parent != null)
            {
                _parent.Close();
            }
        }

        private void AddComPortsToList()
        {
            var ports = EnumerateSerialPorts();
            
            IComparer<string> comparer = new NumericComparer();
            ports.Sort(comparer);
            cbComPort.Items.Clear();
            foreach (var portName in ports)
            {
                cbComPort.Items.Add(portName);
            }
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

        private void CbComPort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbName.Text.Length < 1)
            {
                tbName.Text = "PHCC - " + cbComPort.SelectedItem.ToString();
            }
            else
            {
                tbName.Text = tbName.Text + " - " + cbComPort.Text;
            }
        }
    }
}
