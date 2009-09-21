using System;
using System.Collections.Generic;
using System.Collections;
using MiniSqlQuery.Core.Forms;

namespace MiniSqlQuery.Core.Commands
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
			using (OptionsForm optionsForm = new OptionsForm(Services))
			{
				optionsForm.ShowDialog(HostWindow.Instance);
			}
		}
	}
}