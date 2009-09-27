using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.ExternalTools.Plugin.Commands;

namespace MiniSqlQuery.ExternalTools.Plugin
{
	public class Loader : PluginLoaderBase
	{
		public Loader()
			: base("Run External Tools Wrapper", "A plugin that wraps executing external tools.")
		{
		}

		public override void InitializePlugIn()
		{
			Services.HostWindow.AddPluginCommand<RunExportSqlCeCommand>();
			Services.HostWindow.AddPluginCommand<ShowSiteForExportSqlCeCommand>();
		}
	}
}