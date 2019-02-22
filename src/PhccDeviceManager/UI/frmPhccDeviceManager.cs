using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Excel;
using Common.MacroProgramming;
using log4net;
using OfficeOpenXml;
using Phcc.DeviceManager.UI.DigitalConfig;
using Phcc.DeviceManager.UI.Doa7SegConfig;
using PhccConfiguration.Config;
 



namespace Phcc.DeviceManager.UI
{
    public partial class frmPhccDeviceManager : Form
    {
        #region Class variables

        private static readonly ILog _log = LogManager.GetLogger(typeof (frmPhccDeviceManager));
        private List<PhccHardwareSupportModule.Phcc.PhccHardwareSupportModule> _hsm;
        #endregion

        #region Instance variables

        private bool _configIsModified;
        private ConfigurationManager _configMgr = new ConfigurationManager();
        private string _currentConfigurationFilePath;

        #endregion

        #region Constructors

        public frmPhccDeviceManager()
        {
            InitializeComponent();

#if DEBUG
            testToolStripMenuItem.Visible = true;
#else
            testToolStripMenuItem.Visible = true;
#endif
        }

        #endregion

        private Motherboard GetSelectedMotherboard()
        {
            var currentNode = tvDevicesAndPeripherals.SelectedNode;

            while (currentNode != null)
            {
                var currentNodeData = currentNode.Tag;
                if (currentNodeData is Motherboard)
                {                 
                    return (Motherboard) currentNodeData;
                }
                else
                {
                    currentNode = currentNode.Parent;
                }
            }
            return null;
        }

        private void RemoveSelectedNode()
        {
            var currentNodeData = tvDevicesAndPeripherals.SelectedNode.Tag;
            if (currentNodeData is Motherboard)
            {
                _configMgr.Motherboards.Remove((Motherboard) currentNodeData);
                _configIsModified = true;
            }
            if (currentNodeData is Peripheral)
            {
                var selectedMotherboard = GetSelectedMotherboard();
                if (selectedMotherboard != null)
                {
                    selectedMotherboard.Peripherals.Remove((Peripheral) currentNodeData);
                    _configIsModified = true;
                }
            }
            RenderCurrentConfiguration();
        }

        private void frmConfigureDevicesAndPeripherals_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + " v" + Application.ProductVersion;
            RenderCurrentConfiguration();
        }

        private TreeNode FindNodeForConfigElement(PhccConfigElement e)
        {
            if (e == null) return null;
            foreach (TreeNode node in tvDevicesAndPeripherals.Nodes)
            {
                var nodeData = node.Tag;
                if (nodeData != null && nodeData == e)
                {
                    return node;
                }
            }
            return null;
        }

        private static string GetHexRepresentation(byte someByte)
        {
            var basicHexRep = someByte.ToString("X2").ToUpper();
            if (basicHexRep.Length < 2) basicHexRep = "0" + basicHexRep;
            basicHexRep = "0x" + basicHexRep;
            return basicHexRep;
        }

        private void RefreshPhccHsm()
        {
            if (_hsm == null)
            {
                _hsm = new List<PhccHardwareSupportModule.Phcc.PhccHardwareSupportModule>();
                foreach (var mb in _configMgr.Motherboards)
                {
                    _hsm.Add(PhccHardwareSupportModule.Phcc.PhccHardwareSupportModule.GetModuleForCalibration(mb));
                }
                
            }
            else
            {
                foreach (var hsm in _hsm)
                {
                    hsm.Dispose();
                }
                _hsm = new List<PhccHardwareSupportModule.Phcc.PhccHardwareSupportModule>();
                foreach (var mb in _configMgr.Motherboards)
                {
                    _hsm.Add(PhccHardwareSupportModule.Phcc.PhccHardwareSupportModule.GetModuleForCalibration(mb));
                }
            }

            EnableDisableUIElements();
        }

