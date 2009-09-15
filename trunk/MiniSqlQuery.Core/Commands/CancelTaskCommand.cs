using System;

namespace MiniSqlQuery.Core.Commands
{
	public class CancelTaskCommand
		: CommandBase
	{
		public CancelTaskCommand()
			: base("&Cancel")
		{
			//ShortcutKeys = Keys.;
			SmallImage = ImageResource.stop;
		}

		public override bool Enabled
		{
			get
			{
				IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
				if (editor != null)
				{
					return editor.IsBusy;
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
				editor.CancelTask();
			}
		}
	}
}