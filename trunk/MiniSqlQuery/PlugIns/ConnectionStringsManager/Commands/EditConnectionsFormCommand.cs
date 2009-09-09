using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.PlugIns.ConnectionStringsManager;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager.Commands
{
	public class EditConnectionsFormCommand : CommandBase
	{
		public EditConnectionsFormCommand()
			: base("&Edit Connection Strings")
		{
			SmallImage = ImageResource.database_edit;
		}

		public override void Execute()
		{
			DbConnectionsForm frm = new DbConnectionsForm(Services, Settings);
			frm.ShowDialog(Services.HostWindow.Instance);
		}
	}
}