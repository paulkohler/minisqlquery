using System;
using MiniSqlQuery.PlugIns.ViewTable.Commands;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	public class ViewTableLoader : IPlugIn
	{
		private IApplicationServices _services;

		public void LoadPlugIn(IApplicationServices services)
		{
			_services = services;
		}

		public string PluginName
		{
			get
			{
				return "View Table Data";
			}
		}

		public string PluginDescription
		{
			get
			{
				return "A Mini SQL Query Plugin for viewing table data.";
			}
		}

		public int RequestedLoadOrder
		{
			get
			{
				return 50;
			}
		}

		public void InitializePlugIn()
		{
			// the DB inspector may not be present
			if (_services.HostWindow.DatabaseInspector != null)
			{
				_services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
					CommandControlBuilder.CreateToolStripMenuItem<ViewTableFromInspectorCommand>());
			}
			_services.HostWindow.AddPluginCommand<ViewTableFormCommand>();
		}

		public void UnloadPlugIn()
		{
			// disposals?
		}
	}
}