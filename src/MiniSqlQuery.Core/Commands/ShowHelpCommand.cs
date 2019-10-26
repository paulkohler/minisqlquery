#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The show help command.
	/// </summary>
	public class ShowHelpCommand
		: ShowUrlCommand
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "ShowHelpCommand" /> class.
		/// </summary>
		public ShowHelpCommand()
			: base("&Index (github.com ... Quickstart.md)", "https://github.com/paulkohler/minisqlquery/blob/master/src/Docs/Quickstart.md", ImageResource.help)
		{
		}
	}
}