using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
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
				Settings.ResetConnection();
				Services.HostWindow.SetStatus(null, "Connection reset");
			}
			finally
			{
				Services.HostWindow.SetPointerState(Cursors.Default);
			}
		}
	}
}