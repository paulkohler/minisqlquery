#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	public partial class ViewTableForm : DockContent, IViewTable
	{
		private readonly QueryBatch _batch;
		private readonly IApplicationServices _services;
		private readonly IApplicationSettings _settings;
		private readonly object _syncLock = new object();

		private DbConnection _dbConnection;
		private bool _isBusy;
		private IDatabaseSchemaService _metaDataService;
		private string _status = string.Empty;

		public ViewTableForm()
		{
			InitializeComponent();
		}

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

		#region IViewTable Members

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

		public QueryBatch Batch
		{
			get { return _batch; }
		}

		#endregion

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
				if (dt.Columns[i].DataType == typeof (DateTime))
				{
					DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle();
					dateCellStyle.NullValue = nullText;
					dateCellStyle.Format = dateTimeFormat;
					dataGridViewResult.Columns[i].DefaultCellStyle = dateCellStyle;
				}
			}
		}

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
			_services.HostWindow.SetStatus(this, _status);
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
		}

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

		private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			LoadTableData();
		}

		private void dataGridViewResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
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

					if (i % 10 == 0)
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

		private void UpdateStatus(string msg)
		{
			_services.HostWindow.SetStatus(this, msg);
			Application.DoEvents();
		}
	}
}