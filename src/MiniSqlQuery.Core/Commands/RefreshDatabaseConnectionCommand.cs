﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The refresh database connection command.
	/// </summary>
	public class RefreshDatabaseConnectionCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "RefreshDatabaseConnectionCommand" /> class.
		/// </summary>
		public RefreshDatabaseConnectionCommand()
			: base("&Refresh Database Connection")
		{
			SmallImage = ImageResource.database_refresh;
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			try
			{
				HostWindow.SetPointerState(Cursors.WaitCursor);
				Settings.ResetConnection();
				HostWindow.SetStatus(null, "Connection reset");
			}
			finally
			{
				HostWindow.SetPointerState(Cursors.Default);
			}
		}
	}
}