using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.TextEditor.Document;
using Microsoft.VisualBasic;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using MiniSqlQuery.Core.Template;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public partial class TemplateEditorForm : DockContent, IEditor, IFindReplaceProvider, INavigatableDocument, ITemplateEditor
	{
		private readonly IApplicationServices _services;
		private readonly IHostWindow _hostWindow;

		private string _fileName;
		private bool _highlightingProviderLoaded;
		private bool _isDirty;
		private ITextFindService _textFindService;

		public TemplateEditorForm(IApplicationServices services, IHostWindow hostWindow)
		{
			InitializeComponent();
			txtEdit.Document.DocumentChanged += DocumentDocumentChanged;
			_services = services;
			_hostWindow = hostWindow;
		}

		#region IEditor Members

		public string SelectedText
		{
			get { return txtEdit.ActiveTextAreaControl.SelectionManager.SelectedText; }
		}

		public string AllText
		{
			get { return txtEdit.Text; }
			set { txtEdit.Text = value; }
		}

		public void SetSyntax(string name)
		{
			LoadHighlightingProvider();
			txtEdit.SetHighlighting(name);
		}


		public string FileName
		{
			get { return _fileName; }
			set
			{
				_fileName = value;
				Text = FileName;
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


		public void LoadFile()
		{
			txtEdit.LoadFile(FileName);
			IsDirty = false;
		}

		public void SaveFile()
		{
			txtEdit.SaveFile(FileName);
			IsDirty = false;
		}

		#endregion

		#region IFindReplaceProvider Members

		public int CursorOffset
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Offset; }
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

			txtEdit.Document.Replace(startIndex, length, value);

			return true;
		}

		#endregion

		#region INavigatableDocument Members

		public bool SetCursorByOffset(int offset)
		{
			if (offset >= 0)
			{
				txtEdit.ActiveTextAreaControl.Caret.Position = txtEdit.Document.OffsetToPosition(offset);
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

			txtEdit.ActiveTextAreaControl.Caret.Line = line;
			txtEdit.ActiveTextAreaControl.Caret.Column = column;

			return true;
		}

		public int CursorLine
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Line; }
			set { txtEdit.ActiveTextAreaControl.Caret.Line = value; }
		}

		public int CursorColumn
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Column; }
			set { txtEdit.ActiveTextAreaControl.Caret.Column = value; }
		}

		public int TotalLines
		{
			get { return txtEdit.Document.TotalNumberOfLines; }
		}

		#endregion

		#region ITemplateEditor Members

		public void RunTemplate()
		{
			TemplateModel templateModel = _services.Resolve<TemplateModel>();
			TemplateResult templateResult = null;

			try
			{
				string[] lines = AllText.Replace("\r", string.Empty).Split('\n');
				string text;
				Dictionary<string, object> items = new Dictionary<string, object>();
				items[TemplateModel.Extension] = templateModel.InferExtensionFromFilename(FileName, items);
				text = templateModel.PreProcessTemplate(lines, GetValue, items);
				templateResult = templateModel.ProcessTemplate(text, items);
			}
			catch (TemplateException exp)
			{
				_hostWindow.DisplaySimpleMessageBox(this, exp.Message, "Template Error");
				// todo - try to get the line number and move cursor...
			}

			if (templateResult != null)
			{
				// display in new window
				IFileEditorResolver resolver = _services.Resolve<IFileEditorResolver>();
				IEditor editor = _services.Resolve<IEditor>(resolver.ResolveEditorNameByExtension(templateResult.Extension));
				editor.AllText = templateResult.Text;
				editor.SetSyntax(templateResult.SyntaxName);
				_hostWindow.DisplayDockedForm(editor as DockContent);
			}
		}

		public void ExecuteTask()
		{
			RunTemplate();
		}

		public void CancelTask()
		{
			// N/A
		}

		public bool IsBusy
		{
			get { return false; }
		}

		#endregion

		private string GetValue(string name)
		{
			string val = Interaction.InputBox(string.Format("Value for '{0}'", name), "Supply a Value", name, -1, -1);
			return val;
		}

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
			txtEdit.SetHighlighting("NVelocity");
			_highlightingProviderLoaded = true;
		}

		private void TemplateEditorForm_Load(object sender, EventArgs e)
		{
		}

		public void HighlightString(int offset, int length)
		{
			if (offset < 0 || length < 1)
			{
				return;
			}

			int endPos = offset + length;
			txtEdit.ActiveTextAreaControl.SelectionManager.SetSelection(
				txtEdit.Document.OffsetToPosition(offset),
				txtEdit.Document.OffsetToPosition(endPos));
			SetCursorByOffset(endPos);
		}

		public void ClearSelection()
		{
			txtEdit.ActiveTextAreaControl.SelectionManager.ClearSelection();
		}

		private void SetTabTextByFilename()
		{
			string dirty = string.Empty;
			string text = "Untitled";
			string tabtext = "";

			if (_isDirty)
			{
				dirty = " *";
			}

			if (txtEdit.FileName != null)
			{
				text = FileName;
				tabtext = Path.GetFileName(FileName);
			}
			else
			{
				text += _services.Settings.GetUntitledDocumentCounter();
				tabtext = text;
			}

			text += dirty;
			TabText = tabtext;
			ToolTipText = text;
		}

		private void DocumentDocumentChanged(object sender, DocumentEventArgs e)
		{
			IsDirty = true;
		}

		private void TemplateEditorForm_FormClosing(object sender, FormClosingEventArgs e)
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
	}
}