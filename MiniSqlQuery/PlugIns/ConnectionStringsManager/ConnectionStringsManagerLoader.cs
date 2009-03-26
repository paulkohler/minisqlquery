using System;
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
			Services.HostWindow.AddPluginCommand<EditConnectionsFormCommand>();
			Services.HostWindow.AddToolStripCommand<EditConnectionsFormCommand>(null);
		}
	}
}