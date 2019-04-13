using System;
using System.Collections;
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
using Common.SimLinkup.Configuration;
using Common.SimLinkup.Scripting;

namespace SimLinkupControls
{
    /// <summary>
    /// Interação lógica para SignalList.xam
    /// </summary>
    public partial class SignalList : UserControl
    {

        public ScriptingContext Context { get; set; }
        public SignalList<Signal> Signals { get; set; }
        public Signal SelectedSignal { get; private set; }
        public Window _window { get; set; }

        public SignalList()
        {
            InitializeComponent();
             
        }

        public SignalList(ScriptingContext ctx, bool selectButton, Window w)
        {
            try
            {
                _window = w;
                Context = ctx;
                Signals = Context.AllSignals;
                InitializeComponent();
                if (selectButton)
                {
                    btnSelectSignal.Visibility = Visibility.Visible;
                }
                else
                {
                    btnSelectSignal.Visibility = Visibility.Hidden;
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         
            //TvSignals
        }

        public void Initialize(ScriptingContext ctx)
        {
            Context = ctx;
            Signals = Context.AllSignals;
            BuildTreeView();
        }


        private void BuildTreeView()
        {
            if (Signals != null)
            {
                var sources = Signals.GetUniqueSources();
                foreach (var source in sources)
                {
                    var signalsFromSource = Signals.GetSignalsFromSource(source);
                    var item = new TreeViewItem();
                    item.Header = source;


                    foreach (var Categories in signalsFromSource.GetDistinctSignalCategories())
                    {
                        var collItem = new TreeViewItem();
                        collItem.Header = Categories;
                        var signalsFromCollection = signalsFromSource.GetSignalsByCategory(Categories);
                        var collections = signalsFromCollection.GetDistinctSignalCollectionNames();


                        foreach (var collection in collections)
                        {
                            var subitem = new TreeViewItem();
                            subitem.Header = collection;

                            var sig = signalsFromSource.GetSignalsByCategory(Categories)
                                .GetSignalsByCollection(collection);
                            subitem.DataContext = sig;
                            collItem.Items.Add(subitem);
                            
                        }

                        item.Items.Add(collItem);
                    }
                  

                    TvSignals.Items.Add(item);
                }
            }
        }

        private void SignalList_OnLoaded(object sender, RoutedEventArgs e)
        {



        }

        private void TreeView_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            var source = LvSignals.Items;
            LvSignals.UnselectAll();
        
            
            while (LvSignals.Items.Count >0)
            {
                LvSignals.Items.Remove(LvSignals.Items[0]);
                LvSignals.Items.Refresh();
            }


            LvSignals.SelectionMode = SelectionMode.Single;

            if (sender != null && sender is TreeView)
            {

                var ss = sender as TreeView;
                if (ss.SelectedItem != null)
                {
                    if (ss.SelectedItem is TreeViewItem)
                    {
                        var tvItem = ss.SelectedItem as TreeViewItem;
                        //if (tvItem.DataContext != null && tvItem.DataContext is Signal)
                        //{
                        //    var typedSignal = tvItem.DataContext as Signal;
                        //    Plotter.MonitorSignal(typedSignal, SimLinkupConfig.GetConfig().PlotterTime);
                        //}
                        if (tvItem.DataContext != null && tvItem.DataContext is SignalList<Signal>)
                        {
                            var lst = tvItem.DataContext as SignalList<Signal>;

                            var mdls = lst.Select(c => new SignalModel()
                            {
                                Category = c.Category,
                                CollectionName = c.CollectionName,
                                FriendlyName = c.FriendlyName,
                                Id = c.Id
                            });
                            foreach (var signal in mdls)
                            {
                                LvSignals.Items.Add(signal);

                            }

                        }

                    }

                }
            }
        }

        private void ListView_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {

            var tvItem = sender as ListView;
            if (tvItem != null)
            {
                if (tvItem.SelectedItem is SignalModel signalModel)
                {
                    
                    var signal = Signals.FirstOrDefault(c => c.Id.Equals(signalModel.Id));
                    if (signal != null)
                    {
                        Plotter.MonitorSignal(signal, SimLinkupConfig.GetConfig().PlotterTime);
                        SelectedSignal = signal;
                    }
                    
                    
                }
            }
            LvSignals.SelectionMode = SelectionMode.Single;
      

            
        }


        private void BtnSelectSignal_OnClick(object sender, RoutedEventArgs e)
        {
            _window.Close();
        }
    }

    class SignalModel
    {
        public string FriendlyName { get; set; }
        public string CollectionName { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
    }
}
