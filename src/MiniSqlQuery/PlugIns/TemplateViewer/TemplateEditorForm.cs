#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using Microsoft.VisualBasic;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core.Template;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	/// <summary>The template editor form.</summary>
	public partial class TemplateEditorForm : DockContent, IEditor, IFindReplaceProvider, INavigatableDocument, ITemplateEditor
	{
		/// <summary>The _host window.</summary>
		private readonly IHostWindow _hostWindow;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _file name.</summary>
		private string _fileName;

		/// <summary>The _highlighting provider loaded.</summary>
		private bool _highlightingProviderLoaded;

		/// <summary>The _is dirty.</summary>
		private bool _isDirty;

		/// <summary>The _text find service.</summary>
		private ITextFindService _textFindService;

		/// <summary>Initializes a new instance of the <see cref="TemplateEditorForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="hostWindow">The host window.</param>
		public TemplateEditorForm(IApplicationServices services, IHostWindow hostWindow)
		{
			InitializeComponent();
			txtEdit.Document.DocumentChanged += DocumentDocumentChanged;
			_services = services;
			_hostWindow = hostWindow;

			formContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileCommand>());
			formContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItemSeparator());
			formContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseActiveWindowCommand>());
			formContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseAllWindowsCommand>());
			formContextMenuStrip.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyQueryEditorFileNameCommand>());

			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(formContextMenuStrip);
		}

		/// <summary>Gets or sets AllText.</summary>
		public string AllText
		{
			get { return txtEdit.Text; }
			set { txtEdit.Text = value; }
		}

		/// <summary>Gets a value indicating whether CanReplaceText.</summary>
		public bool CanReplaceText
		{
			get { return true; }
		}

		/// <summary>Gets or sets CursorColumn.</summary>
		public int CursorColumn
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Column; }
			set { txtEdit.ActiveTextAreaControl.Caret.Column = value; }
		}

		/// <summary>Gets or sets CursorLine.</summary>
		public int CursorLine
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Line; }
			set { txtEdit.ActiveTextAreaControl.Caret.Line = value; }
		}

		/// <summary>Gets CursorOffset.</summary>
		public int CursorOffset
		{
			get { return txtEdit.ActiveTextAreaControl.Caret.Offset; }
		}

		/// <summary>Gets FileFilter.</summary>
		public string FileFilter
		{
			get { return "Mini SQL Template Files (*.mt)|*.mt|All Files (*.*)|*.*"; }
		}

		/// <summary>Gets or sets FileName.</summary>
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

		/// <summary>Gets a value indicating whether IsBusy.</summary>
		public bool IsBusy
		{
			get { return false; }
		}

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

		/// <summary>Gets SelectedText.</summary>
		public string SelectedText
		{
			get { return txtEdit.ActiveTextAreaControl.SelectionManager.SelectedText; }
		}

		/// <summary>Gets TextFindService.</summary>
		public ITextFindService TextFindService
		{
			get
			{
				if (_textFindService == null)
				{
					_textFindService = _services.Resolve<ITextFindService>();
				}

				return _textFindService;
			}
		}

		/// <summary>Gets TotalLines.</summary>
		public int TotalLines
		{
			get { return txtEdit.Document.TotalNumberOfLines; }
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
			txtEdit.SetHighlighting("NVelocity");
			_highlightingProviderLoaded = true;
		}

		/// <summary>The clear selection.</summary>
		public void ClearSelection()
		{
			txtEdit.ActiveTextAreaControl.SelectionManager.ClearSelection();
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
			txtEdit.ActiveTextAreaControl.SelectionManager.SetSelection(
				txtEdit.Document.OffsetToPosition(offset), 
				txtEdit.Document.OffsetToPosition(endPos));
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

			int offset = txtEdit.ActiveTextAreaControl.Caret.Offset;

			// if some text is selected we want to replace it
			if (txtEdit.ActiveTextAreaControl.SelectionManager.IsSelected(offset))
			{
				offset = txtEdit.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
				txtEdit.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
			}

			txtEdit.Document.Insert(offset, text);
			int newOffset = offset + text.Length; // new offset at end of inserted text

			// now reposition the caret if required to be after the inserted text
			if (CursorOffset != newOffset)
			{
				SetCursorByOffset(newOffset);
			}

			txtEdit.Focus();
		}


		/// <summary>The load file.</summary>
		public void LoadFile()
		{
			txtEdit.LoadFile(FileName);
			IsDirty = false;
		}

		/// <summary>The save file.</summary>
		public void SaveFile()
		{
			txtEdit.SaveFile(FileName);
			IsDirty = false;
		}

		/// <summary>The set syntax.</summary>
		/// <param name="name">The name.</param>
		public void SetSyntax(string name)
		{
			LoadHighlightingProvider();
			txtEdit.SetHighlighting(name);
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

			txtEdit.Document.Replace(startIndex, length, value);

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

			txtEdit.ActiveTextAreaControl.Caret.Line = line;
			txtEdit.ActiveTextAreaControl.Caret.Column = column;

			return true;
		}

		/// <summary>The set cursor by offset.</summary>
		/// <param name="offset">The offset.</param>
		/// <returns>The set cursor by offset.</returns>
		public bool SetCursorByOffset(int offset)
		{
			if (offset >= 0)
			{
				txtEdit.ActiveTextAreaControl.Caret.Position = txtEdit.Document.OffsetToPosition(offset);
				return true;
			}

			return false;
		}

		/// <summary>The cancel task.</summary>
		public void CancelTask()
		{
			// N/A
		}

		/// <summary>The execute task.</summary>
		public void ExecuteTask()
		{
			RunTemplate();
		}

		/// <summary>The run template.</summary>
		public void RunTemplate()
		{
			TemplateModel templateModel = _services.Resolve<TemplateModel>();
			TemplateResult templateResult = null;
			txtErrors.Clear();

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

// todo - try to get the line number and move cursor?...
				txtErrors.Text = exp.Message;
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

		/// <summary>The document document changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DocumentDocumentChanged(object sender, DocumentEventArgs e)
		{
			IsDirty = true;
		}

		/// <summary>The get value.</summary>
		/// <param name="name">The name.</param>
		/// <returns>The get value.</returns>
		private string GetValue(string name)
		{
			string val = Interaction.InputBox(string.Format("Value for '{0}'", name), "Supply a Value", name, -1, -1);
			return val;
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

			TabText = tabtext + dirty;
			ToolTipText = text + dirty;
		}

		/// <summary>The template editor form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The template editor form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void TemplateEditorForm_Load(object sender, EventArgs e)
		{
			rtfHelp.Rtf = TemplateResources.TemplateHelp;
		}
	}
}