        private void RenderCurrentConfiguration()
        {
            SetTitleText();
            tvDevicesAndPeripherals.Nodes.Clear();
            if (_configMgr.Motherboards != null && _configMgr.Motherboards.Count != 0)
            {
                foreach (var motherboard in _configMgr.Motherboards)
                {
                    var comPort = "Unknown COM port";
                    var mName = "";
                    if (motherboard != null && !string.IsNullOrEmpty(motherboard.ComPort))
                    {
                        comPort = motherboard.ComPort;
                        mName = motherboard.Name;
                    }
                    var tn = new TreeNode();
                    tn.Tag = motherboard;
                    tn.Text = "PHCC Motherboard "+ mName + " (" + comPort + ")";
                    tvDevicesAndPeripherals.Nodes.Add(tn);
                    if (motherboard.Peripherals != null)
                    {
                        foreach (var p in motherboard.Peripherals)
                        {
                            tn = new TreeNode();
                            tn.Tag = p;
                            var deviceAddress = GetHexRepresentation(p.Address);
                            if (p is Doa40Do)
                            {
                                tn.Text = "DOA_40DO - Digital Output card " + p.FriendlyName + " @ " + deviceAddress;
                            }
                            else if (p is Doa7Seg)
                            {
                                tn.Text = "DOA_7Seg - 7-segment display driver card (BIT MODE) " + p.FriendlyName + " @ " + deviceAddress;
                            }                          
                            else if (p is Doa8Servo)
                            {
                                tn.Text = "DOA_8Servo - Servo motor driver card " + p.FriendlyName + " @ " + deviceAddress;
                            }
                            else if (p is DoaAirCore)
                            {
                                tn.Text = "DOA_AirCore - Air Core motor driver card " + p.FriendlyName + " @ " + deviceAddress;
                            }
                            else if (p is DoaStepper)
                            {
                                tn.Text = "DOA_Stepper - Stepper motor output card " + p.FriendlyName + " @ " + deviceAddress;
                            }
                            else if (p is DoaAnOut1)
                            {
                                tn.Text = "DOA_AnOut1 - Analog output card @ " + p.FriendlyName + " " + deviceAddress;
                            }
                            else if (p is DoaArduinoX27)
                            {
                                tn.Text = "DOA Arduino Stepper Motor @ " + p.FriendlyName + " " + deviceAddress;
                            }
                            var motherboardNode = FindNodeForConfigElement(motherboard);
                            if (motherboardNode != null)
                            {
                                motherboardNode.Nodes.Add(tn);
                            }
                        }
                        RefreshPhccHsm();
                    }
                }
            }
            tvDevicesAndPeripherals.Update();
            tvDevicesAndPeripherals.ExpandAll();
            EnableDisableUIElements();
        }

        private void SetTitleText()
        {
            var titleText = new StringBuilder();
            if (_currentConfigurationFilePath != null)
            {
                titleText.Append(new FileInfo(_currentConfigurationFilePath).Name);
            }
            else
            {
                titleText.Append("Untitled");
            }
            if (_configIsModified) titleText.Append("*");
            titleText.Append(" - ");
            titleText.Append(Application.ProductName);
            Text = titleText.ToString();
        }

        private void AddPeripheralToTree(Peripheral p)
        {
            var selectedMotherboard = GetSelectedMotherboard();
            if (selectedMotherboard != null)
            {
                selectedMotherboard.Peripherals.Add(p);
                RenderCurrentConfiguration();
                SelectNodeInTreeForConfigElement(p);
            }
        }

        private void SelectNodeInTreeForConfigElement(PhccConfigElement e)
        {
            if (e == null) return;
            var node = FindNodeForConfigElement(e);
            if (node != null)
            {
                tvDevicesAndPeripherals.SelectedNode = node;
            }
            tvDevicesAndPeripherals.Select();
        }

