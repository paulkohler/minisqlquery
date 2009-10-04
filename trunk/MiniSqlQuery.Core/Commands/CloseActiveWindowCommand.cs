#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class CloseActiveWindowCommand
		: CommandBase
	{
		public CloseActiveWindowCommand()
			: base("&Close")
		{
		}

		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm != null; }
		}

		public override void Execute()
		{
			Form frm = HostWindow.ActiveChildForm;
			if (frm != null)
			{
				frm.Close();
			}
		}
	}
}