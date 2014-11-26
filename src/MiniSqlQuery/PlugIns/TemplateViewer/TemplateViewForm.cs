#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	/// <summary>The template view form.</summary>
	public partial class TemplateViewForm : DockContent
	{
		/// <summary>The _model.</summary>
		private readonly TemplateModel _model;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _selected file.</summary>
		private FileInfo _selectedFile;

		/// <summary>Initializes a new instance of the <see cref="TemplateViewForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="model">The model.</param>
		public TemplateViewForm(IApplicationServices services, TemplateModel model)
		{
			InitializeComponent();
			_services = services;
			_model = model;
		}

		/// <summary>The get value.</summary>
		/// <param name="name">The name.</param>
		/// <returns>The get value.</returns>
		private string GetValue(string name)
		{
			string val = Interaction.InputBox(string.Format("Value for '{0}'", name), "Supply a Value", name, -1, -1);
			return val;
		}

		/// <summary>The populate tree.</summary>
		private void PopulateTree()
		{
			tvTemplates.Nodes[0].Nodes.Clear();
			tvTemplates.Nodes[0].Nodes.AddRange(_model.CreateNodes());
			tvTemplates.Nodes[0].Expand();
		}

		/// <summary>The run template.</summary>
		/// <param name="fi">The fi.</param>
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

		/// <summary>The template view form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void TemplateViewForm_Load(object sender, EventArgs e)
		{
			PopulateTree();
			templateFileWatcher.Path = _model.GetTemplatePath();
		}

		/// <summary>The template file watcher_ changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void templateFileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			PopulateTree();
		}

		/// <summary>The template file watcher_ renamed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void templateFileWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			// todo - scan and modify tree
			PopulateTree();
		}

		/// <summary>The tool strip menu item edit_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The tool strip menu item run_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripMenuItemRun_Click(object sender, EventArgs e)
		{
			if (_selectedFile != null)
			{
				RunTemplate(_selectedFile);
			}
		}

		/// <summary>The tv templates_ node mouse click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The tv templates_ node mouse double click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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
	}
}