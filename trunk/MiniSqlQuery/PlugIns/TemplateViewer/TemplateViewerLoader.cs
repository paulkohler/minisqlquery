using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateViewerLoader : PluginLoaderBase
	{
		public TemplateViewerLoader()
			: base("Template Viewer", "A Mini SQL Query Plugin for displaying template SQL items.", 50)
		{
		}

		public override void InitializePlugIn()
		{
			Services.HostWindow.ShowToolWindow(new TemplateViewForm(), WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
		}
	}
}