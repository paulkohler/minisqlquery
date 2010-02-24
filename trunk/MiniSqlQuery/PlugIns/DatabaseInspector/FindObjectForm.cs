using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	public partial class FindObjectForm : Form
	{
		private readonly IDatabaseInspector _databaseInspector;

		public FindObjectForm(IDatabaseInspector databaseInspector)
		{
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
			var collection = new AutoCompleteStringCollection();

			try
			{
				UseWaitCursor = true;

				if (_databaseInspector.DbSchema == null)
				{
					_databaseInspector.LoadDatabaseDetails();
				}

				foreach (DbModelTable table in _databaseInspector.DbSchema.Tables)
				{
					collection.Add(table.FullName);
				}
				foreach (DbModelView view in _databaseInspector.DbSchema.Views)
				{
					collection.Add(view.FullName);
				}
			}
			finally
			{
				UseWaitCursor = false;
			}

			cboObjects.AutoCompleteCustomSource = collection;
		}
	}
}