        private void AddNewPeripheral<T>() where T : Peripheral, new()
        {
            var prompt = new frmPromptForPeripheralAddress();
            var m = GetSelectedMotherboard();
            if (m != null && m.Peripherals != null)
            {
                foreach (var p in m.Peripherals)
                {
                    if (prompt.ProhibitedBaseAddresses == null) prompt.ProhibitedBaseAddresses = new List<byte>();
                    prompt.ProhibitedBaseAddresses.Add(p.Address);
                }
            }
            var result = prompt.ShowDialog(this);
            if (result == DialogResult.Cancel) return;
            var newDevice = new T {Address = prompt.BaseAddress, FriendlyName = prompt.FriendlyName};
            AddPeripheralToTree(newDevice);
            _configIsModified = true;
        }

        private void CalibrateDoa8Servo()
        {
            var servoNode = tvDevicesAndPeripherals.SelectedNode.Tag as Doa8Servo;
            if (servoNode != null)
            {
                var selectServoDialog = new frmSelectServo();
                var result = selectServoDialog.ShowDialog(this);
                if (result == DialogResult.Cancel) return;
                var selectedServo = selectServoDialog.SelectedServo;

                if (servoNode.ServoCalibrations == null)
                {
                    servoNode.ServoCalibrations = CreateDefaultServoCalibrations();
                }

                var calibrateServoDialog = new frmCalibrateServo();
                calibrateServoDialog.Gain = servoNode.ServoCalibrations[selectedServo - 1].Gain;
                calibrateServoDialog.CalibrationOffset =
                    (ushort) servoNode.ServoCalibrations[selectedServo - 1].CalibrationOffset;

                result = calibrateServoDialog.ShowDialog(this);

                if (result != DialogResult.Cancel)
                {
                    var thisServoCalibration = servoNode.ServoCalibrations[selectedServo - 1];
                    if (thisServoCalibration == null)
                    {
                        thisServoCalibration = new ServoCalibration();
                        thisServoCalibration.ServoNum = (short) selectedServo;
                    }
                    thisServoCalibration.Gain = (byte) calibrateServoDialog.Gain;
                    thisServoCalibration.CalibrationOffset = (short) calibrateServoDialog.CalibrationOffset;
                    _configIsModified = true;
                    RenderCurrentConfiguration();
                }
            }
        }

        private static List<ServoCalibration> CreateDefaultServoCalibrations()
        {
            var toReturn = new List<ServoCalibration>(8);
            for (var i = 0; i < 8; i++)
            {
                unchecked
                {
                    var defaultVal = (short) 0xE8F0;
                    toReturn.Add
                        (
                            new ServoCalibration
                                {
                                    ServoNum = (short) i,
                                    Gain = 200,
                                    CalibrationOffset = defaultVal
                                }
                        );
                }
            }
            return toReturn;
        }

        private void tvDevicesAndPeripherals_Click(object sender, EventArgs e)
        {
            var args = (MouseEventArgs) e;
            if (args.Button == MouseButtons.Right)
            {
                if (tvDevicesAndPeripherals.SelectedNode != null)
                {
                    ctxContext.Show(tvDevicesAndPeripherals, args.Location, ToolStripDropDownDirection.BelowRight);
                }
            }
        }

