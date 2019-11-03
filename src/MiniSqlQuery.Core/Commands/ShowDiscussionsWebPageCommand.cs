#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

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