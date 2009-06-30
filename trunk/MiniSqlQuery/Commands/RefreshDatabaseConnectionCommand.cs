using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class RefreshDatabaseConnectionCommand
        : CommandBase
    {
		public RefreshDatabaseConnectionCommand()
            : base("&Refresh Database Connection")
        {
			SmallImage = ImageResource.database_refresh;
        }

        public override void Execute()
        {
			try
			{
				Services.HostWindow.SetPointerState(Cursors.WaitCursor);
				Services.Settings.ResetConnection();
				Services.HostWindow.SetStatus(null, "Connection reset");
			}
			finally
			{
				Services.HostWindow.SetPointerState(Cursors.Default);
			}
		}

    }
}
