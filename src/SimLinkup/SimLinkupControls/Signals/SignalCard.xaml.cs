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

namespace SimLinkupControls.Signals
{
    /// <summary>
    /// Interação lógica para SignalCard.xam
    /// </summary>
    public partial class SignalCard : UserControl
    {
        public SignalCard(Signal signal)
        {
            InitializeComponent();
            lblName.Content = signal.FriendlyName;
            lblCategory.Content = signal.Category;
            lblSubCategory.Content = signal.CollectionName;
            lblType.Content = signal.SignalType;
            lblPublisher.Content = signal.SourceFriendlyName;
            if (signal is DigitalSignal)
            {
                var typedSignal = signal as DigitalSignal;
                lblMax.Content = "true";
                lblmin.Content = "false";
            }
            else if (signal is AnalogSignal)
            {
                var typedSignal = signal as AnalogSignal;
                lblMax.Content = typedSignal.MaxValue.ToString();
                lblmin.Content = typedSignal.MinValue.ToString();
            }
            else
            {
                lblmin.Content = "";
                lblMax.Content = "";
            }
        }


    }
}
