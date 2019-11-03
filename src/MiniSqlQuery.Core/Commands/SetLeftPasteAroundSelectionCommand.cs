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
	/// 	The set left paste around selection command.
	/// </summary>
	public class SetLeftPasteAroundSelectionCommand : CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "SetLeftPasteAroundSelectionCommand" /> class.
		/// </summary>
		public SetLeftPasteAroundSelectionCommand()
			: base("Set Left Paste Around Selection text")
		{
			ShortcutKeys = Keys.Alt | Keys.F1;
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				PasteAroundSelectionCommand.LeftText = queryForm.SelectedText;
			}
		}
	}
}