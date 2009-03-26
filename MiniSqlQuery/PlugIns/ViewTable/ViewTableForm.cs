using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	public partial class ViewTableForm: DockContent
	{
		IApplicationServices _services;
		string _status = string.Empty;
		DataSet _result;
		private DbConnection _dbConnection;
		private DatabaseMetaDataService _metaDataService;

		public ViewTableForm()
		{
			InitializeComponent();
		}

		public ViewTableForm(IApplicationServices services, string tableName)
			: this()
		{
			_services = services;
			TableName = tableName;

			_services.Settings.DatabaseConnectionReset += new EventHandler(Settings_DatabaseConnectionReset);

			if (_metaDataService == null)
			{
				_metaDataService = new DatabaseMetaDataService(
					_services.Settings.ProviderFactory,
					_services.Settings.ConnectionDefinition.ConnectionString);

				DataTable schema = _metaDataService.GetSchema();
				List<string> tableNames = new List<string>();
				foreach (DataRow row in schema.Rows)
				{
					string schemaName = string.Format("{0}", row["Schema"]);
					string table = string.Format("{0}", row["Table"]);
					string fullTableName = null;

					if (string.IsNullOrEmpty(schemaName))
					{
						fullTableName = table;
					}
					else
					{
						fullTableName = string.Concat(schemaName, ".", table);
					}

					if (!tableNames.Contains(fullTableName))
					{
						tableNames.Add(fullTableName);
					}
				}

				cboTableName.Items.AddRange(tableNames.ToArray());
			}


		}

		void Settings_DatabaseConnectionReset(object sender, EventArgs e)
		{
			_dbConnection = null;
		}

		private void ViewTableForm_Load(object sender, EventArgs e)
		{
			LoadTableData();


		}

		public string TableName
		{
			get
			{
				return cboTableName.Text;
			}
			set
			{
				cboTableName.Text = value;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				TabText = Text;
			}
		}

		public void SetStatus(string text)
		{
			_status = text;
			UpdateHostStatus();
		}

		protected void UpdateHostStatus()
		{
			ApplicationServices.Instance.HostWindow.SetStatus(this, _status);
		}

		private void LoadTableData()
		{
			DbDataAdapter adapter = null;
			DbCommand cmd = null;
			//bool errored = false;
			DataTable dt = null;

			if (string.IsNullOrEmpty(TableName))
			{
				return;
			}
		
			try
			{
				_services.HostWindow.SetPointerState(Cursors.WaitCursor);
				if (_dbConnection == null)
				{
					_dbConnection = _services.Settings.GetOpenConnection();
					_dbConnection.StateChange += new StateChangeEventHandler(_dbConnection_StateChange);
				}

				_result = new DataSet();
				adapter = _services.Settings.ProviderFactory.CreateDataAdapter();
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT * FROM " + TableName;
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;
				adapter.Fill(_result);
			}
			catch (DbException dbExp)
			{
				// todo: improve!
				ApplicationServices.Instance.HostWindow.DisplaySimpleMessageBox(this, dbExp.Message, "View Table Error");
				SetStatus(dbExp.Message);
			}
			finally
			{
				if (adapter != null)
				{
					adapter.Dispose();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
				_services.HostWindow.SetPointerState(Cursors.Default);
			}

			if (_result != null && _result.Tables.Count > 0)
			{
				dt = _result.Tables[0];
				Text = "Table: " + TableName;
			}

			dataGridViewResult.DataSource = dt;
		}

		void _dbConnection_StateChange(object sender, StateChangeEventArgs e)
		{
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadTableData();
		}

		private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			LoadTableData();
		}

		private void dataGridViewResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private void queryToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

	}
}