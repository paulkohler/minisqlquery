using System;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.ExternalTools.Plugin.Properties;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
	public class ShowSiteForExportSqlCeCommand
		: ShowUrlCommand
	{
		public ShowSiteForExportSqlCeCommand()
			: base("&Export SQL CE site (http://exportsqlce.codeplex.com/)", "http://exportsqlce.codeplex.com/", Resources.data_out.ToBitmap())
		{
		}
	}
}