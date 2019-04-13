using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Common.SimLinkup.Configuration;
using Common.SimLinkup.Runtime;
using log4net;
using MahApps.Metro.Controls;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using SimLinkupControls;
using SimLinkupControls.Mapping;
using SimLinkupV2.Ui.Options;
using Application = System.Windows.Forms.Application;
 

namespace SimLinkupV2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MainWindow));
        public Runtime _runtime;
        private SimLinkupConfig _cfg;
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        private WindowState m_storedWindowState = WindowState.Normal;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _cfg = SimLinkupConfig.GetConfig();
             
                _runtime = new Runtime();
                if (_cfg.AutoStart)
                    StartRuntime();
                stop.IsEnabled = false;
                m_notifyIcon = new System.Windows.Forms.NotifyIcon();
                m_notifyIcon.BalloonTipText = "SimLinkup has been minimised. Click the tray icon to open.";
                m_notifyIcon.BalloonTipTitle = "SimLinkup";
                m_notifyIcon.Text = "SimLinkup";
                m_notifyIcon.Icon = SimLinkupV2.Properties.Resources.simlinkup_1f8_icon;
                m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
                UpdateWindowsStartupRegKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        #region Runtime
        private void StartRuntime()
        {
            if (_cfg.MinimizeStart && !_cfg.MinimizeToTray)
                this.WindowState = WindowState.Minimized;

            if (_cfg.MinimizeToTray)
            {
                this.WindowState = WindowState.Minimized;
            }

            _runtime.Start();
        }
        void OnClose(object sender, CancelEventArgs args)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }


        #endregion



        #region TrayIcon



        private void UpdateWindowsStartupRegKey()
        {
            if (_cfg.LauchAtSystemStart)
            {
                //update the Windows Registry's Run-at-startup applications list according
                //to the new user settings
                var c = new Computer();
                try
                {
                    using (
                        var startupKey =
                            c.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                    {
                        startupKey?.SetValue(System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.ExecutablePath,
                            RegistryValueKind.String);
                    }
                }
                catch (Exception ex)
                {
                    _log.Debug(ex.Message, ex);
                }
            }
            else
            {
                var c = new Computer();
                try
                {
                    using (
                        var startupKey =
                            c.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                    {
                        startupKey?.DeleteValue(Application.ProductName, false);
                     
                    }
                }
                catch (Exception ex)
                {
                    _log.Debug(ex.Message, ex);
                }
            }
             
        }

 

        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                if (_cfg.MinimizeToTray)
                {
                    Hide();
                    if (m_notifyIcon != null)
                        m_notifyIcon.ShowBalloonTip(2000);
                }
                else
                {
                    m_storedWindowState = WindowState;
                }
              
            }
            else
                m_storedWindowState = WindowState;
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }
        #endregion

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
    
            SignalList.Initialize(_runtime.ScriptingContext);

        }

        #region MenuHandlers

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void mnuStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StartRuntime();
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            DisableUiElements(true);
        }

      
        private void mnuStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _runtime.Stop();

            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            DisableUiElements(false);
        }
        private void DisableUiElements(bool disable)
        {
            if (disable)
            {
                mnuFile.IsEnabled = false;
                mnuOptions.IsEnabled = false;
                mnuSignals.IsEnabled = false;
                stop.IsEnabled = true;
                start.IsEnabled = false;
            }
            else
            {
                start.IsEnabled = true;
                stop.IsEnabled = false;
                mnuFile.IsEnabled = true;
                mnuOptions.IsEnabled = true;
                mnuSignals.IsEnabled = true;
            }

        }
        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_runtime.IsRunning)
                {
                    var r = new Options();
                    r.ShowDialog();
                    _cfg = SimLinkupConfig.GetConfig();


                    System.Windows.Application.Current.Shutdown();

                }
                else
                {
                     MessageBox.Show("Please Stop SimLinkup First!");
                }
              
            }
            catch (Exception exception)
            {
                LogError(exception);
            }

           
        }
        #endregion

        #region Logger

        private void LogError(Exception ex)
        {
            throw ex;
        }

        #endregion

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ShowSupportModules(ModuleType.HSM);

        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuMapper_OnClick(object sender, RoutedEventArgs e)
        {
              Window w = new Window();
              MainPanel.Children.Remove(SignalList);
              Mapper p = new Mapper(w, _runtime, SignalList);
              w.Content = p;
              w.ShowDialog();
              var mainnn = SignalList.Parent;
              var child = mainnn.GetChildObjects().ToList();
              child.Remove(SignalList);
               
              MainPanel.Children.Add(SignalList);

        }

        private void SsmMnu_OnClick(object sender, RoutedEventArgs e)
        {
            ShowSupportModules(ModuleType.SSM);
        }

        private void ShowSupportModules(ModuleType mType)
        {
            try
            {
                ModuleList l = new ModuleList(_runtime.ScriptingContext, mType);
                Window w = new Window();
                w.Content = l;
                w.ShowDialog();
                w.Close();

                MessageBox.Show("Changes will take effect after a restart.\nPlease Restart the Application");
                // System.Windows.Application.Current.Shutdown();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString());
            }
        }
    }
}
