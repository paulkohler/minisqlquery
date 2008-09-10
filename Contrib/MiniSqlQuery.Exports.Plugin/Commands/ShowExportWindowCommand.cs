using System;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Exports.Plugin.Commands
{
	public class ShowExportWindowCommand : CommandBase
	{
		public ShowExportWindowCommand()
			: base("&Export data...")
		{
		}

		public override void Execute()
		{
			ExportWindow frm = new ExportWindow();
			frm.Show(Services.HostWindow.Instance);
		}
	}
}