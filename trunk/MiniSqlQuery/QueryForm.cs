#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery
{
	/// <summary>The query form.</summary>
	public partial class QueryForm : DockContent, IQueryEditor, IPrintableContent
	{
		/// <summary>The _host window.</summary>
		private readonly IHostWindow _hostWindow;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _settings.</summary>
		private readonly IApplicationSettings _settings;

		/// <summary>The _sync lock.</summary>
		private static object _syncLock = new object();

		/// <summary>The _highlighting provider loaded.</summary>
		private bool _highlightingProviderLoaded;

		/// <summary>The _is dirty.</summary>
		private bool _isDirty;

		/// <summary>The _runner.</summary>
		private QueryRunner _runner;

		/// <summary>The _status.</summary>
		private string _status = string.Empty;

		/// <summary>The _text find service.</summary>
		private ITextFindService _textFindService;

		/// <summary>Initializes a new instance of the <see cref="QueryForm"/> class.</summary>
		public QueryForm()
		{
			InitializeComponent();

			txtQuery.ContextMenuStrip = contextMenuStripQuery;
			LoadHighlightingProvider();
			txtQuery.Document.DocumentChanged += DocumentDocumentChanged;

			contextMenuStripQuery.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<ExecuteTaskCommand>());
			contextMenuStripQuery.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CancelTaskCommand>());

			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItemSeparator());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseActiveWindowCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseAllWindowsCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyQueryEditorFileNameCommand>());

			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(editorContextMenuStrip);
		}

		/// <summary>Initializes a new instance of the <see cref="QueryForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		/// <param name="hostWindow">The host window.</param>
		public QueryForm(IApplicationServices services, IApplicationSettings settings, IHostWindow hostWindow)
			: this()
		{
			_services = services;
			_settings = settings;
			_hostWindow = hostWindow;
		}

		/// <summary>Gets or sets AllText.</summary>
		public string AllText
		{
			get { return txtQuery.Text; }
			set { txtQuery.Text = value; }
		}

		/// <summary>
		/// Gets a reference to the batch of queries.
		/// </summary>
		/// <value>The query batch.</value>
		public QueryBatch Batch
		{
			get { return _runner == null ? null : _runner.Batch; }
		}

		/// <summary>Gets a value indicating whether CanReplaceText.</summary>
		public bool CanReplaceText
		{
			get { return true; }
		}

		/// <summary>Gets or sets CursorColumn.</summary>
		public int CursorColumn
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Column; }
			set { txtQuery.ActiveTextAreaControl.Caret.Column = value; }
		}

		/// <summary>Gets or sets CursorLine.</summary>
		public int CursorLine
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Line; }
			set { txtQuery.ActiveTextAreaControl.Caret.Line = value; }
		}

		/// <summary>Gets CursorOffset.</summary>
		public int CursorOffset
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Offset; }
		}

		/// <summary>Gets EditorControl.</summary>
		public Control EditorControl
		{
			get { return txtQuery; }
		}


		/// <summary>Gets FileFilter.</summary>
		public string FileFilter
		{
			get { return "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*"; }
		}

		/// <summary>Gets or sets FileName.</summary>
		public string FileName
		{
			get { return txtQuery.FileName; }
			set
			{
				txtQuery.FileName = value;

				SetTabTextByFilename();
			}
		}

		/// <summary>Gets a value indicating whether IsBusy.</summary>
		public bool IsBusy { get; private set; }

		/// <summary>Gets or sets a value indicating whether IsDirty.</summary>
		public bool IsDirty
		{
			get { return _isDirty; }
			set
			{
				if (_isDirty != value)
				{
					_isDirty = value;
					SetTabTextByFilename();
				}
			}
		}

		/// <summary>Gets PrintDocument.</summary>
		public PrintDocument PrintDocument
		{
			get { return txtQuery.PrintDocument; }
		}

		/// <summary>Gets SelectedText.</summary>
		public string SelectedText
		{
			get { return txtQuery.ActiveTextAreaControl.SelectionManager.SelectedText; }
		}

		/// <summary>Gets TextFindService.</summary>
		public ITextFindService TextFindService
		{
			get
			{
				if (_textFindService == null)
				{
					_textFindService = _services.Container.Resolve<ITextFindService>();
				}

				return _textFindService;
			}
		}

		/// <summary>Gets TotalLines.</summary>
		public int TotalLines
		{
			get { return txtQuery.Document.TotalNumberOfLines; }
		}

		/// <summary>The execute query.</summary>
		/// <param name="sql">The sql.</param>
		public void ExecuteQuery(string sql)
		{
			if (IsBusy)
			{
				_hostWindow.DisplaySimpleMessageBox(this, "Please wait for the current operation to complete.", "Busy");
				return;
			}

			lock (_syncLock)
			{
				IsBusy = true;
			}

			_runner = QueryRunner.Create(_settings.ProviderFactory, _settings.ConnectionDefinition.ConnectionString, _settings.EnableQueryBatching, _settings.CommandTimeout);
			UseWaitCursor = true;
			queryBackgroundWorker.RunWorkerAsync(sql);
		}

		/// <summary>The load highlighting provider.</summary>
		public void LoadHighlightingProvider()
		{
			if (_highlightingProviderLoaded)
			{
				return;
			}

			// see: http://wiki.sharpdevelop.net/Syntax%20highlighting.ashx
			string dir = Path.GetDirectoryName(GetType().Assembly.Location);
			FileSyntaxModeProvider fsmProvider = new FileSyntaxModeProvider(dir);
			HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmProvider); // Attach to the text editor.
			txtQuery.SetHighlighting("SQL");
			_highlightingProviderLoaded = true;
		}

		/// <summary>The clear selection.</summary>
		public void ClearSelection()
		{
			txtQuery.ActiveTextAreaControl.SelectionManager.ClearSelection();
		}

		/// <summary>The highlight string.</summary>
		/// <param name="offset">The offset.</param>
		/// <param name="length">The length.</param>
		public void HighlightString(int offset, int length)
		{
			if (offset < 0 || length < 1)
			{
				return;
			}

			int endPos = offset + length;
			txtQuery.ActiveTextAreaControl.SelectionManager.SetSelection(
				txtQuery.Document.OffsetToPosition(offset), 
				txtQuery.Document.OffsetToPosition(endPos));
			SetCursorByOffset(endPos);
		}

		/// <summary>The insert text.</summary>
		/// <param name="text">The text.</param>
		public void InsertText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}

			int offset = txtQuery.ActiveTextAreaControl.Caret.Offset;

			// if some text is selected we want to replace it
			if (txtQuery.ActiveTextAreaControl.SelectionManager.IsSelected(offset))
			{
				offset = txtQuery.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
				txtQuery.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
			}

			txtQuery.Document.Insert(offset, text);
			int newOffset = offset + text.Length; // new offset at end of inserted text

			// now reposition the caret if required to be after the inserted text
			if (CursorOffset != newOffset)
			{
				SetCursorByOffset(newOffset);
			}

			txtQuery.Focus();
		}

		/// <summary>The load file.</summary>
		public void LoadFile()
		{
			txtQuery.LoadFile(FileName);
			IsDirty = false;
		}

		/// <summary>The save file.</summary>
		/// <exception cref="InvalidOperationException"></exception>
		public void SaveFile()
		{
			if (FileName == null)
			{
				throw new InvalidOperationException("The 'FileName' cannot be null");
			}

			txtQuery.SaveFile(FileName);
			IsDirty = false;
		}

		/// <summary>The set syntax.</summary>
		/// <param name="name">The name.</param>
		public void SetSyntax(string name)
		{
			LoadHighlightingProvider();
			txtQuery.SetHighlighting(name);
		}


		/// <summary>The find string.</summary>
		/// <param name="value">The value.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="comparisonType">The comparison type.</param>
		/// <returns>The find string.</returns>
		public int FindString(string value, int startIndex, StringComparison comparisonType)
		{
			if (string.IsNullOrEmpty(value) || startIndex < 0)
			{
				return -1;
			}

			string text = AllText;
			int pos = text.IndexOf(value, startIndex, comparisonType);
			if (pos > -1)
			{
				ClearSelection();
				HighlightString(pos, value.Length);
			}

			return pos;
		}

		/// <summary>The replace string.</summary>
		/// <param name="value">The value.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="length">The length.</param>
		/// <returns>The replace string.</returns>
		public bool ReplaceString(string value, int startIndex, int length)
		{
			if (value == null || startIndex < 0 || length < 0)
			{
				return false;
			}

			if ((startIndex + length) > AllText.Length)
			{
				return false;
			}

			txtQuery.Document.Replace(startIndex, length, value);

			return true;
		}

		/// <summary>The set text find service.</summary>
		/// <param name="textFindService">The text find service.</param>
		public void SetTextFindService(ITextFindService textFindService)
		{
			// accept nulls infering a reset
			_textFindService = textFindService;
		}

		/// <summary>The set cursor by location.</summary>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <returns>The set cursor by location.</returns>
		public bool SetCursorByLocation(int line, int column)
		{
			if (line > TotalLines)
			{
				return false;
			}

			txtQuery.ActiveTextAreaControl.Caret.Line = line;
			txtQuery.ActiveTextAreaControl.Caret.Column = column;

			return true;
		}

		/// <summary>The set cursor by offset.</summary>
		/// <param name="offset">The offset.</param>
		/// <returns>The set cursor by offset.</returns>
		public bool SetCursorByOffset(int offset)
		{
			if (offset >= 0)
			{
				txtQuery.ActiveTextAreaControl.Caret.Position = txtQuery.Document.OffsetToPosition(offset);
				return true;
			}

			return false;
		}

		/// <summary>The cancel task.</summary>
		public void CancelTask()
		{
			// todo
		}

		/// <summary>The execute task.</summary>
		public void ExecuteTask()
		{
			if (!string.IsNullOrEmpty(SelectedText))
			{
				ExecuteQuery(SelectedText);
			}
			else
			{
				ExecuteQuery(AllText);
			}
		}

		/// <summary>The set status.</summary>
		/// <param name="text">The text.</param>
		public void SetStatus(string text)
		{
			_status = text;
			UpdateHostStatus();
		}

		/// <summary>The create default font.</summary>
		/// <returns></returns>
		protected Font CreateDefaultFont()
		{
			return new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
		}

		/// <summary>The update host status.</summary>
		protected void UpdateHostStatus()
		{
			_hostWindow.SetStatus(this, _status);
		}

		/// <summary>The create query complete message.</summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns>The create query complete message.</returns>
		private static string CreateQueryCompleteMessage(DateTime start, DateTime end)
		{
			TimeSpan ts = end.Subtract(start);
			string msg = string.Format(
				"Query complete, {0:00}:{1:00}.{2:000}", 
				ts.Minutes, 
				ts.Seconds, 
				ts.Milliseconds);
			return msg;
		}

		/// <summary>The add tables.</summary>
		private void AddTables()
		{
			ClearGridsAndTabs();

			if (Batch != null)
			{
				string nullText = _settings.NullText;
				int counter = 1;
				foreach (Query query in Batch.Queries)
				{
					DataSet ds = query.Result;
					if (ds != null)
					{
						foreach (DataTable dt in ds.Tables)
						{
							DataGridView grid = new DataGridView();
							DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

							grid.AllowUserToAddRows = false;
							grid.AllowUserToDeleteRows = false;
							grid.Dock = DockStyle.Fill;
							grid.Name = "gridResults_" + counter;
							grid.ReadOnly = true;
							grid.DataSource = dt;
							grid.DataError += GridDataError;
							grid.DefaultCellStyle = cellStyle;
							cellStyle.NullValue = nullText;
							cellStyle.Font = CreateDefaultFont();
							grid.DataBindingComplete += GridDataBindingComplete;
							grid.Disposed += GridDisposed;

							TabPage tabPage = new TabPage();
							tabPage.Controls.Add(grid);
							tabPage.Name = "tabPageResults_" + counter;
							tabPage.Padding = new Padding(3);
							tabPage.Text = string.Format("{0}/Table {1}", ds.DataSetName, counter);
							tabPage.UseVisualStyleBackColor = false;

							_resultsTabControl.TabPages.Add(tabPage);
							grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
							counter++;
						}
					}
				}


				if (!string.IsNullOrEmpty(Batch.Messages))
				{
					RichTextBox rtf = new RichTextBox();
					rtf.Font = CreateDefaultFont();
					rtf.Dock = DockStyle.Fill;
					rtf.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
					rtf.Text = Batch.Messages;

					TabPage tabPage = new TabPage();
					tabPage.Controls.Add(rtf);
					tabPage.Name = "tabPageResults_Messages";
					tabPage.Padding = new Padding(3);
					tabPage.Dock = DockStyle.Fill;
					tabPage.Text = Resources.Messages;
					tabPage.UseVisualStyleBackColor = false;

					_resultsTabControl.TabPages.Add(tabPage);
				}
			}
		}

		/// <summary>Iterate backweards through list of tabs disposing grid and removing the tab page.</summary>
		private void ClearGridsAndTabs()
		{
			for (int i = _resultsTabControl.TabPages.Count - 1; i >= 0; i--)
			{
				TabPage tabPage = _resultsTabControl.TabPages[i];
				if (tabPage.Controls.Count > 0)
				{
					tabPage.Controls[0].Dispose(); // dispose grid
				}

				_resultsTabControl.TabPages.Remove(tabPage);
			}
		}

		/// <summary>The document document changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DocumentDocumentChanged(object sender, DocumentEventArgs e)
		{
			IsDirty = true;
		}

		/// <summary>Change the format style of date time columns. This has to be done post-bind.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			DataGridView grid = sender as DataGridView;
			if (grid == null)
			{
				return;
			}

			DataTable dt = grid.DataSource as DataTable;
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
					grid.Columns[i].DefaultCellStyle = dateCellStyle;
				}
			}
		}

		/// <summary>The grid data error.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void GridDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		/// <summary>Clean up event subscriptions.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridDisposed(object sender, EventArgs e)
		{
			DataGridView grid = sender as DataGridView;
			if (grid == null)
			{
				return;
			}

			grid.DataBindingComplete -= GridDataBindingComplete;
			grid.Disposed -= GridDisposed;
		}

		/// <summary>The query form_ activated.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void QueryForm_Activated(object sender, EventArgs e)
		{
			UpdateHostStatus();
		}

		/// <summary>The query form_ deactivate.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void QueryForm_Deactivate(object sender, EventArgs e)
		{
			_hostWindow.SetStatus(this, string.Empty);
		}

		/// <summary>The query form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void QueryForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_isDirty)
			{
				DialogResult saveFile = _hostWindow.DisplayMessageBox(
					this, 
					"Contents changed, do you want to save the file?\r\n" + TabText, "Save Changes?", 
					MessageBoxButtons.YesNoCancel, 
					MessageBoxIcon.Question, 
					MessageBoxDefaultButton.Button1, 
					0, 
					null, 
					null);

				if (saveFile == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (saveFile == DialogResult.Yes)
				{
					CommandManager.GetCommandInstance<SaveFileCommand>().Execute();
				}
			}
		}

		/// <summary>The query form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void QueryForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>The runner batch progress.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void RunnerBatchProgress(object sender, BatchProgressEventArgs e)
		{
			// push the progress % through to the background worker
			decimal i = Math.Max(1, e.Index);
			decimal count = Math.Max(1, e.Count);
			queryBackgroundWorker.ReportProgress(Convert.ToInt32(i/count*100m));
		}

		/// <summary>The set tab text by filename.</summary>
		private void SetTabTextByFilename()
		{
			string dirty = string.Empty;
			string text = "Untitled";
			string tabtext;

			if (_isDirty)
			{
				dirty = " *";
			}

			if (txtQuery.FileName != null)
			{
				text = FileName;
				tabtext = Path.GetFileName(FileName);
			}
			else
			{
				text += _settings.GetUntitledDocumentCounter();
				tabtext = text;
			}

			TabText = tabtext + dirty;
			ToolTipText = text + dirty;
		}

		/// <summary>The copy tool strip menu item_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyForm win = null;

			try
			{
				DataGridView grid = (DataGridView)_resultsTabControl.SelectedTab.Controls[0];

				if (grid.SelectedCells.Count == 0)
				{
					return;
				}

				win = new CopyForm();

				if (win.ShowDialog() == DialogResult.Cancel)
				{
					return;
				}

				SortedList headers = new SortedList();
				SortedList rows = new SortedList();

				string delimiter = win.Delimiter;
				string line = string.Empty;

				for (int i = 0; i < grid.SelectedCells.Count; i++)
				{
					DataGridViewCell cell = grid.SelectedCells[i];
					DataGridViewColumn col = cell.OwningColumn;

					if (!headers.ContainsKey(col.Index))
					{
						headers.Add(col.Index, col.Name);
					}

					if (!rows.ContainsKey(cell.RowIndex))
					{
						rows.Add(cell.RowIndex, cell.RowIndex);
					}
				}

				if (win.IncludeHeaders)
				{
					for (int i = 0; i < headers.Count; i++)
					{
						line += (string)headers.GetByIndex(i);
						if (i != headers.Count)
						{
							line += delimiter;
						}
					}

					line += "\r\n";
				}

				for (int i = 0; i < rows.Count; i++)
				{
					DataGridViewRow row = grid.Rows[(int)rows.GetKey(i)];
					DataGridViewCellCollection cells = row.Cells;

					for (int j = 0; j < headers.Count; j++)
					{
						DataGridViewCell cell = cells[(int)headers.GetKey(j)];

						if (cell.Selected)
						{
							line += cell.Value;
						}

						if (j != (headers.Count - 1))
						{
							line += delimiter;
						}
					}

					line += "\r\n";
				}

				if (!string.IsNullOrEmpty(line))
				{
					Clipboard.Clear();
					Clipboard.SetText(line);

					_hostWindow.SetStatus(this, "Selected data has been copied to your clipboard");
				}
			}
			finally
			{
				if (win != null)
				{
					win.Dispose();
				}
			}
		}

		/// <summary>The query background worker_ do work.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void queryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string sql = (string)e.Argument;
			_runner.BatchProgress += RunnerBatchProgress;
			_runner.ExecuteQuery(sql);
		}

		/// <summary>The query background worker_ progress changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void queryBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SetStatus(string.Format("Processing batch {0}%...", e.ProgressPercentage));
		}

		/// <summary>The query background worker_ run worker completed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void queryBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				_runner.BatchProgress -= RunnerBatchProgress;
				if (e.Error != null)
				{
					// todo: improve!
					_hostWindow.DisplaySimpleMessageBox(this, e.Error.Message, "Error");
					SetStatus(e.Error.Message);
				}
				else
				{
					_hostWindow.SetPointerState(Cursors.Default);
					string message = CreateQueryCompleteMessage(_runner.Batch.StartTime, _runner.Batch.EndTime);
					if (_runner.Exception != null)
					{
						message = "ERROR - " + message;
					}

					_hostWindow.SetStatus(this, message);
					AddTables();
					txtQuery.Focus();
				}
			}
			finally
			{
				UseWaitCursor = false;
				lock (_syncLock)
				{
					IsBusy = false;
				}
			}
		}

		/// <summary>The select all tool strip menu item_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridView grid = (DataGridView)_resultsTabControl.SelectedTab.Controls[0];
			grid.SelectAll();
		}
	}
}