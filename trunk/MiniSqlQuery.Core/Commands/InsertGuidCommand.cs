using System;

namespace MiniSqlQuery.Core.Commands
{
	public class InsertGuidCommand : CommandBase
	{
		public InsertGuidCommand()
			: base("Insert GUID")
		{
			//todo SmallImage = ImageResource.;
		}

		public override void Execute()
		{
			IQueryEditor queryForm = ActiveFormAsEditor;
			if (queryForm != null)
			{
				queryForm.InsertText(Guid.NewGuid().ToString());
			}
		}
	}
}