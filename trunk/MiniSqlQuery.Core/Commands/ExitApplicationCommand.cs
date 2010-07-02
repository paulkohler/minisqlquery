#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The exit application command.</summary>
	public class ExitApplicationCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ExitApplicationCommand"/> class.</summary>
		public ExitApplicationCommand()
			: base("E&xit")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			HostWindow.Instance.Close();
		}
	}
}