using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.SimLinkup.Configuration;

namespace PhccHardwareSupportModule
{
    /// <summary>
    /// needs refactoring?
    /// </summary>
    internal static class Util
    {
       
    
        public static string DefaultConfig
        {
            get
            {
                var cfg = SimLinkupConfig.GetConfig().HsmDir;
               
                var files = Directory.GetFiles(cfg, "phcc.config", SearchOption.AllDirectories).FirstOrDefault();
                if (files != null || files.Length > 1)
                {
                    return files;
                }
                else
                {
                    throw new Exception("Error - PHCC HSM Config File not found! \nPlease configure PHCC and save file on HSM modules Folder!");
                }
                
            }
        }

      
 
    }
}