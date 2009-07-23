using System;

namespace MiniSqlQuery.Core.Commands
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