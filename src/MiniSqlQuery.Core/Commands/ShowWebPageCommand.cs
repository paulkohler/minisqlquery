#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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