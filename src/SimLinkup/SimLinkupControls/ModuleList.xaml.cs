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
using Common.HardwareSupport;
using Common.MacroProgramming;
using Common.SimLinkup.Scripting;
using Common.SimSupport;

namespace SimLinkupControls
{
    public enum ModuleType
    {
        HSM, 
        SSM, 
        ALL,
    }
    /// <summary>
    /// Interação lógica para ModuleList.xam
    /// </summary>
    public partial class ModuleList : UserControl
    {

        public ScriptingContext Context { get; set; }
        public IHardwareSupportModule SelectedHsm { get; private set; }
        public SimSupportModule SelectedSSM { get; private set; }
        private ModuleType _moduleType;

        public ModuleList()
        {
           
        }

        public ModuleList(ScriptingContext ctx, ModuleType mType)
        {
            InitializeComponent();
            Context = ctx;
            _moduleType = mType;
            btnConfigure.IsEnabled = false;
            PopulateModules();
        }


        private void PopulateModules()
        {
            if(_moduleType == ModuleType.HSM)
            if (Context.AllSignals != null)
            {
                var sources = Context.HardwareSupportModules.ToList();
                foreach (var source in sources)
                {
                    var item = new TreeViewItem();
                    item.Header = source.FriendlyName;
                    item.DataContext = source;
                    TvSignals.Items.Add(item);
                }
            }
            if (_moduleType == ModuleType.SSM)
                if (Context.AllSignals != null)
                {
                    var sources = Context.SimSupportModules.ToList();
                    foreach (var source in sources)
                    {
                        var item = new TreeViewItem();
                        item.Header = source.FriendlyName;
                        item.DataContext = source;
                        TvSignals.Items.Add(item);
                    }
                }
            if (_moduleType == ModuleType.ALL)
                if (Context.AllSignals != null)
                {
                    var sources = Context.SimSupportModules.ToList();
                    foreach (var source in sources)
                    {
                        var item = new TreeViewItem();
                        item.Header = source.FriendlyName;
                        item.DataContext = source;
                        TvSignals.Items.Add(item);
                    }
                    var sources2 = Context.HardwareSupportModules.ToList();
                    foreach (var source in sources2)
                    {
                        var item = new TreeViewItem();
                        item.Header = source.FriendlyName;
                        item.DataContext = source;
                        TvSignals.Items.Add(item);
                    }
                }
        }

        private void TvSignals_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender != null && sender is TreeView)
            {

                var ss = sender as TreeView;
                if (ss.SelectedItem != null)
                {
                    if (ss.SelectedItem is TreeViewItem)
                    {
                        var tvItem = ss.SelectedItem as TreeViewItem;
                       
                        if (tvItem.DataContext != null && tvItem.DataContext is IHardwareSupportModule)
                        {
                            SelectedHsm = tvItem.DataContext as IHardwareSupportModule;
                            btnConfigure.IsEnabled = true;
                            SelectedSSM = null;
                        }
                        else if (tvItem.DataContext != null && tvItem.DataContext is SimSupportModule)
                        {
                            SelectedSSM = tvItem.DataContext as SimSupportModule;
                            btnConfigure.IsEnabled = true;
                            SelectedHsm = null;
                        }
                        else 
                        {
                            btnConfigure.IsEnabled = false;
                            SelectedHsm = null;
                            SelectedSSM = null;
                        }

                    }

                }
            }
        }

        private void ButtonConfigure_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedHsm != null)
            {
                try
                {
                    SelectedHsm.Configure();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            else if (SelectedSSM != null)
            {
                try
                {
                    SelectedSSM.Configure();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            
        }

        
    }
}
