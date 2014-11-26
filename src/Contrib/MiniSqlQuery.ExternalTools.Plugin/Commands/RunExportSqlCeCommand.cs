using System;
using MiniSqlQuery.ExternalTools.Plugin.Properties;

namespace MiniSqlQuery.ExternalTools.Plugin.Commands
{
	public class RunExportSqlCeCommand : RunExportSqlCeCommandBase
	{
		public RunExportSqlCeCommand()
			: base("Run 'Export SQL CE 3.5' Tool")
		{
			SmallImage = Resources.data_out.ToBitmap();
		}

		public override void Execute()
		{
			RunExportSqlCe("ExportSqlCE.exe");
		}
	}
}