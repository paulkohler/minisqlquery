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
		: ShowUrlCommand
	{
		public ShowHelpCommand()
			: base("&Index (pksoftware.net/MiniSqlQuery/Help)", "http://www.pksoftware.net/MiniSqlQuery/Help/", ImageResource.help)
		{
		}
	}
}
