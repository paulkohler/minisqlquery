using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn.Tests
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ConnectionStringBuilderForm frm = new ConnectionStringBuilderForm();
			//frm.ProviderName = "System.Data.SqlClient";
			frm.ShowDialog();
			MessageBox.Show(frm.DialogResult.ToString());
			MessageBox.Show(frm.ToString());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ConnectionStringBuilderForm frm = new ConnectionStringBuilderForm(
				"Test Conn", "System.Data.SqlClient", "Server=.; Database=master; Integrated Security=SSPI");
			frm.ShowDialog();
			MessageBox.Show(frm.DialogResult.ToString());
			MessageBox.Show(frm.ToString());
		}
	}
}
