using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
using System.IO;
using MiniSqlQuery.TemplateViewer.PlugIn.Commands;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.TemplateViewer.PlugIn
{
	public partial class TemplateViewForm : DockContent
	{
		TemplateModel _model;

		public TemplateViewForm()
		{
			InitializeComponent();

			_model = new TemplateModel();

		}

		private void TemplateViewForm_Load(object sender, EventArgs e)
		{
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
					new NewQueryByTemplateCommand().Execute();
					IQueryEditor editor = (IQueryEditor)ApplicationServices.Instance.HostWindow.ActiveChildForm;
					string template = _model.ProcessTemplate(fi.FullName);
					editor.AllText = template;
				}
			}
		}




	}
}
