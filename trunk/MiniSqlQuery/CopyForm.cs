using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MiniSqlQuery {

    public partial class CopyForm : Form {

        public CopyForm() 
        {
            InitializeComponent();
            rbTAB.Checked = true;
        }

        public string Delimiter 
        {
            get 
            {
                if (rbCSV.Checked) return ",";
                if (rbTAB.Checked) return "\t";
                return txtOther.Text;
            }
        }

        public bool IncludeHeaders 
        {
            get 
            { 
                return xbIncludeHeaders.Checked; 
            }
        }

        private void okButton_Click(object sender, EventArgs e) 
        {

            Close();

        }

        private void cancelButton_Click(object sender, EventArgs e) 
        {

            Close();

        }

    }

}
