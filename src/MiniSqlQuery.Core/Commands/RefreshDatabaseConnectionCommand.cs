#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
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