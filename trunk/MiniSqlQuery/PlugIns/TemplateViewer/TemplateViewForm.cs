using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.TemplateViewer.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public partial class TemplateViewForm : DockContent
	{
		private readonly IApplicationServices _services;
		private readonly TemplateModel _model;
		private FileInfo _selectedFile;
		private FileSystemWatcher _fileSystemWatcher;

		public TemplateViewForm(IApplicationServices services, TemplateModel model)
		{
			InitializeComponent();
			_services = services;
			_model = model;
		}

		private void TemplateViewForm_Load(object sender, EventArgs e)
		{
			PopulateTree();
			templateFileWatcher.Path = _model.GetTemplatePath();
		}

		private void PopulateTree()
		{
			tvTemplates.Nodes[0].Nodes.Clear();
			tvTemplates.Nodes[0].Nodes.AddRange(_model.CreateNodes());
			tvTemplates.Nodes[0].Expand();
		}

		private void tvTemplates_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e != null && e.Node != null)
			{
				FileInfo fi = e.Node.Tag as FileInfo;
				if (fi != null)
				{
					RunTemplate(fi);
				}
			}
		}

		private void RunTemplate(FileInfo fi)
		{
			TemplateResult templateResult = _model.ProcessTemplateFile(fi.FullName, GetValue);

			// display in new window
			IFileEditorResolver resolver = _services.Resolve<IFileEditorResolver>();
			IEditor editor = _services.Resolve<IEditor>(resolver.ResolveEditorNameByExtension(templateResult.Extension));
			editor.AllText = templateResult.Text;
			editor.SetSyntax(templateResult.SyntaxName);
			_services.HostWindow.DisplayDockedForm(editor as DockContent);

		}

		string GetValue(string name)
		{
			string val = Interaction.InputBox(string.Format("Value for '{0}'", name), "Supply a Value", name, -1, -1);
			return val;
		}

		private void tvTemplates_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			FileInfo fi = null;
			if (e.Button == MouseButtons.Right && e.Node != null && e.Node.Tag is FileInfo)
			{
				fi = e.Node.Tag as FileInfo;
			}
			_selectedFile = fi;
			treeMenuStrip.Enabled = _selectedFile != null;
		}

		private void toolStripMenuItemRun_Click(object sender, EventArgs e)
		{
			if (_selectedFile!=null)
			{
				RunTemplate(_selectedFile);
			}
		}

		private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
		{
			if (_selectedFile != null)
			{
				IFileEditorResolver resolver = _services.Resolve<IFileEditorResolver>();
				var editor = resolver.ResolveEditorInstance(_selectedFile.FullName);
				editor.FileName = _selectedFile.FullName;
				editor.LoadFile();
				_services.HostWindow.DisplayDockedForm(editor as DockContent);
			}
		}

		private void templateFileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			PopulateTree();
		}

		private void templateFileWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			// todo - scan and modify tree
			PopulateTree();
		}

	}
}