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
	/// 	The set right paste around selection command.
	/// </summary>
	public class SetRightPasteAroundSelectionCommand : CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "SetRightPasteAroundSelectionCommand" /> class.
		/// </summary>
		public SetRightPasteAroundSelectionCommand()
			: base("Set Right Paste Around Selection text")
		{
			ShortcutKeys = Keys.Alt | Keys.F2;
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				PasteAroundSelectionCommand.RightText = queryForm.SelectedText;
			}
		}
	}
}