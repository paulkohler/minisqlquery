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
				HostWindow.SetPointerState(Cursors.WaitCursor);
				Settings.ResetConnection();
				HostWindow.SetStatus(null, "Connection reset");
			}
			finally
			{
				HostWindow.SetPointerState(Cursors.Default);
			}
		}
	}
}