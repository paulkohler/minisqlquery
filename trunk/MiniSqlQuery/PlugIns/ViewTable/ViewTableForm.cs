#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	/// <summary>The view table form.</summary>
	public partial class ViewTableForm : DockContent, IViewTable
	{
		/// <summary>The _batch.</summary>
		private readonly QueryBatch _batch;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _settings.</summary>
		private readonly IApplicationSettings _settings;

		/// <summary>The _sync lock.</summary>
		private readonly object _syncLock = new object();

		/// <summary>The _db connection.</summary>
		private DbConnection _dbConnection;

		/// <summary>The _is busy.</summary>
		private bool _isBusy;

		/// <summary>The _meta data service.</summary>
		private IDatabaseSchemaService _metaDataService;

		/// <summary>The _status.</summary>
		private string _status = string.Empty;

		private int? _rowCount;

		/// <summary>Initializes a new instance of the <see cref="ViewTableForm"/> class.</summary>
		public ViewTableForm()
		{
			InitializeComponent();
		}

		/// <summary>Initializes a new instance of the <see cref="ViewTableForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public ViewTableForm(IApplicationServices services, IApplicationSettings settings)
			: this()
		{
			_services = services;
			_settings = settings;
			_batch = new QueryBatch();
			TableName = string.Empty;
			Text = Resources.ViewData;

			dataGridViewResult.DefaultCellStyle.NullValue = _settings.NullText;
			dataGridViewResult.DataBindingComplete += DataGridViewResultDataBindingComplete;
			_services.Settings.DatabaseConnectionReset += SettingsDatabaseConnectionReset;
			_services.SystemMessagePosted += ServicesSystemMessagePosted;
		}

		/// <summary>Gets a value indicating whether AutoReload.</summary>
		public bool AutoReload
		{
			get { return chkAutoReload.Checked; }
		}

		/// <summary>Gets Batch.</summary>
		public QueryBatch Batch
		{
			get { return _batch; }
		}

		/// <summary>Gets CursorColumn.</summary>
		public int CursorColumn
		{
			get { return dataGridViewResult.SelectedCells.Count > 0 ? dataGridViewResult.SelectedCells[0].ColumnIndex : 0; }
		}

		/// <summary>Gets CursorLine.</summary>
		public int CursorLine
		{
			get { return (dataGridViewResult.CurrentRow != null) ? dataGridViewResult.CurrentRow.Index : 0; }
		}

		/// <summary>Gets CursorOffset.</summary>
		public int CursorOffset
		{
			get { return CursorLine; }
		}

		/// <summary>Gets or sets a value indicating whether IsBusy.</summary>
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

		/// <summary>Gets or sets TableName.</summary>
		public string TableName
		{
			get { return cboTableName.Text; }
			set { cboTableName.Text = value; }
		}

		/// <summary>Gets or sets Text.</summary>
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				TabText = Text;
			}
		}

		/// <summary>Gets TotalLines.</summary>
		public int TotalLines
		{
			get { return (dataGridViewResult.DataSource != null) ? dataGridViewResult.Rows.Count : 0; }
		}

		/// <summary>The set status.</summary>
		/// <param name="text">The text.</param>
		public void SetStatus(string text)
		{
			_status = text;
			UpdateHostStatus();
		}

		public void SetRowCount(int? rows)
		{
			_rowCount = rows;
			UpdateHostStatus();
		}

		/// <summary>The set cursor by location.</summary>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <returns>The set cursor by location.</returns>
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

		/// <summary>The set cursor by offset.</summary>
		/// <param name="offset">The offset.</param>
		/// <returns>The set cursor by offset.</returns>
		public bool SetCursorByOffset(int offset)
		{
			return SetCursorByLocation(offset, 0);
		}

		/// <summary>The cancel task.</summary>
		public void CancelTask()
		{
			// not supported (yet?)
		}

		/// <summary>The execute task.</summary>
		public void ExecuteTask()
		{
			LoadTableData();
		}

		/// <summary>The update host status.</summary>
		protected void UpdateHostStatus()
		{
			_services.HostWindow.SetStatus(this, _status);
			_services.HostWindow.SetResultCount(this, _rowCount);
		}

		/// <summary>The data grid view result data binding complete.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DataGridViewResultDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			DataTable dt = dataGridViewResult.DataSource as DataTable;
			if (dt == null)
			{
				return;
			}

			string nullText = _settings.NullText;
			string dateTimeFormat = _settings.DateTimeFormat;
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				if (dt.Columns[i].DataType == typeof(DateTime))
				{
					DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle();
					dateCellStyle.NullValue = nullText;
					dateCellStyle.Format = dateTimeFormat;
					dataGridViewResult.Columns[i].DefaultCellStyle = dateCellStyle;
				}
			}
		}

		/// <summary>The get tables and views.</summary>
		private void GetTablesAndViews()
		{
			if (_metaDataService == null)
			{
				_metaDataService = DatabaseMetaDataService.Create(_services.Settings.ConnectionDefinition.ProviderName);

				DbModelInstance model = _metaDataService.GetDbObjectModel(_services.Settings.ConnectionDefinition.ConnectionString);
				List<string> tableNames = new List<string>();
				foreach (DbModelTable table in model.Tables)
				{
					tableNames.Add(Utility.MakeSqlFriendly(table.FullName));
				}

				foreach (DbModelView view in model.Views)
				{
					tableNames.Add(Utility.MakeSqlFriendly(view.FullName));
				}

				cboTableName.Items.AddRange(tableNames.ToArray());
			}
		}

		/// <summary>The load table data.</summary>
		private void LoadTableData()
		{
			GetTablesAndViews();

			DbDataAdapter adapter = null;
			DbCommand cmd = null;
			DataTable dt = null;
			Query query = new Query("SELECT * FROM " + Utility.MakeSqlFriendly(TableName));

			if (string.IsNullOrEmpty(TableName))
			{
				Text = Resources.Table_none;
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
				SetCommandTimeout(cmd, _settings.CommandTimeout);
				adapter.SelectCommand = cmd;
				adapter.Fill(query.Result);
				SetStatus(string.Format("Loaded table '{0}'", TableName));
				Text = TableName;
			}
			catch (DbException dbExp)
			{
				// todo: improve!
				_services.HostWindow.DisplaySimpleMessageBox(this, dbExp.Message, "View Table Error");
				SetStatus(dbExp.Message);
				Text = Resources.ViewDataError;
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
				Text = Resources.Table_colon + TableName;
			}

			dataGridViewResult.DefaultCellStyle.NullValue = _settings.NullText;
			dataGridViewResult.DataSource = dt;
			dataGridViewResult.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

			if (dt != null)
			{
				SetRowCount(dt.Rows.Count);
			}
		}


		/// <summary>
		/// Sets the command timeout, currently only tested against MSSQL.
		/// </summary>
		/// <param name="cmd">The command.</param>
		/// <param name="commandTimeout">The command timeout.</param>
		private void SetCommandTimeout(IDbCommand cmd, int commandTimeout)
		{
			if (_services.Settings.ProviderFactory is SqlClientFactory)
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				cmd.CommandTimeout = commandTimeout;
			}
			else
			{
				Trace.WriteLine("Command Timeout only supported by SQL Client (so far)");
			}
		}

		/// <summary>The services system message posted.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The settings database connection reset.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void SettingsDatabaseConnectionReset(object sender, EventArgs e)
		{
			_dbConnection = null;
		}

		/// <summary>The update status.</summary>
		/// <param name="msg">The msg.</param>
		private void UpdateStatus(string msg)
		{
			_services.HostWindow.SetStatus(this, msg);
			Application.DoEvents();
		}

		/// <summary>The view table form_ shown.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ViewTableForm_Shown(object sender, EventArgs e)
		{
			LoadTableData();
		}

		/// <summary>The data grid view result_ data error.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void dataGridViewResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		/// <summary>The lnk export script_ link clicked.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lnkExportScript_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DataTable dt = dataGridViewResult.DataSource as DataTable;

			if (dt != null)
			{
				var stringWriter = new StringWriter();
				var hostWindow = _services.HostWindow;
				var dbModelTable = hostWindow.DatabaseInspector.DbSchema.FindTableOrView(TableName);
				var sqlWriter = _services.Resolve<ISqlWriter>();
				sqlWriter.IncludeComments = false;
				sqlWriter.InsertLineBreaksBetweenColumns = false;
				sqlWriter.IncludeReadOnlyColumnsInExport = _settings.IncludeReadOnlyColumnsInExport;

				for (int i = 0; i < dt.Rows.Count; i++)
				{
					DataRow dataRow = dt.Rows[i];

					foreach (var column in dbModelTable.Columns)
					{
						column.DbType.Value = dataRow[dt.Columns[column.Name]];
					}

					sqlWriter.WriteInsert(stringWriter, dbModelTable);
					if (_settings.EnableQueryBatching)
					{
						stringWriter.WriteLine("GO");
					}

					stringWriter.WriteLine();

					if (i%10 == 0)
					{
						UpdateStatus(string.Format("Processing {0} of {1} rows", i + 1, dt.Rows.Count));
					}
				}

				UpdateStatus(string.Format("Processed {0} rows. Opening file...", dt.Rows.Count));

				// HACK - need to clean up the values for now as the model is holding the last rows data  ;-)
				// TODO - add a "deep clone" method to the table/columns
				foreach (var column in dbModelTable.Columns)
				{
					column.DbType.Value = null;
				}

				// create a new sql editor and push the sql into it
				IEditor editor = _services.Resolve<IQueryEditor>();
				editor.AllText = stringWriter.ToString();
				hostWindow.DisplayDockedForm(editor as DockContent);

				UpdateStatus(null);
			}
		}

		/// <summary>The lnk refresh_ link clicked.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			LoadTableData();
		}

		private void ViewTableForm_Activated(object sender, EventArgs e)
		{
			UpdateHostStatus();
		}
	}
}