using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlServerDeleteReorderExample.Classes
{
    public static class Dialogs
    {
        [DebuggerStepThrough]
        public static bool Question(string pText)
        {
            return (MessageBox.Show(
                        pText,
                        Application.ProductName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }
    }
}
