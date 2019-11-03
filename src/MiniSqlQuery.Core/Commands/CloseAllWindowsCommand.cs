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
	/// 	The close all windows command.
	/// </summary>
	public class CloseAllWindowsCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "CloseAllWindowsCommand" /> class.
		/// </summary>
		public CloseAllWindowsCommand()
			: base("Close &All Windows")
		{
		}

		/// <summary>
		/// 	Gets a value indicating whether Enabled.
		/// </summary>
		/// <value>The enabled state.</value>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm != null; }
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var forms = HostWindow.Instance.MdiChildren;
			foreach (var frm in forms)
			{
				Application.DoEvents();
				frm.Close();
			}
		}
	}
}