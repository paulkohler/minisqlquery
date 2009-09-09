using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.TemplateViewer.Commands;

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
			Services.RegisterEditor<TemplateEditorForm>("mt-editor", "mt");
			Services.RegisterComponent<TemplateHost>("TemplateHost");

			Services.RegisterComponent<TemplateViewForm>("TemplateViewForm");
			Services.HostWindow.AddPluginCommand<RunTemplateCommand>();
			Services.HostWindow.ShowToolWindow(Services.Resolve<TemplateViewForm>(), WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
		}
	}
}