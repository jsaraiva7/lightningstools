using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace GlobalConfiguration
{
    public class F4Config
    {
        public string F4ExeFile { get; set; } = "";
        public string Callsign { get; set; } = "";
        public string KeyFile { get; set; } = "";


        public static F4Config GetConfig()
        {
            try
            {
                var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                               .FileName);
                
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string[] files = Directory.GetFiles(path, "Config.f4ssm", SearchOption.AllDirectories);

                if (files.Length < 1)
                {
                    return new F4Config();
                }
                var cfg = Common.Serialization.Util.DeserializeFromXmlFile<F4Config>(files[0]);
                if (cfg != null)
                {
                    return cfg;
                }
                else
                {
                    return new F4Config();
                }
            }
            catch
            {

            }
            //todo - Add dynamic XML loading!
            return new F4Config();
        }

        public void SaveConfiguration()
        {
            try
            {
                var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                               .FileName);

                string[] files = Directory.GetFiles(path, "F4Utils.SimSupport.dll", SearchOption.AllDirectories);

                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }



                var cfgFileName = "Config.f4ssm";
                
                var dd = new DirectoryInfo(files[0]);
                var pth = dd.Parent.FullName + "\\" + cfgFileName;
                Common.Serialization.Util.SerializeToXmlFile(this, pth);
            }
            catch (Exception e)
            {

            }
        }
    }
}