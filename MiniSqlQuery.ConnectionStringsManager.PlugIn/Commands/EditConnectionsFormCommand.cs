using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.ConnectionStringsManager.PlugIn.Commands
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
			DbConnectionsForm frm = new DbConnectionsForm();
			frm.ShowDialog(Services.HostWindow.Instance);
        }
    }
}
