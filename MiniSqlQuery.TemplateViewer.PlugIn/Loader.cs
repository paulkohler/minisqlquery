using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.TemplateViewer.PlugIn
{
	public class Loader : IPlugIn
	{
		private IApplicationServices _services;

		public int RequestedLoadOrder
		{
			get { return 50; }
		}

		public string PluginName
		{
			get { return "Template Viewer"; }
		}

		public string PluginDescription
		{
			get { return "A Mini SQL Query Plugin for displaying template SQL items."; }
		}

		public void LoadPlugIn(IApplicationServices services)
		{
			_services = services;
		}

		public void InitializePlugIn()
		{
			_services.HostWindow.ShowToolWindow(new TemplateViewForm(), WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
			//_services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
			//    CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectStatementCommand>());

		}

		public void UnloadPlugIn()
		{
		}
	}
}
