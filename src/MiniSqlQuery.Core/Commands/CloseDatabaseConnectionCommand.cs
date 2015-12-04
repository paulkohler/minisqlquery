#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System.Data;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The close database connection command.</summary>
	public class CloseDatabaseConnectionCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="CloseDatabaseConnectionCommand"/> class.</summary>
		public CloseDatabaseConnectionCommand()
			: base("Close Current connection")
		{
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		/// <value>The enabled state.</value>
		public override bool Enabled
		{
			get
			{
				if (Settings.Connection == null ||
					(Settings.Connection.State == ConnectionState.Closed &&
					Settings.Connection.State == ConnectionState.Broken))
				{
					return false;
				}

				return true;
			}
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			Settings.CloseConnection();
		}
	}
}