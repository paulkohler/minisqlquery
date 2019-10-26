#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The show web page command.
	/// </summary>
	public class ShowWebPageCommand
		: ShowUrlCommand
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "ShowWebPageCommand" /> class.
		/// </summary>
		public ShowWebPageCommand()
			: base("Mini SQL Query on GitHub", "https://github.com/paulkohler/minisqlquery", ImageResource.house)
		{
		}
	}
}