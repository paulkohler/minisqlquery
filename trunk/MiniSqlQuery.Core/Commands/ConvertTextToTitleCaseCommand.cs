#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Globalization;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The convert text to title case command.</summary>
	public class ConvertTextToTitleCaseCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ConvertTextToTitleCaseCommand"/> class.</summary>
		public ConvertTextToTitleCaseCommand()
			: base("Convert to 'Title Case' text")
		{
			ShortcutKeys = Keys.Control | Keys.Alt | Keys.U;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		/// <value>The enabled.</value>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm as IEditor != null; }
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IEditor editor = ActiveFormAsEditor;
			if (Enabled && editor.SelectedText.Length > 0)
			{
				editor.InsertText(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editor.SelectedText));
			}
		}
	}
}