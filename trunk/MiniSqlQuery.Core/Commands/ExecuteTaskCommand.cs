using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class ExecuteTaskCommand
		: CommandBase
	{
		public ExecuteTaskCommand()
			: base("&Execute")
		{
			ShortcutKeys = Keys.F5;
			ShortcutKeysText = "F5";
			SmallImage = ImageResource.lightning;
		}

		public override bool Enabled
		{
			get
			{
				IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
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

			IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
			if (editor != null)
			{
				editor.ExecuteTask();
			}
		}
	}
}