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
			Services.RegisterComponent<TemplateViewForm>("TemplateViewForm");
			Services.HostWindow.ShowToolWindow(Services.Resolve<TemplateViewForm>(), WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
		}
	}
}