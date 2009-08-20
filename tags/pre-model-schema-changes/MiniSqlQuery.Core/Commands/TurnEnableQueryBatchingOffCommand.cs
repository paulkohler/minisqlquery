using System;

namespace MiniSqlQuery.Core.Commands
{
	public class TurnEnableQueryBatchingOffCommand
		: CommandBase
	{
		public TurnEnableQueryBatchingOffCommand()
			: base("Turn 'Batch Query' mode Off")
		{
		}

		public override bool Enabled
		{
			get { return Services.Settings.EnableQueryBatching; }
		}

		public override void Execute()
		{
			Services.Settings.EnableQueryBatching = false;
		}
	}
}