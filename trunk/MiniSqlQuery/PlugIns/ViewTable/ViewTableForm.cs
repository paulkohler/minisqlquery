using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	public partial class ViewTableForm : DockContent, IPerformTask, IQueryBatchProvider, INavigatableDocument
	{
		private QueryBatch _batch;
		private DbConnection _dbConnection;
		private bool _isBusy;
		private IDatabaseSchemaService _metaDataService;
		private IApplicationServices _services;
		private string _status = string.Empty;
		private object _syncLock = new object();

		public ViewTableForm()
		{
			InitializeComponent();
		}

		public ViewTableForm(IApplicationServices services, string tableName)
			: this()
		{
			_services = services;
			_batch = new QueryBatch();
			TableName = tableName;

			_services.Settings.DatabaseConnectionReset += SettingsDatabaseConnectionReset;
			_services.SystemMessagePosted += ServicesSystemMessagePosted;
		}

		public string TableName
		{
			get { return cboTableName.Text; }
			set { cboTableName.Text = value; }
		}

		public override string Text
		{
			get { return base.Text; }
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

		#region IPerformTask Members

		public void ExecuteTask()
		{
			LoadTableData();
		}

		public void CancelTask()
		{
			// not supported (yet?)
		}

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				lock (_syncLock)
				{
					_isBusy = value;
				}
			}
		}

		#endregion

		#region IQueryBatchProvider Members

		public QueryBatch Batch
		{
			get { return _batch; }
		}

		#endregion

		private void ServicesSystemMessagePosted(object sender, SystemMessageEventArgs e)
		{
			if (e.Message == SystemMessage.TableTruncated)
			{
				if (AutoReload && TableName.Equals(e.Data))
				{
					LoadTableData();
				}
			}
		}

		private void SettingsDatabaseConnectionReset(object sender, EventArgs e)
		{
			_dbConnection = null;
		}

		private void ViewTableForm_Shown(object sender, EventArgs e)
		{
			LoadTableData();
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
			GetTablesAndViews();

			DbDataAdapter adapter = null;
			DbCommand cmd = null;
			DataTable dt = null;
			Query query = new Query("SELECT * FROM " + TableName);

			if (string.IsNullOrEmpty(TableName))
			{
				Text = "Table: (none)";
				return;
			}

			try
			{
				IsBusy = true;
				UseWaitCursor = true;
				dataGridViewResult.DataSource = null;
				Application.DoEvents();

				if (_dbConnection == null)
				{
					_dbConnection = _services.Settings.GetOpenConnection();
				}

				query.Result = new DataSet(TableName + " View");
				_batch.Clear();
				_batch.Add(query);

				adapter = _services.Settings.ProviderFactory.CreateDataAdapter();
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = query.Sql;
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;
				adapter.Fill(query.Result);
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
				UseWaitCursor = false;
				IsBusy = false;
			}

			if (query.Result != null && query.Result.Tables.Count > 0)
			{
				dt = query.Result.Tables[0];
				Text = "Table: " + TableName;
			}

			dataGridViewResult.DataSource = dt;
			dataGridViewResult.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
		}

		private void GetTablesAndViews()
		{
			if (_metaDataService == null)
			{
				_metaDataService = DatabaseMetaDataService.Create(_services.Settings.ConnectionDefinition.ProviderName);

				DbModelInstance model = _metaDataService.GetDbObjectModel(_services.Settings.ConnectionDefinition.ConnectionString);
				List<string> tableNames = new List<string>();
				model.Tables.ForEach(t => tableNames.Add(Utility.MakeSqlFriendly(t.FullName)));
				cboTableName.Items.AddRange(tableNames.ToArray());
			}
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

		#region Implementation of ISupportCursorOffset

		public int CursorOffset
		{
			get { return CursorLine; }
		}

		#endregion

		#region Implementation of INavigatableDocument

		public bool SetCursorByOffset(int offset)
		{
			return SetCursorByLocation(offset, 0);
		}

		public bool SetCursorByLocation(int line, int column)
		{
			if (line > 0 && line <= TotalLines)
			{
				dataGridViewResult.FirstDisplayedScrollingRowIndex = line;
				dataGridViewResult.Refresh();
				dataGridViewResult.Rows[line].Selected = true;
				return true;
			}
			return false;
		}

		public int CursorLine
		{
			get { return (dataGridViewResult.CurrentRow != null) ? dataGridViewResult.CurrentRow.Index : 0; }
		}

		public int CursorColumn
		{
			get { return dataGridViewResult.SelectedCells.Count > 0 ? dataGridViewResult.SelectedCells[0].ColumnIndex : 0; }
		}

		public int TotalLines
		{
			get { return (dataGridViewResult.DataSource != null) ? dataGridViewResult.Rows.Count : 0; }
		}

		#endregion
	}
}