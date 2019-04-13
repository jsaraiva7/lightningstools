using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Common.SimLinkup.Scripting;
using SimLinkupControls.Mapping.Models;
using SimLinkupControls.Signals;
using Signal = Common.MacroProgramming.Signal;

namespace SimLinkupControls.Mapping
{
    /// <summary>
    /// Interação lógica para AddMapping.xam
    /// </summary>
    public partial class AddMapping : UserControl
    {
        public bool Cancel { get; set; } = false;
        private Window _window;
        private ScriptingContext _ctx;


        private Signal Input { get; set; }
        private Signal Output { get; set; }
        public SignalMapping Mapping { get; set; }

        private SignalList _signalList;

        public AddMapping(Window w, ScriptingContext ctx, SignalList lst)
        {
            _signalList = lst;
            _window = w;
            _ctx = ctx;
            InitializeComponent();
            _window.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            try
            {
                this.MyGrid.Children.Remove(_signalList);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                 
            }
      

        }
        private void BtnSelectSource_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Window();
                _signalList._window = w;
                var p = _signalList;
                p.btnSelectSignal.Visibility = Visibility.Visible;
                w.Content = p;
                w.ShowDialog();
                
                Input = p.SelectedSignal;
                var s = new SignalCard(Input);
                rectSource.Content = s;
             //   lblInput.Content = Input.Id;
                p.btnSelectSignal.Visibility = Visibility.Hidden;
                MyGrid.Children.Remove(_signalList);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
               
            }
        }

        private void BtnSelectDestination_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Window();
                _signalList._window = w;
                var p = _signalList;
                
                p.btnSelectSignal.Visibility = Visibility.Visible;
                w.Content = p;
                w.ShowDialog();
              
                Output = p.SelectedSignal;
                var s = new SignalCard(Output);
                rectDest.Content = s;

                // lblOutput.Content = Output.Id;
                p.btnSelectSignal.Visibility = Visibility.Hidden;
                MyGrid.Children.Remove(_signalList);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {

            SignalMapping m = new SignalMapping();
            if (Input is Common.MacroProgramming.AnalogSignal)
            {
                m.Source = new AnalogSignal() { Id = Input.Id };
            }
            else if (Input is Common.MacroProgramming.DigitalSignal)
            {
                m.Source = new DigitalSignal() { Id = Input.Id };
            }
            else if (Input is Common.MacroProgramming.TextSignal)
            {
                m.Source = new TextSignal() { Id = Input.Id };
            }
            if (Output is Common.MacroProgramming.AnalogSignal)
            {
                m.Destination = new AnalogSignal() { Id = Output.Id };
            }
            else if (Output is Common.MacroProgramming.DigitalSignal)
            {
                m.Destination = new DigitalSignal() { Id = Output.Id };
            }
            else if (Output is Common.MacroProgramming.TextSignal)
            {
                m.Destination = new TextSignal() { Id = Output.Id };
            }
            Mapping = m;

            Cancel = false;
            _window.Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Cancel = true;
            _window.Close();
        }
    }
}
