#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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
			: base("&Index (github.com/paul-kohler-au/minisqlquery)", "https://github.com/paul-kohler-au/minisqlquery", ImageResource.help)
		{
		}
	}
}