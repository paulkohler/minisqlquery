using System;

namespace MiniSqlQuery.Core.Commands
{
	public class ExitApplicationCommand
		: CommandBase
	{
		public ExitApplicationCommand()
			: base("E&xit")
		{
		}

		public override void Execute()
		{
			Services.HostWindow.Instance.Close();
		}
	}
}