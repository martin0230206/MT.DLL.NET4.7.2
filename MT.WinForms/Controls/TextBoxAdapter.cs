using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT.WinForms
{
    public class TextBoxAdapter : TextBox, ISystemControl
    {
        public string GetColumnName() => Tag == null ? "" : Tag.ToString();

        public string GetValue() => this.Text;
    }
}
