using System;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
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
			get { return Services.Settings.EnableQueryBatching == false; }
		}

		public override void Execute()
		{
			Services.Settings.EnableQueryBatching = true;
		}
	}
}