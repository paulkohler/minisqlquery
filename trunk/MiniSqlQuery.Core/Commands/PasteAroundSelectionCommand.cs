#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>Description of PasteAroundSelectionCommand.</summary>
	public class PasteAroundSelectionCommand : CommandBase
	{
		/// <summary>The left text.</summary>
		public static string LeftText;

		/// <summary>The right text.</summary>
		public static string RightText;

		/// <summary>Initializes a new instance of the <see cref="PasteAroundSelectionCommand"/> class.</summary>
		public PasteAroundSelectionCommand()
			: base("Paste &Around Selection")
		{
			ShortcutKeys = Keys.Alt | Keys.A;
			SmallImage = ImageResource.around_text;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				string newText = string.Concat(LeftText, queryForm.SelectedText, RightText);
				queryForm.InsertText(newText);
			}
		}
	}
}