using System;
using System.Collections.Generic;
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
using Common.MacroProgramming;
using PhccHsm.ConfigUi.OldForms;
using PhccHsm.ConfigUi.OldForms.DigitalConfig;
using PhccHsm.ConfigUi.OldForms.Doa7SegConfig;
using PhccHsm.HsmSupport.Peripherals;
using Motherboard = PhccHsm.HsmSupport.Motherboard;

namespace PhccHsm.ConfigUi
{
    /// <summary>
    /// Interação lógica para PhccConfigMain.xam
    /// </summary>
    public partial class PhccConfigMain : UserControl
    {

        private List<Motherboard> _cfg = new List<Motherboard>();
        private Window _parent;

        private Motherboard selectedMotherboard => GetSelectedMotherboard();

        private BasePeripheral selectedPeripheral { get; set; }
      

        public PhccConfigMain(Window parent, List<Motherboard> cfg)
        {
            _parent = parent;
            InitializeComponent();
            if (cfg != null)
            {
                _cfg = cfg;
            }
            else
            {
                _cfg = Motherboard.GetDevices();
            }
           
            if (_cfg == null)
            {
                _cfg = new List<Motherboard>();
            }

            SetUi();
            RenderCurrentConfiguration();
        }

        private void MnuFile_OnClick(object sender, RoutedEventArgs e)
        {

        }

        public void SetUi()
        {
            //MainMenu
            addMboard.Visibility = Visibility.Visible;
            Item.Visibility = Visibility.Hidden;

            //ctxMenu
            editComPort.Visibility = Visibility.Hidden;
            configMb.Visibility = Visibility.Hidden;
            peripherals.Visibility = Visibility.Hidden;
            editPhAddress.Visibility = Visibility.Hidden;
            Configure.Visibility = Visibility.Hidden;
            removeSelected.Visibility = Visibility.Hidden;
            addMboard2.Visibility = Visibility.Visible;

        }

        public void EnableDisabeUi()
        {
            if (selectedPeripheral == null && selectedMotherboard != null)
            {
                //MainMenu
                addMboard.Visibility = Visibility.Hidden;
                Item.Visibility = Visibility.Visible;

                //ctxMenu
                editComPort.Visibility = Visibility.Visible;
                configMb.Visibility = Visibility.Visible;
                peripherals.Visibility = Visibility.Visible;
                editPhAddress.Visibility = Visibility.Hidden;
                Configure.Visibility = Visibility.Hidden;
                removeSelected.Visibility = Visibility.Visible;
                addMboard2.Visibility = Visibility.Visible;
            }
            else if (selectedPeripheral != null)
            {
                //MainMenu
                addMboard.Visibility = Visibility.Hidden;
                Item.Visibility = Visibility.Visible;

                //ctxMenu
                editComPort.Visibility = Visibility.Visible;
                configMb.Visibility = Visibility.Visible;
                peripherals.Visibility = Visibility.Visible;
                editPhAddress.Visibility = Visibility.Visible;
                Configure.Visibility = Visibility.Visible;
                removeSelected.Visibility = Visibility.Visible;
                addMboard2.Visibility = Visibility.Visible;
            }
        }

        public void Save()
        {
            Motherboard.SaveDeviceConfig(_cfg);
        }

        #region Ui



        private void RenderCurrentConfiguration()
        {
            tvConfig.Items.Clear();
            foreach (var mb in _cfg)
            {
                TreeViewItem mboard = new TreeViewItem();
                mboard.DataContext = mb;

                mboard.Header = mb.FriendlyName;
                foreach (var mbPeripheral in mb.Peripherals)
                {
                    if (mbPeripheral.FriendlyName == null || mbPeripheral.FriendlyName.Length < 2)
                    {


                        if (mbPeripheral is HsmDoa40Do)
                        {
                            mbPeripheral.FriendlyName = "DOA 40 DO " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoaStepper)
                        {
                            mbPeripheral.FriendlyName = "DOA Stepper " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoa7Seg)
                        {
                            mbPeripheral.FriendlyName = "DOA 7 Segments " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoa8Servo)
                        {
                            mbPeripheral.FriendlyName = "DOA 8 Servos " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoaAirCore)
                        {
                            mbPeripheral.FriendlyName = "DOA Aircores " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoaAnOut1)
                        {
                            mbPeripheral.FriendlyName = "DOA AN OUT 1 " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                        else if (mbPeripheral is HsmDoaArduinoX27)
                        {
                            mbPeripheral.FriendlyName = "DOA Arduino X27 " + mbPeripheral.FriendlyName + " @ 0x" +
                                                        mbPeripheral.Address.ToString("X");
                        }
                    }

                    TreeViewItem Ph = new TreeViewItem();
                    Ph.DataContext = mbPeripheral;

                    Ph.Header = mbPeripheral.FriendlyName;
                    mboard.Items.Add(Ph);
                }

                tvConfig.Items.Add(mboard);
            }

        }

        private Motherboard GetSelectedMotherboard()
        {
            var currentNode = tvConfig.SelectedItem as TreeViewItem;

            while (currentNode != null)
            {
                var currentNodeData = currentNode.DataContext;
                if (currentNodeData is Motherboard)
                {
                    return (Motherboard) currentNodeData;
                }
                else
                {
                    ItemsControl parent = GetSelectedTreeViewItemParent(currentNode);
                    if (parent.DataContext is Motherboard)
                    {
                        return (Motherboard) parent.DataContext;
                    }
                }
            }

            return null;
        }


        #endregion

        #region Helpers

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }

