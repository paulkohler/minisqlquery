using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Exports.Plugin
{
	public class Loader : PluginLoaderBase
	{
		public Loader()
			: base("Exports for MiniSqlQuery", "Enables exporting of data")
		{
		}

		public override void InitializePlugIn()
		{
			Services.HostWindow.AddPluginCommand<Commands.ShowExportWindowCommand>();
		}
	}
}