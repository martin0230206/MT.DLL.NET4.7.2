using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.WinForms
{
    public interface ISystemControl
    {
        string GetValue();
        string GetColumnName();
    }
}
