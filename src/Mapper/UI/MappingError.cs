using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapper.UI
{
    public partial class MappingError : Form
    {
        private string DisplayText { get; set; }
        public MappingError(string text)
        {
            InitializeComponent();
            DisplayText = text;
        }

        private void MappingError_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = DisplayText;
            richTextBox1.Refresh();
        }
    }
}
