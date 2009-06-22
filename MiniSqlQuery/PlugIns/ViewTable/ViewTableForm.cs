using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.Common;
using MiniSqlQuery.Commands;
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

			_services.Settings.DatabaseConnectionReset += SettingsDatabaseConnectionReset;
			_services.SystemMessagePosted += ServicesSystemMessagePosted;

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
					string fullTableName;

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

		void ServicesSystemMessagePosted(object sender, SystemMessageEventArgs e)
		{
			if (e.Message == SystemMessage.TableTruncated)
			{
				if (TableName.Equals(e.Data) && AutoReload)
				{
					LoadTableData();
				}
			}
		}

		void SettingsDatabaseConnectionReset(object sender, EventArgs e)
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

		public bool AutoReload
		{
			get { return chkAutoReload.Checked; }
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
				Text = "Table: (none)";
				return;
			}
		
			try
			{
				_services.HostWindow.SetPointerState(Cursors.WaitCursor);
				if (_dbConnection == null)
				{
					_dbConnection = _services.Settings.GetOpenConnection();
					_dbConnection.StateChange += DbConnectionStateChange;
				}

				_result = new DataSet();
				adapter = _services.Settings.ProviderFactory.CreateDataAdapter();
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT * FROM " + TableName;
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;
				adapter.Fill(_result);
				SetStatus(string.Format("Loaded table '{0}'", TableName));
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

		void DbConnectionStateChange(object sender, StateChangeEventArgs e)
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