using System;
using System.Collections;
using System.Collections.Generic;
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
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery
{
	public partial class QueryForm : DockContent, IQueryEditor, IPrintableContent
	{
		private static object _syncLock = new object();
		private readonly IHostWindow _hostWindow;
		private readonly IApplicationServices _services;
		private readonly IApplicationSettings _settings;

		private bool _highlightingProviderLoaded;
		private bool _isDirty;
		private QueryRunner _runner;

		private string _status = string.Empty;
		private ITextFindService _textFindService;

		public QueryForm()
		{
			InitializeComponent();

			txtQuery.ContextMenuStrip = contextMenuStripQuery;
			LoadHighlightingProvider();
			txtQuery.Document.DocumentChanged += DocumentDocumentChanged;

			contextMenuStripQuery.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<ExecuteTaskCommand>());
			contextMenuStripQuery.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CancelTaskCommand>());

			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseActiveWindowCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseAllWindowsCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyQueryEditorFileNameCommand>());

			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(editorContextMenuStrip);
		}

		public QueryForm(IApplicationServices services, IApplicationSettings settings, IHostWindow hostWindow)
			: this()
		{
			_services = services;
			_settings = settings;
			_hostWindow = hostWindow;
		}

		#region IPrintableContent Members

		public PrintDocument PrintDocument
		{
			get { return txtQuery.PrintDocument; }
		}

		#endregion

		#region IQueryEditor Members

		public string SelectedText
		{
			get { return txtQuery.ActiveTextAreaControl.SelectionManager.SelectedText; }
		}

		public string AllText
		{
			get { return txtQuery.Text; }
			set { txtQuery.Text = value; }
		}

		public Control EditorControl
		{
			get { return txtQuery; }
		}


		public string FileName
		{
			get { return txtQuery.FileName; }
			set
			{
				txtQuery.FileName = value;

				SetTabTextByFilename();
			}
		}

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

		public bool IsBusy { get; private set; }

		[Obsolete]
		public DataSet DataSet { get; private set; }

		public void SetStatus(string text)
		{
			_status = text;
			UpdateHostStatus();
		}

		public void SetSyntax(string name)
		{
			LoadHighlightingProvider();
			txtQuery.SetHighlighting(name);
		}

		public void LoadFile()
		{
			txtQuery.LoadFile(FileName);
			IsDirty = false;
		}

		public void SaveFile()
		{
			if (FileName == null)
			{
				throw new InvalidOperationException("The 'FileName' cannot be null");
			}

			txtQuery.SaveFile(FileName);
			IsDirty = false;
		}


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

		public void CancelTask()
		{
			//todo
		}

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


		public void ClearSelection()
		{
			txtQuery.ActiveTextAreaControl.SelectionManager.ClearSelection();
		}

		public bool SetCursorByOffset(int offset)
		{
			if (offset >= 0)
			{
				txtQuery.ActiveTextAreaControl.Caret.Position = txtQuery.Document.OffsetToPosition(offset);
				return true;
			}

			return false;
		}

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

		public int CursorLine
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Line; }
			set { txtQuery.ActiveTextAreaControl.Caret.Line = value; }
		}

		public int CursorColumn
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Column; }
			set { txtQuery.ActiveTextAreaControl.Caret.Column = value; }
		}

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

		public int TotalLines
		{
			get { return txtQuery.Document.TotalNumberOfLines; }
		}

		public int CursorOffset
		{
			get { return txtQuery.ActiveTextAreaControl.Caret.Offset; }
		}

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

		public void SetTextFindService(ITextFindService textFindService)
		{
			// accept nulls infering a reset
			_textFindService = textFindService;
		}

		public bool CanReplaceText
		{
			get { return true; }
		}

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

		/// <summary>
		/// Gets a reference to the batch of queries.
		/// </summary>
		/// <value>The query batch.</value>
		public QueryBatch Batch
		{
			get { return _runner == null ? null : _runner.Batch; }
		}

		#endregion

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

		private void SetTabTextByFilename()
		{
			string dirty = string.Empty;
			string text = "Untitled";

			if (_isDirty)
			{
				dirty = " *";
			}

			if (txtQuery.FileName != null)
			{
				text = FileName;
			}
			else
			{
				text += _settings.GetUntitledDocumentCounter();
			}

			text += dirty;
			TabText = text;
			ToolTipText = text;
		}

		private void DocumentDocumentChanged(object sender, DocumentEventArgs e)
		{
			IsDirty = true;
		}

		protected void UpdateHostStatus()
		{
			_hostWindow.SetStatus(this, _status);
		}

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

			_runner = QueryRunner.Create(_settings.ProviderFactory, _settings.ConnectionDefinition.ConnectionString, _settings.EnableQueryBatching);
			UseWaitCursor = true;
			queryBackgroundWorker.RunWorkerAsync(sql);
		}

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
					tabPage.Text = "Messages";
					tabPage.UseVisualStyleBackColor = false;

					_resultsTabControl.TabPages.Add(tabPage);
				}
			}
		}

		/// <summary>
		/// Iterate backweards through list of tabs disposing grid and removing the tab page.
		/// </summary>
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

		/// <summary>
		/// Clean up event subscriptions.
		/// </summary>
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

		/// <summary>
		/// Change the format style of date time columns. This has to be done post-bind.
		/// </summary>
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
				if (dt.Columns[i].DataType == typeof (DateTime))
				{
					DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle();
					dateCellStyle.NullValue = nullText;
					dateCellStyle.Format = dateTimeFormat;
					grid.Columns[i].DefaultCellStyle = dateCellStyle;
				}
			}
		}

		protected Font CreateDefaultFont()
		{
			return new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
		}

		private void GridDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private void QueryForm_Load(object sender, EventArgs e)
		{
		}

		private void QueryForm_Activated(object sender, EventArgs e)
		{
			UpdateHostStatus();
		}

		private void QueryForm_Deactivate(object sender, EventArgs e)
		{
			_hostWindow.SetStatus(this, string.Empty);
		}

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

		private void queryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string sql = (string) e.Argument;
			_runner.BatchProgress += RunnerBatchProgress;
			_runner.ExecuteQuery(sql);
		}

		private void RunnerBatchProgress(object sender, BatchProgressEventArgs e)
		{
			// push the progress % through to the background worker
			decimal i = Math.Max(1, e.Index);
			decimal count = Math.Max(1, e.Count);
			queryBackgroundWorker.ReportProgress(Convert.ToInt32(i/count*100m));
		}

		private void queryBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SetStatus(string.Format("Processing batch {0}%...", e.ProgressPercentage));
		}

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

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridView grid = (DataGridView) _resultsTabControl.SelectedTab.Controls[0];
			grid.SelectAll();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyForm win = null;

			try
			{
				DataGridView grid = (DataGridView) _resultsTabControl.SelectedTab.Controls[0];

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
						line += (string) headers.GetByIndex(i);
						if (i != headers.Count)
						{
							line += delimiter;
						}
					}

					line += "\r\n";
				}

				for (int i = 0; i < rows.Count; i++)
				{
					DataGridViewRow row = grid.Rows[(int) rows.GetKey(i)];
					DataGridViewCellCollection cells = row.Cells;

					for (int j = 0; j < headers.Count; j++)
					{
						DataGridViewCell cell = cells[(int) headers.GetKey(j)];

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
	}
}