using System;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class ShowDiscussionsWebPageCommand
		: ShowUrlCommand
	{
		public ShowDiscussionsWebPageCommand()
			: base("Show the discussions page on Codeplex", "http://minisqlquery.codeplex.com/Discussions/", ImageResource.comments)
		{
		}
	}
}