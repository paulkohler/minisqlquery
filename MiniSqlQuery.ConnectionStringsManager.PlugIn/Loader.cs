using System;
using MiniSqlQuery.ConnectionStringsManager.PlugIn.Commands;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn
{
	public class Loader : PluginLoaderBase
	{
		public Loader() : base(
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
