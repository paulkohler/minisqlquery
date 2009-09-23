using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	public class ShowOptionsFormCommand
		: CommandBase
	{
		public ShowOptionsFormCommand()
			: base("Options")
		{
			//ShortcutKeys = ?;
			SmallImage = ImageResource.cog;
		}

		public override void Execute()
		{
			using (OptionsForm optionsForm = Services.Resolve<OptionsForm>())
			{
				optionsForm.ShowDialog(HostWindow.Instance);
			}
		}
	}
}