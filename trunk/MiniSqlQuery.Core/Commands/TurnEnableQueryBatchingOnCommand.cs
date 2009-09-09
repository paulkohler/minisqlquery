using System;

namespace MiniSqlQuery.Core.Commands
{
	public class TurnEnableQueryBatchingOnCommand
		: CommandBase
	{
		public TurnEnableQueryBatchingOnCommand()
			: base("Turn 'Batch Query' mode On")
		{
		}

		public override bool Enabled
		{
			get { return Settings.EnableQueryBatching == false; }
		}

		public override void Execute()
		{
			Settings.EnableQueryBatching = true;
		}
	}
}