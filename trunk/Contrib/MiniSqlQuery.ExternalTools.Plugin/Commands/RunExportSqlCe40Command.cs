using System;
using MiniSqlQuery.ExternalTools.Plugin.Properties;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
	public class RunExportSqlCe40Command : RunExportSqlCeCommandBase
	{
		public RunExportSqlCe40Command()
			: base("Run 'Export SQL CE 4.0' Tool")
		{
			SmallImage = Resources.data_out.ToBitmap();
		}

		public override void Execute()
		{
			RunExportSqlCe("ExportSqlCE40.exe");
		}
	}
}