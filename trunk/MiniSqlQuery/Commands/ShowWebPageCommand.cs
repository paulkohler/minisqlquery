using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class ShowWebPageCommand
		: ShowUrlCommand
	{
		public ShowWebPageCommand()
			: base("Mini SQL Query on Codeplex", "http://minisqlquery.codeplex.com/", ImageResource.house)
		{
		}
	}
}