#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The close all windows command.</summary>
	public class CloseAllWindowsCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="CloseAllWindowsCommand"/> class.</summary>
		public CloseAllWindowsCommand()
			: base("Close &All Windows")
		{
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		/// <value>The enabled.</value>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm != null; }
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			Form[] forms = HostWindow.Instance.MdiChildren;
			foreach (Form frm in forms)
			{
				Application.DoEvents();
				frm.Close();
			}
		}
	}
}