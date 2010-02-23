using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	public partial class FindObjectForm : Form
	{
		private readonly IApplicationServices _services;
		private readonly IDatabaseInspector _databaseInspector;

		public FindObjectForm(IApplicationServices services, IDatabaseInspector databaseInspector)
		{
			_services = services;
			_databaseInspector = databaseInspector;
			InitializeComponent();
		}

		public string SelectedTableName
		{
			get { return cboObjects.Text; }
		}

		private void FindObjectForm_Load(object sender, EventArgs e)
		{

		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
					DialogResult = DialogResult.Cancel;
					Close();
					break;
				case Keys.Enter:
					DialogResult = DialogResult.OK;
					Close();
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void FindObjectForm_Shown(object sender, EventArgs e)
		{
			AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
			if (_databaseInspector.DbSchema == null)
			{
				_databaseInspector.LoadDatabaseDetails();
			}

			foreach (DbModelTable table in _databaseInspector.DbSchema.Tables)
			{
				collection.Add(table.FullName);
			}

			cboObjects.AutoCompleteCustomSource = collection;
		}

		//private void cboObjects_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    IDbModelNamedObject obj = null;
		//    _databaseInspector.DbSchema.FindTable(cboObjects.SelectedItem.ToString());
		//    _databaseInspector.NavigateTo(obj);
		//}
	}
}
