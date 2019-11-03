#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	/// <summary>The show go to line form command.</summary>
	public class ShowGoToLineFormCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowGoToLineFormCommand"/> class.</summary>
		public ShowGoToLineFormCommand()
			: base("Go To Line...")
		{
			ShortcutKeys = Keys.Control | Keys.G;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm is INavigatableDocument; }
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			if (Enabled)
			{
				GoToLineForm frm = Services.Resolve<GoToLineForm>();
				frm.ShowDialog(HostWindow as Form);
			}
		}
	}
}