        #endregion

        #region mnuHandlers

        private void TvConfig_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
  
            var currentNode = tvConfig.SelectedValue as TreeViewItem;
            if (currentNode != null)
            {
                var currentNodeData = currentNode.DataContext;
                if (currentNodeData is BasePeripheral)
                {
                     selectedPeripheral = currentNodeData as BasePeripheral;
                }
            }
            EnableDisabeUi();
        }


        private void AddMboard_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            var prompt = new ComPort(window);
            window.Content = prompt;
            window.Width = prompt.Width;
            window.Height = prompt.Height;
            window.ShowDialog();
            if (prompt.Port != null && prompt.Port.Length > 2 && !prompt.Cancel)
            {
                Motherboard m = new Motherboard() {ComPort = prompt.Port, FriendlyName = prompt.FriendlyName};
                _cfg.Add(m);
                RenderCurrentConfiguration();
            }
        }

        private void AddDoa40Do_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoa40Do>();
        }

        private void Add7Segments_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoa7Seg>();
        }

        private void AddAirCore_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoaAirCore>();
        }

        private void AddStepper_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoaStepper>();
        }

        private void AddarduinoStepper_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoaArduinoX27>();
        }

        private void AddAn1_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoaAnOut1>();
        }
        private void AddServo_OnClick(object sender, RoutedEventArgs e)
        {
            AddNewPeripheral<HsmDoa8Servo>();
        }
        #endregion

        #region Peripherals
        private void AddNewPeripheral<T>() where T : BasePeripheral, new()
        {
            var window = new Window();
            var prompt = new PeripheralAddress(window);
          
            var m = GetSelectedMotherboard();
            if (m != null && m.Peripherals != null)
            {
                foreach (var p in m.Peripherals)
                {
                    if (prompt.ProhibitedBaseAddresses == null) prompt.ProhibitedBaseAddresses = new List<byte>();
                    prompt.ProhibitedBaseAddresses.Add((byte)p.Address);
                }
            }

            window.Content = prompt;

            window.Width = prompt.Width;
            window.Height = prompt.Height;
            window.ShowDialog();
            if (!prompt.Cancel)
            {
                 
                var newDevice = new T { Address = prompt.Address, FriendlyName = prompt.FriendlyName };
                var mb = GetSelectedMotherboard();
                mb.Peripherals.Add(newDevice);
               RenderCurrentConfiguration();
            }

        }
        private void RemoveSelectedNode()
        {
            var currentNode = tvConfig.SelectedValue as TreeViewItem;
            var currentNodeData = currentNode.DataContext;
            if (currentNodeData is Motherboard)
            {
                _cfg.Remove((Motherboard)currentNodeData);

            }
            if (currentNodeData is BasePeripheral)
            {
                var selectedMotherboard = GetSelectedMotherboard();
                if (selectedMotherboard != null)
                {
                    selectedMotherboard.Peripherals.Remove((BasePeripheral)currentNodeData);

                }
            }
            RenderCurrentConfiguration();
        }

        #endregion

        private void MnuSave_OnClick(object sender, RoutedEventArgs e)
        {
            Motherboard.SaveDeviceConfig(_cfg);
        }

        private void MnuClose_OnClick(object sender, RoutedEventArgs e)
        {
            if (_parent != null)
            {
                _parent.Close();
            }
        }


        private void EditComPort_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            var prompt = new ComPort(window);
            window.Content = prompt;
            window.Width = prompt.Width;
            window.Height = prompt.Height;
            var m = GetSelectedMotherboard();
            prompt.tbName.Text = m.FriendlyName;
            prompt.cbComPort.Text = m.ComPort;
            window.ShowDialog();
            if (prompt.Port != null && prompt.Port.Length > 2 && !prompt.Cancel)
            {

                m.FriendlyName = prompt.FriendlyName;
                m.ComPort = prompt.Port;
                RenderCurrentConfiguration();
            }
        }

        private void ConfigMb_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigureNode();
        }
      
        private void EditPhAddress_OnClick(object sender, RoutedEventArgs e)
        {
            var currentNodeData = tvConfig.SelectedValue as TreeViewItem;
          
            if (currentNodeData.DataContext is BasePeripheral)
            {
                var typedNode = currentNodeData.DataContext as BasePeripheral;

                var window = new Window();
                var prompt = new PeripheralAddress(window);
                prompt.tbAddress.Text = typedNode.Address.ToString("X");
                prompt.tbFriendly.Text = typedNode.FriendlyName;
                var m = GetSelectedMotherboard();
                if (m != null && m.Peripherals != null)
                {
                    foreach (var p in m.Peripherals)
                    {
                        if (prompt.ProhibitedBaseAddresses == null) prompt.ProhibitedBaseAddresses = new List<byte>();
                        prompt.ProhibitedBaseAddresses.Add((byte)p.Address);
                    }
                }

                window.Content = prompt;

                window.Width = prompt.Width;
                window.Height = prompt.Height;
                window.ShowDialog();
                if (!prompt.Cancel)
                {
                    typedNode.FriendlyName = prompt.FriendlyName;
                    typedNode.Address = (int) prompt.Address;                 
                    RenderCurrentConfiguration();
                }

            }
        }

        private void Configure_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigureNode();
        }

        private void RemoveSelected_OnClick(object sender, RoutedEventArgs e)
        {
            RemoveSelectedNode();
        }


        private void ConfigureNode()
        {
            var currentNode = tvConfig.SelectedValue as TreeViewItem;
            var item = currentNode.DataContext;
            if (item is Motherboard)
            {
                var board = item as Motherboard;
                DigitalInputConfig p = new DigitalInputConfig(board.InputConfig);
                p.ShowDialog();
                board.InputConfig = p.Configuration;
                p.Dispose();

            }
            else if (item is HsmDoa40Do)
            {
                var board = item as HsmDoa40Do;
                DigitalInputConfig p = new DigitalInputConfig(board.OutputConfig);
                p.ShowDialog();
                board.OutputConfig = p.Configuration;
                p.Dispose();
            }
            else if (item is HsmDoaStepper)
            {
                var board = item as HsmDoaStepper;
                CalibrationSelect p = new CalibrationSelect(board.AnalogOutputs.ConvertAll(c=> (Signal)c),
                    GetSelectedMotherboard().DigitalInputs.ToList(), item as HsmDoaStepper);            
                board.OutputConfigs = p.OutputConfigs;
                p.ShowDialog();
            }
            else if (item is HsmDoa7Seg)
            {
                var board = item as HsmDoa7Seg;
                Doa7SegConfigList p = new Doa7SegConfigList(board.Configuration);
                p.ShowDialog();
                board.Configuration = p.Configuration;
                p.Dispose();
            }
            else if (item is HsmDoa8Servo)
            {
                var board = item as HsmDoa8Servo;
                CalibrationSelect p = new CalibrationSelect(board.AnalogOutputs.ConvertAll(c => (Signal)c),
                   GetSelectedMotherboard().DigitalInputs.ToList(), item as HsmDoa8Servo);
                p.ShowDialog();
                board.OutputConfigs = p.OutputConfigs;
               
            }
            else if (item is HsmDoaAirCore)
            {
                var board = item as HsmDoaAirCore;
                CalibrationSelect p = new CalibrationSelect(board.AnalogOutputs.ConvertAll(c => (Signal)c),
                   GetSelectedMotherboard().DigitalInputs.ToList(), item as HsmDoaAirCore);
                p.ShowDialog();
                board.OutputConfigs = p.OutputConfigs;
                
            }
            else if (item is HsmDoaAnOut1)
            {
                var board = item as HsmDoaAnOut1;
                CalibrationSelect p = new CalibrationSelect(board.AnalogOutputs.ConvertAll(c => (Signal)c),
                   GetSelectedMotherboard().DigitalInputs.ToList(), item as HsmDoaAnOut1);
                p.ShowDialog();
                board.OutputConfigs = p.OutputConfigs;
               
            }
            else if (item is HsmDoaArduinoX27)
            {
                var board = item as HsmDoaArduinoX27;
                CalibrationSelect p = new CalibrationSelect(board.AnalogOutputs.ConvertAll(c => (Signal)c),
                   GetSelectedMotherboard().DigitalInputs.ToList(), item as HsmDoaArduinoX27);
                p.ShowDialog();
                board.OutputConfigs = p.OutputConfigs;
                
            }

            RenderCurrentConfiguration();
        }

       
    }
}
