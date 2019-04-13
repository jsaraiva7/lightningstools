using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using Common.SimLinkup.Configuration;

namespace SimLinkupV2
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var cfg = SimLinkupConfig.GetConfig();
            if (cfg.EnableCallBacks && !IsRunAsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Assembly.GetEntryAssembly().CodeBase;

            

                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    MessageBox.Show("This application requires elevated credentials in order to properly support callbacks.");
                }
            }
            else
            {
                //Normal program logic...
            }
        }
        private static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
