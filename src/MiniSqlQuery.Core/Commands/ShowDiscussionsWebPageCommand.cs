#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The show discussions web page command.
	/// </summary>
	public class ShowDiscussionsWebPageCommand
		: ShowUrlCommand
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "ShowDiscussionsWebPageCommand" /> class.
		/// </summary>
		public ShowDiscussionsWebPageCommand()
			: base("Show the discussions page on Codeplex", "http://minisqlquery.codeplex.com/Thread/List.aspx", ImageResource.comments)
		{
		}
	}
}