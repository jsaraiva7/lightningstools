using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Generic;
using F4KeyFile;
using GlobalConfiguration;

namespace F4Utils.SimSupport.Configuration.CallBackConfig
{
    public class CallBackModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Category { get; set; } = "";
        public string SubCategory { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string Description { get; set; } = "";

        public string CallBack { get; set; } = "";

        public static CallBackModel CallBackResolver(List<CallBackModel> callbackList, string desiredCallBack)
        {
            return callbackList.FirstOrDefault(v => v.CallBack.ToLower().Contains(desiredCallBack.ToLower()));
        }

        public static List<CallBackModel> ConvertCallback()
        {
            var list = new List<CallBackModel>();
            foreach (Callbacks callback in Enum.GetValues(typeof(Callbacks)))
            {
                var category = EnumAttributeReader.GetAttribute<CategoryAttribute>(callback).Category;
                var subCategory = EnumAttributeReader.GetAttribute<SubCategoryAttribute>(callback).SubCategory;
                var description = EnumAttributeReader.GetAttribute<DescriptionAttribute>(callback).Description;
                var shortDesc = EnumAttributeReader.GetAttribute<ShortDescriptionAttribute>(callback).ShortDescription;
                var mdl = new CallBackModel()
                    {CallBack = callback.ToString(), Category = category, Description = description, SubCategory = subCategory, ShortDescription = shortDesc };

              list.Add(mdl);
                 
            }

            return list;
        }
      
        public static List<CallBackModel> GetCallBacks()
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
                string[] files = Directory.GetFiles(path, "Callbacks.f4ssm", SearchOption.AllDirectories);

                if (files.Length < 1)
                {
                    return new List<CallBackModel>();
                }
                var cfg = Common.Serialization.Util.DeserializeFromXmlFile<List<CallBackModel>>(files[0]);
                if (cfg != null)
                {
                    return cfg;
                }
                else
                {
                    return new List<CallBackModel>();
                }
            }
            catch
            {

            }
            
            return  new List<CallBackModel>();
        }

        public static void SaveCallBackList(List<CallBackModel> callbacks)
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



                var cfgFileName = "Callbacks.f4ssm";

                var dd = new DirectoryInfo(files[0]);
                var pth = dd.Parent.FullName + "\\" + cfgFileName;
                Common.Serialization.Util.SerializeToXmlFile(callbacks, pth);
            }
            catch (Exception e)
            {

            }
        }

    }
}