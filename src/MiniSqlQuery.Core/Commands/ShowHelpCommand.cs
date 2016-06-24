#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

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
			: base("&Index (pksoftware.net/MiniSqlQuery/Help)", "http://www.pksoftware.net/MiniSqlQuery/Help/", ImageResource.help)
		{
		}
	}
}