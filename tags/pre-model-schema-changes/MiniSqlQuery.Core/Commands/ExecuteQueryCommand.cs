using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class ExecuteQueryCommand
		: CommandBase
	{
		public ExecuteQueryCommand()
			: base("&Execute Query")
		{
			ShortcutKeys = Keys.F5;
			SmallImage = ImageResource.lightning;
		}

		public override bool Enabled
		{
			get
			{
				IQueryEditor editor = ActiveFormAsEditor;
				if (editor != null)
				{
					return !editor.IsBusy;
				}
				return false;
			}
		}

		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			IQueryEditor editor = ActiveFormAsEditor;
			if (editor != null)
			{
				editor.ExecuteQuery();
			}
		}
	}
}