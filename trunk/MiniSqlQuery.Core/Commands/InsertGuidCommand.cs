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
			IEditor editor = ActiveFormAsEditor;
			if (editor != null)
			{
				editor.InsertText(Guid.NewGuid().ToString());
			}
		}

		public override bool Enabled
		{
			get
			{
				return ActiveFormAsEditor != null;
			}
		}
	}
}