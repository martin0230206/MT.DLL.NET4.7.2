using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT.WinForms
{
    public partial class SingleTableForm : Form
    {
        public SingleTableForm()
        {
            InitializeComponent();
        }

        protected virtual Dictionary<string, string> GetFormData()
        {
            var result = new Dictionary<string, string>();
            foreach (ISystemControl control in this.Controls.OfType<ISystemControl>())
            {
                result.Add(control.GetColumnName(), control.GetValue());
            }
            return result;
        }

        protected virtual string GenerateInsertSql(string tableName)
        {
            var formData = GetFormData();
            var columns = string.Join(", ", formData.Keys);
            var values = "@" + string.Join("', @", formData.Values);

            string sql = $"INSERT INTO {tableName} ({columns}) VALUES({values})";
            return sql;
        }
    }
}
