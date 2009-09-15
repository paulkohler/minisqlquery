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
			HostWindow.Instance.Close();
		}
	}
}