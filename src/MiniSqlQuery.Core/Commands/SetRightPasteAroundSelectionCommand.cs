#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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