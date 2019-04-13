using System;
using System.Collections.Generic;
using System.Globalization;

namespace PhccSupportModule.ConfigUi
{
    /// <summary>
    /// Interação lógica para PeripheralAddress.xam
    /// </summary>
    public partial class PeripheralAddress : UserControl
    {
        
        
        public byte Address { get; set; }
        public string FriendlyName { get; set; }
        public Window ParentWindow { get; set; }
        public List<byte> ProhibitedBaseAddresses { get; set; }
        public bool Cancel { get; set; } = false;
        

        public PeripheralAddress(Window parent)
        {
            ParentWindow = parent;
            InitializeComponent();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidateBaseAddress())
            {
                ParentWindow?.Close();
            }
            else
            {
                
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Cancel = true;
            ParentWindow?.Close();
        }

     

        private bool ValidateBaseAddress()
        {
        
            var baseAddressAsEntered = tbAddress.Text;
            byte val = 0;
            var valid = TryParseBaseAddress(baseAddressAsEntered, out val);
            if (!valid)
            {

                MessageBox.Show(
                    "Invalid hexadecimal or decimal byte value.\nHex values should be preceded by the\ncharacters '0x' (zero, x) " +
                    "and\nshould be in the range 0x00-0xFF.\nDecimal values should be in the range 0-255.");

            }
            else
            {
                if (ProhibitedBaseAddresses != null)
                {
                    foreach (var prohibitedAddress in ProhibitedBaseAddresses)
                    {
                        if (prohibitedAddress == val)
                        {
                            valid = false;
                            MessageBox.Show(
                               "That peripheral address is already being used by another peripheral.");
                            break;
                        }
                    }
                }
                if (valid)
                {
                    Address = val;
                }
            }
            return valid;
        }

        private bool TryParseBaseAddress(string baseAddress, out byte val)
        {
            var valid = true;
            val = 0;
            if (!String.IsNullOrEmpty(baseAddress))
            {
                baseAddress = baseAddress.Trim();
            }
            if (String.IsNullOrEmpty(baseAddress))
            {
                valid = false;
            }
            else
            {
                if (baseAddress.StartsWith("0x") || baseAddress.StartsWith("0X"))
                {
                    baseAddress = baseAddress.Substring(2, baseAddress.Length - 2);
                    valid = Byte.TryParse(baseAddress, NumberStyles.HexNumber, null, out val);
                }
                else
                {
                    valid = Byte.TryParse(baseAddress, out val);
                }
            }
            return valid;
        }
    }
}
