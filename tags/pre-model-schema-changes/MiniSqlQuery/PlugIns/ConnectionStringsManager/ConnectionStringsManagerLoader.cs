using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.ConnectionStringsManager.Commands;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	public class ConnectionStringsManagerLoader : PluginLoaderBase
	{
		public ConnectionStringsManagerLoader() : base(
			"Connection String Manager", 
			"A Mini SQL Query Plugin for managing the list of connection strings.", 
			10)
		{
		}

		public override void InitializePlugIn()
		{
			ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<EditConnectionsFormCommand>());
			Services.HostWindow.AddToolStripCommand<EditConnectionsFormCommand>(null);
		}
	}
}