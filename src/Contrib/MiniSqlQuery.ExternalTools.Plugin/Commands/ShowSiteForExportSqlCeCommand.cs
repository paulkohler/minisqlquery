using System;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.ExternalTools.Plugin.Properties;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
	public class ShowSiteForExportSqlCeCommand
		: ShowUrlCommand
	{
		public ShowSiteForExportSqlCeCommand()
			: base("&Export SQL CE site (https://github.com/ErikEJ/SqlCeToolbox/)", "https://github.com/ErikEJ/SqlCeToolbox/", Resources.data_out.ToBitmap())
		{
		}
	}
}