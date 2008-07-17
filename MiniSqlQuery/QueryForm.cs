using System;
using System.Data;
using System.Data.Common;
using System.Drawing.Printing;
using System.Windows.Forms;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery
{
	public partial class QueryForm : DockContent, IQueryEditor, IFindReplaceProvider, IPrintableContent
    {
		private static int _untitledCounter = 1;
		private string _status = string.Empty;
		private string _messages = string.Empty;
		private bool _isDirty = false;
		private bool _isBusy = false;
		private ITextFindService _textFindService;

		private IApplicationServices _services;
		internal DataSet _result;

		public QueryForm()
		{
			InitializeComponent();

			txtQuery.ContextMenuStrip = this.contextMenuStripQuery;
			txtQuery.SetHighlighting("SQL");
			txtQuery.Document.DocumentChanged += new ICSharpCode.TextEditor.Document.DocumentEventHandler(Document_DocumentChanged);

			queryToolStripMenuItem.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ExecuteQueryCommand>());
			queryToolStripMenuItem.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveResultsAsDataSetCommand>());
			queryToolStripMenuItem.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			
			contextMenuStripQuery.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<ExecuteQueryCommand>());

			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseActiveWindowCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseAllWindowsCommand>());
			editorContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyQueryEditorFileNameCommand>());

			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(editorContextMenuStrip);
		}

        public string SelectedText
        {
            get
            {
                return txtQuery.ActiveTextAreaControl.SelectionManager.SelectedText;
            }
        }

        public string AllText
        {
            get
            {
                return txtQuery.Text;
            }
            set
            {
                txtQuery.Text = value;
            }
        }

		public Control EditorControl
		{
			get
			{
				return txtQuery;
			}
		}

		public TabControl ResultsControl
		{
			get
			{
				return tabControlResults;
			}
		}

		public string FileName
		{
			get
			{
				return txtQuery.FileName;
			}
			set
			{
				txtQuery.FileName = value;

				SetTabTextByFilename();
			}
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
				text += _untitledCounter;
				_untitledCounter++;
			}

			text += dirty;
			TabText = text;
			ToolTipText = text;
		}

		public bool IsDirty
		{
			get
			{
				return _isDirty;
			}
			set
			{
				if (_isDirty != value)
				{
					_isDirty = value;
					SetTabTextByFilename();
				}
			}
		}

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
			}
		}

		public DataSet DataSet
		{
			get
			{
				return _result;
			}
		}

        void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
        {
			IsDirty = true;
        }

        public void SetStatus(string text)
        {
            _status = text;
            UpdateHostStatus();
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

		protected void UpdateHostStatus()
        {
			ApplicationServices.Instance.HostWindow.SetStatus(this, _status);
        }

		public QueryForm(IApplicationServices services)
			: this()
		{
			_services = services;
		}


		public void ExecuteQuery()
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

		public void ExecuteQuery(string sql)
		{
			DbConnection dbConnection;
            DbDataAdapter adapter = null;
            DbCommand cmd = null;
			bool errored = false;

			if (_isBusy)
			{
				_services.HostWindow.DisplaySimpleMessageBox(this, "Please wait for the current operation to complete.", "Busy");
				return;
			}

			if (string.IsNullOrEmpty(_services.Settings.ConnectionDefinition.ConnectionString))
			{
				_services.HostWindow.DisplaySimpleMessageBox(this, "Please supply a connection string", "No Connection");
				return;
			}

			DateTime start = DateTime.Now;
			DateTime end = DateTime.Now;

			try
			{
				_services.HostWindow.SetPointerState(Cursors.WaitCursor);
				txtQuery.Enabled = false;
				_isBusy = true;

				dbConnection = _services.Settings.GetOpenConnection();
                dbConnection.StateChange += new StateChangeEventHandler(conn_StateChange);
				_messages = string.Empty;
				//if (dbConnection is System.Data.SqlClient.SqlConnection)
				//{
				//    // todo - inprogress - support various InfoMessage events
				//    ((System.Data.SqlClient.SqlConnection)dbConnection).InfoMessage += SqlClienInfoMessage;
				//}

                _result = new DataSet();

				// TODO - Support GO syntax

				adapter = _services.Settings.ProviderFactory.CreateDataAdapter();
                cmd = dbConnection.CreateCommand();
				cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                adapter.SelectCommand = cmd;
                adapter.Fill(_result);

				// now merge?
			}
			catch (DbException dbExp)
			{
				// todo: improve!
				_services.HostWindow.DisplaySimpleMessageBox(this, dbExp.Message, "Error");
				SetStatus(dbExp.Message);
				errored = true;
			}
			catch (InvalidOperationException invalidExp)
			{
				// todo: improve!
				_services.HostWindow.DisplaySimpleMessageBox(this, invalidExp.Message, "Error");
				SetStatus(invalidExp.Message);
				errored = true;
			}
			finally
			{
				end = DateTime.Now;
				txtQuery.Enabled = true;
				if (adapter != null)
				{
					adapter.Dispose();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
				_isBusy = false;
				_services.HostWindow.SetPointerState(Cursors.Default);
				//if (dbConnection is System.Data.SqlClient.SqlConnection)
				//{
				//    ((System.Data.SqlClient.SqlConnection)dbConnection).InfoMessage -= SqlClienInfoMessage;
				//}
			}

			if (!errored)
			{
				_services.HostWindow.SetStatus(this, CreateQueryCompleteMessage(start, end));
				AddTables();
			}

			txtQuery.Focus();
		}

		//void SqlClienInfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
		//{
		//    _messages += e.Message + Environment.NewLine;

		//}

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
            tabControlResults.TabPages.Clear();

            if (_result != null)
            {
                int counter = 1;
                foreach (DataTable dt in _result.Tables)
                {
                    DataGridView grid = new DataGridView();
					DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
					
					grid.AllowUserToAddRows = false;
                    grid.AllowUserToDeleteRows = false;
                    grid.Dock = DockStyle.Fill;
                    grid.Name = "gridResults_" + counter;
                    grid.ReadOnly = true;
                    grid.DataSource = dt;
					grid.DataError += gridDataError;
					grid.DefaultCellStyle = cellStyle;

					cellStyle.NullValue = "<NULL>";

                    TabPage tabPage = new TabPage();
                    tabPage.Controls.Add(grid);
                    tabPage.Name = "tabPageResults_" + counter;
                    tabPage.Padding = new System.Windows.Forms.Padding(3);
                    tabPage.Text = "Table " + counter;
                    tabPage.UseVisualStyleBackColor = false;

                    tabControlResults.TabPages.Add(tabPage);
                    counter++;
                }

				if (!string.IsNullOrEmpty(_messages))
				{
					RichTextBox rtf = new RichTextBox();
					rtf.Text = _messages;

					TabPage tabPage = new TabPage();
					tabPage.Controls.Add(rtf);
					tabPage.Name = "tabPageResults_Messages";
					tabPage.Padding = new System.Windows.Forms.Padding(3);
					tabPage.Text = "Messages";
					tabPage.UseVisualStyleBackColor = false;

					tabControlResults.TabPages.Add(tabPage);
					counter++;
				}
            }
        }

		void gridDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		void conn_StateChange(object sender, StateChangeEventArgs e)
		{
            SetStatus(e.CurrentState.ToString());
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
			_services.HostWindow.SetStatus(this, string.Empty);
        }

		private void QueryForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_isDirty)
			{
				DialogResult saveFile = _services.HostWindow.DisplayMessageBox(
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

		public void InsertText(string text)
		{
			if (text == null || text.Length == 0)
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

		public PrintDocument PrintDocument
		{
			get
			{
				return txtQuery.PrintDocument;
			}
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
			get
			{
				return txtQuery.ActiveTextAreaControl.Caret.Line;
			}
			set
			{
				txtQuery.ActiveTextAreaControl.Caret.Line = value;
			}
		}

		public int CursorColumn
		{
			get
			{
				return txtQuery.ActiveTextAreaControl.Caret.Column;
			}
			set
			{
				txtQuery.ActiveTextAreaControl.Caret.Column = value;
			}
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
			get 
			{ 
				return txtQuery.Document.TotalNumberOfLines; 
			}
		}

		public int CursorOffset 
		{
			get
			{
				return txtQuery.ActiveTextAreaControl.Caret.Offset;
			}
		}

		#region IFindReplaceProvider Members

		public ITextFindService TextFindService
		{
			get
			{
				if (_textFindService == null)
				{
					_textFindService = _services.Container.Resolve<ITextFindService>("DefaultTextFindService");
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
			get 
			{
				return true;
			}
		}

		public int FindString(string value, int startIndex, StringComparison comparisonType)
		{
			if (value == null || value.Length == 0 || startIndex < 0)
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
			if (value == null)
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

		#endregion
	}
}
