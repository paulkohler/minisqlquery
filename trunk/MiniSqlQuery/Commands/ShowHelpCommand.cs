using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Diagnostics;
using MiniSqlQuery.Core;
using System.Windows.Forms;

namespace MiniSqlQuery.Commands
{
	public class ShowHelpCommand
		: CommandBase
	{
		public ShowHelpCommand()
			: base("&Index (pksoftware.net/MiniSqlQuery/Help)")
		{
			// TODO SmallImage = ImageResource. ?help;
		}

		public override void Execute()
		{
			// todo - version
			Process.Start("http://www.pksoftware.net/MiniSqlQuery/Help/");
		}

	}
}
