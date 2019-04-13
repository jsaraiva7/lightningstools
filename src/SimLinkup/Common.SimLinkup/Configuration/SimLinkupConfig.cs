using System;
using System.IO;

namespace Common.SimLinkup.Configuration
{
    [Serializable]
    public class SimLinkupConfig
    {
        public uint DesiredFrequency { get; set; } = 60; // default
        public int PlotterTime  { get; set; } = 20000;
        public bool LauchAtSystemStart { get; set; } = false;
        public bool AutoStart { get; set; } = false;
        public bool MinimizeToTray { get; set; } = false;
        public bool MinimizeStart { get; set; } = false;
        public bool EnableCallBacks { get; set; } = true;
        public string HsmDir { get; set; }  
        public string SsmDir { get; set; }
        public string MappingDir { get; set; } = "";

        public SimLinkupConfig()
        {
            HsmDir = GetDir(true);
            SsmDir = GetDir(false);
        }
        private string GetDir(bool isHsm)
        {
            var exeDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                .FileName);
            if (isHsm)
            {
                var path = exeDir + "\\HSM";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                   Directory.CreateDirectory(path);
                }

                return path;
            }
            else
            {
                var path = exeDir + "\\SSM";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
          
        }
        public static SimLinkupConfig GetConfig()
        {
            try
            {
                var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                               .FileName) + "\\Configuration";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var cfgFileName = "Config.simlinkup";

                path += "\\" + cfgFileName;

                var cfg = Serialization.Util.DeserializeFromXmlFile<SimLinkupConfig>(path);
                if (cfg != null)
                {
                    return cfg;
                }
                else
                {
                    return new SimLinkupConfig();
                }
            }
            catch 
            {
                 
            }
           


            //todo - Add dynamic XML loading!
            return new SimLinkupConfig();
        }

        public void SaveConfiguration()
        {
            try
            {
                var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                               .FileName) + "\\Configuration";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var cfgFileName = "Config.simlinkup";

                path += "\\" + cfgFileName;
                Serialization.Util.SerializeToXmlFile(this, path);
            }
            catch (Exception e)
            {
                
            }
        }

    }
}