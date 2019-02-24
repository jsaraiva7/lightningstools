using System.Collections.Generic;

namespace Profile_Manager.HSM
{  
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class HardwareSupportModuleRegistry
    {

        private List<string> hardwareSupportModulesField;

        /// <observações/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Module", IsNullable = false)]
        public List<string> HardwareSupportModules
        {
            get
            {
                return this.hardwareSupportModulesField;
            }
            set
            {
                this.hardwareSupportModulesField = value;
            }
        }
    }
}