        private void EnableDisableUIElements()
        {
            mnuContextCalibrate.Enabled = false;
            calibrateServosToolStripMenuItem1.Enabled = false;
            calibrateAnalogToolStripMenuItem1.Enabled = false;
            calibrateServosToolStripMenuItem.Enabled = false;
            calibrateAnalogToolStripMenuItem.Enabled = false;
            configure7SegmentDisplayToolStripMenuItem1.Enabled = false;
            configure7SegmentDisplayToolStripMenuItem.Enabled = false;
            configureDOA40DOToolStripMenuItem.Enabled = false;
            configureDOA40DOToolStripMenuItem1.Enabled = false;
            motherboardDigitalOutputsToolStripMenuItem.Enabled = false;
            motherboardDigitalOutputsToolStripMenuItem1.Enabled = false;
            dOADuinoX27ToolStripMenuItem2.Enabled = false;
            calibrateDOADuinoX27ToolStripMenuItem.Enabled = false;
            mnuDevicesCalibrate.Enabled = false;
            mnuDevicesSetComPort.Enabled = false;
            mnuContextSetCOMPort.Enabled = false;
            mnuDevicesAddPeripheral.Enabled = false;
            mnuContextAddPeripheral.Enabled = false;
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            var selectedMotherboard = GetSelectedMotherboard();
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    if (selectedNodeData is Doa8Servo)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        calibrateServosToolStripMenuItem1.Enabled = true;
                        calibrateServosToolStripMenuItem.Enabled = true;

                    }
                    else if (selectedNodeData is Motherboard)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        mnuDevicesSetComPort.Enabled = true;
                        mnuContextSetCOMPort.Enabled = true;
                        motherboardDigitalOutputsToolStripMenuItem.Enabled = true;
                        motherboardDigitalOutputsToolStripMenuItem1.Enabled = true;
                    }

                    if (selectedNodeData is DoaAirCore)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        calibrateAnalogToolStripMenuItem1.Enabled = true;
                        calibrateAnalogToolStripMenuItem.Enabled = true;

                    }

                    if (selectedNodeData is DoaStepper)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        calibrateAnalogToolStripMenuItem1.Enabled = true;
                        calibrateAnalogToolStripMenuItem.Enabled = true;

                    }

                    if (selectedNodeData is DoaAnOut1)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        calibrateAnalogToolStripMenuItem1.Enabled = true;
                        calibrateAnalogToolStripMenuItem.Enabled = true;

                    }

                    if (selectedNodeData is Doa7Seg)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        configure7SegmentDisplayToolStripMenuItem1.Enabled = true;
                        configure7SegmentDisplayToolStripMenuItem.Enabled = true;

                    }

                    if (selectedNodeData is Doa40Do)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        configureDOA40DOToolStripMenuItem.Enabled = true;
                        configureDOA40DOToolStripMenuItem1.Enabled = true;

                    }
                   
                    if (selectedNodeData is DoaArduinoX27)
                    {
                        mnuDevicesCalibrate.Enabled = true;
                        mnuContextCalibrate.Enabled = true;
                        dOADuinoX27ToolStripMenuItem2.Enabled = true;
                        calibrateDOADuinoX27ToolStripMenuItem.Enabled = true;

                    }
                }

                if (selectedMotherboard != null)
                {
                    mnuDevicesAddPeripheral.Enabled = true;
                    mnuContextAddPeripheral.Enabled = true;
                }
            }
        }

        private void SaveConfigurationViaDialog()
        {
            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.CreatePrompt = false;
            dialog.DefaultExt = ".config";
            dialog.DereferenceLinks = true;
            dialog.FileName = _currentConfigurationFilePath;
            dialog.Filter = "PHCC DevMgr Configuration Files(*.config)|*.config";
            dialog.FilterIndex = 0;
            dialog.InitialDirectory = new FileInfo(Application.ExecutablePath).Directory.FullName;
            dialog.OverwritePrompt = false;
            dialog.RestoreDirectory = true;
            dialog.ShowHelp = false;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Save Configuration File";
            dialog.ValidateNames = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                SaveConfiguration(dialog.FileName);
            }
        }

        private void LoadConfigurationViaDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = ".config";
            dialog.DereferenceLinks = true;
            dialog.Filter = "PHCC DevMgr Configuration Files(*.config)|*.config";
            dialog.FilterIndex = 0;
            dialog.InitialDirectory = new FileInfo(Application.ExecutablePath).Directory.FullName;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.ShowHelp = false;
            dialog.ShowReadOnly = false;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Open Configuration File";
            dialog.ValidateNames = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                _currentConfigurationFilePath = dialog.FileName;
                LoadConfiguration(_currentConfigurationFilePath);
            }
        }

        private void FileNew()
        {
            if (_configIsModified)
            {
                var result =
                    MessageBox.Show("There are unsaved changes.  Would you like to save changes before continuing?",
                                    Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button3);
                switch (result)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        SaveConfigurationViaDialog();
                        break;
                    default:
                        break;
                }
            }
            _currentConfigurationFilePath = null;
            _configMgr = new ConfigurationManager();
            RenderCurrentConfiguration();
        }

        private void FileSave()
        {
            if (_currentConfigurationFilePath == null)
            {
                SaveConfigurationViaDialog();
            }
            else
            {
                SaveConfiguration(_currentConfigurationFilePath);
            }
        }

        private DialogResult SetComPort()
        {
            var selectedMotherboard = GetSelectedMotherboard();
            if (selectedMotherboard == null) return DialogResult.Cancel;
            var result = SetComPort(selectedMotherboard);
            if (result == DialogResult.OK)
            {
                _configIsModified = true;
                RenderCurrentConfiguration();
            }
            return result;
        }

        private DialogResult SetComPort(Motherboard motherboard)
        {
            if (motherboard == null) throw new ArgumentNullException(nameof(motherboard));
            var dialog = new frmSelectCOMPort();
            foreach (var m in _configMgr.Motherboards)
            {
                if (m == motherboard) continue;
                if (!string.IsNullOrEmpty(m.ComPort))
                {
                    if (dialog.COMPorts != null && dialog.COMPorts.Contains(m.ComPort))
                    {
                        dialog.COMPorts.Remove(m.ComPort);
                    }
                }
            }

            dialog.COMPort = motherboard.ComPort;
            dialog.Name = motherboard.Name;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                motherboard.ComPort = dialog.COMPort;
                motherboard.Name = dialog.Name;
            }
            return result;
        }

        private void AddMotherboard()
        {
            var newMotherBoard = new Motherboard();
            var result = SetComPort(newMotherBoard);
            if (result == DialogResult.OK)
            {
                _configMgr.Motherboards.Add(newMotherBoard);
                _configIsModified = true;
                RenderCurrentConfiguration();
            }
        }

        private void Calibrate()
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    if (selectedNodeData is Doa8Servo)
                    {
                        CalibrateDoa8Servo();
                    }
                }
            }
        }

        private void CalibrateAnalog()
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    var dev = selectedNodeData as Peripheral;

                    var test = _hsm.SelectMany(x=> x.AnalogOutputs).Where(x =>
                         Convert.ToByte(x.SubSourceAddress.Replace("0x", ""),16) == dev.Address)
                        .ToList();

                    List<Signal> lst = new List<Signal>();
                    foreach (var analogSignal in test)
                    {
                        lst.Add(analogSignal);
                    }
                    CalibrationSelect p = new CalibrationSelect(lst, _hsm.SelectMany(x=> x.DigitalInputs).ToList(), dev);
                 

                    if (selectedNodeData is DoaAirCore )
                    {
                        var board = selectedNodeData as DoaAirCore;
                        board.OutputConfigs = p.OutputConfigs;

                    }
                    else if (selectedNodeData is Doa8Servo)
                    {
                        var board = selectedNodeData as Doa8Servo;
                        board.OutputConfigs = p.OutputConfigs;
                    }
                    else if (selectedNodeData is DoaAnOut1)
                    {
                        var board = selectedNodeData as DoaAnOut1;
                        board.OutputConfigs = p.OutputConfigs;
                    }
                    else if (selectedNodeData is DoaStepper)
                    {
                        var board = selectedNodeData as DoaStepper;
                        board.OutputConfigs = p.OutputConfigs;
                    }
                    else if (selectedNodeData is DoaArduinoX27)
                    {
                        var board = selectedNodeData as DoaArduinoX27;
                        board.OutputConfigs = p.OutputConfigs;
                    }
                    p.ShowDialog();
                }
            }
        }

        private void mnuContextAddMotherboard_Click(object sender, EventArgs e)
        {
            AddMotherboard();
        }

        #region Menu click event handlers

        #region File Menu

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            var success = false;
            var keepTrying = true;
            while (!success && keepTrying)
            {
                try
                {
                    LoadConfigurationViaDialog();
                    success = true;
                }
                catch (Exception ex)
                {
                    var errorMessage = $"The selected file could not be loaded.\n\n  Reason: {ex.Message}";
                    _log.Error(errorMessage, ex);
                    var result = MessageBox.Show(errorMessage, Application.ProductName,
                                                 MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                                                 MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Cancel) keepTrying = false;
                }
            }
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            FileNew();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Help Menu 

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            new frmHelpAbout().ShowDialog(this);
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveConfigurationViaDialog();
        }

        #endregion

        #region Context Menu

        private void mnuContextSetComPort_Click(object sender, EventArgs e)
        {
            SetComPort();
        }

        private void mnuContextAddPeripheralDoa40Do_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa40Do>();
        }

        private void mnuContextAddPeripheralDoaAircore_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaAirCore>();
        }

        private void mnuContextAddPeripheralDoaStepper_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaStepper>();
        }

        private void mnuContextAddPeripheralDoaAnOut1_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaAnOut1>();
        }

        private void mnuContextAddPeripheralDoa7Seg_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa7Seg>();
        }

        private void mnuContextAddPeripheralDoa8Servo_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa8Servo>();
        }

        private void mnuContextRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedNode();
        }

        private void mnuContextCalibrate_Click(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region Devices Menu

        private void mnuDevicesAddMotherboard_Click(object sender, EventArgs e)
        {
            AddMotherboard();
        }

        private void mnuDevicesAddPeripheralDoa40Do_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa40Do>();
        }

        private void mnuDevicesAddPeripheralDoa7Seg_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa7Seg>();
        }
        private void mnuDevicesAddPeripheralDoa8Servo_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<Doa8Servo>();
        }

        private void mnuDevicesAddPeripheralDoaAircore_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaAirCore>();
        }

        private void mnuDevicesAddPeripheralDoaAnOut1_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaAnOut1>();
        }
        private void dOADuinoX27ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaArduinoX27>();
        }

        private void dOADuinoX27ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaArduinoX27>();
        }
        private void mnuDevicesAddPeripheralDoaStepper_Click(object sender, EventArgs e)
        {
            AddNewPeripheral<DoaStepper>();
        }

        private void mnuDevicesSetComPort_Click(object sender, EventArgs e)
        {
            SetComPort();
        }

        private void mnuDevicesCalibrate_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuDevicesRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedNode();
        }

        #endregion

        private void tvDevicesAndPeripherals_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EnableDisableUIElements();
        }

        #endregion

        #region Load/Save Config Files

        private void LoadConfiguration(string configurationFilePath)
        {
            var fi = new FileInfo(configurationFilePath);
            if (fi.Exists)
            {
                _configMgr = ConfigurationManager.Load(fi.FullName);
                _configIsModified = false;
            }
            else
            {
                throw new FileNotFoundException(configurationFilePath);
            }

            RenderCurrentConfiguration();
        }

        private void SaveConfiguration(string configurationFilePath)
        {
            var success = false;
            var keepTrying = true;
            while (!success && keepTrying)
            {
                try
                {
                    _currentConfigurationFilePath = configurationFilePath;
                    _configMgr.Save(configurationFilePath);
                    _configIsModified = false;
                    RenderCurrentConfiguration();
                    success = true;
                }
                catch (Exception ex)
                {
                    var errorMessage = $"The selected file could not be saved.\n\n  Reason: {ex.Message}";
                    _log.Error(errorMessage, ex);
                    var result = MessageBox.Show(errorMessage, Application.ProductName,
                                                 MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                                                 MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Cancel) keepTrying = false;
                }
            }
        }

        #endregion

        private void calibrateServosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calibrate();
        }

        private void calibrateServosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void calibrateAnalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }

        private void calibrateAnalogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }

        private void configure7SegmentDisplayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Config7Segment();
        }

        private void configure7SegmentDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config7Segment();
        }
        private void configureDOA40DOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config40DO();
        }
        private void configureDOA40DOToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Config40DO();
        }

        private void digitalInputsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigDigitalOutputs();
        }
   
        private void digitalInputsToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ConfigDigitalOutputs();
        }
        private void dOADuinoX27ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }

        private void calibrateDOADuinoX27ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }
        private void gaugeCalibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }

        private void gaugeCalibrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CalibrateAnalog();
        }

        private void gainOffsetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Calibrate();
        }

        private void gainOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calibrate();
        }
        private void Config7Segment()
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    if (selectedNodeData is Doa7Seg)
                    {
                        var board = selectedNodeData as Doa7Seg;                       
                        Doa7SegConfigList p = new Doa7SegConfigList(board.Configuration);
                        p.ShowDialog();
                        board.Configuration = p.Configuration;
                        p.Dispose();
                    }
                    
                    
                }
            }
        }

        private void ConfigDigitalOutputs()
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    if (selectedNodeData is Motherboard)
                    {
                        var board = selectedNodeData as Motherboard;
                        DigitalInputConfig p = new DigitalInputConfig(board.InputConfig);
                        p.ShowDialog();
                        board.InputConfig = p.Configuration;
                        p.Dispose();
                    }


                }
            }
        }
        private void Config40DO()
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null)
            {
                var selectedNodeData = selectedNode.Tag;
                if (selectedNodeData != null)
                {
                    if (selectedNodeData is Doa40Do)
                    {
                        var board = selectedNodeData as Doa40Do;
                        DigitalInputConfig p = new DigitalInputConfig(board.OutputConfig);
                        p.ShowDialog();
                        board.OutputConfig = p.Configuration;
                        p.Dispose();
                    }


                }
            }
        }
        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
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


                ExcelPackage pkg =new ExcelPackage(f);
                foreach (var motherboard in _configMgr.Motherboards)
                {
                    Export.ListToExcel(motherboard.Peripherals, pkg,
                        motherboard.FriendlyName + "- " + motherboard.ComPort);
                    foreach (var board in motherboard.Peripherals)
                    {

                        if (board is DoaStepper)
                        {
                            var b = board as DoaStepper;
                            Export.ListToExcel(b.HomingSignals, pkg,
                                board.FriendlyName + " - " + "Home_IN");
                            Export.ListToExcel(b.OutputConfigs.FirstOrDefault()?.CalibrationData, pkg,
                                board.FriendlyName + " - " + "Calibration");
                        }
                        else if (board is Doa7Seg)
                        {
                            var b = board as Doa7Seg;
                            Export.ListToExcel(b.Configuration.DisplayModeConfiguration, pkg,
                                board.FriendlyName);
                         
                        }
                        else if (board is Doa8Servo)
                        {
                            var b = board as Doa8Servo;
                            Export.ListToExcel(b.OutputConfigs, pkg,
                                board.FriendlyName + " - Configs");

                            Export.ListToExcel(b.OutputConfigs.FirstOrDefault()?.CalibrationData, pkg,
                                board.FriendlyName + " - Calibration");
                        }
                        else if (board is DoaAirCore)
                        {
                            var b = board as DoaAirCore;
                            Export.ListToExcel(b.OutputConfigs.FirstOrDefault()?.CalibrationData, pkg,
                                board.FriendlyName + " - Calibration");
                        }
                        else if (board is DoaAnOut1)
                        {
                            var b = board as DoaAnOut1;
                            Export.ListToExcel(b.OutputConfigs.FirstOrDefault()?.CalibrationData, pkg,
                                board.FriendlyName + " - Calibration");

                        }


                    }
                }
                pkg.Save();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = tvDevicesAndPeripherals.SelectedNode;
            if (selectedNode != null && selectedNode.Tag is Motherboard)
            {
                var mb = selectedNode.Tag as Motherboard;

                for (int i = 0; i < 127; i++)
                {
                    mb.Peripherals.Add(new DoaAnOut1((byte)i));
                   
                }
            }
            RenderCurrentConfiguration();
        }

 
    }
}