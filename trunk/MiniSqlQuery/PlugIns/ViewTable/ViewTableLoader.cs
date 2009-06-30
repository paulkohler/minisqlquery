using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.ViewTable.Commands;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	public class ViewTableLoader : PluginLoaderBase
	{
		public ViewTableLoader()
			: base("View Table Data", "A Mini SQL Query Plugin for viewing table data.", 50)
		{
		}

		public override void InitializePlugIn()
		{
			// the DB inspector may not be present
			if (Services.HostWindow.DatabaseInspector != null)
			{
				Services.HostWindow.DatabaseInspector.TableMenu.Items.Insert(
					0, CommandControlBuilder.CreateToolStripMenuItem<ViewTableFromInspectorCommand>());
			}
			Services.HostWindow.AddPluginCommand<ViewTableFormCommand>();
		}
	}
}