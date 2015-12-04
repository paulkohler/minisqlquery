#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The convert text to lower case command.</summary>
	public class ConvertTextToLowerCaseCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ConvertTextToLowerCaseCommand"/> class.</summary>
		public ConvertTextToLowerCaseCommand()
			: base("Convert to 'lower case' text")
		{
			ShortcutKeys = Keys.Control | Keys.U;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		/// <value>The enabled state.</value>
		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm as IEditor != null; }
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			var editor = ActiveFormAsEditor;
			if (Enabled && editor.SelectedText.Length > 0)
			{
				editor.InsertText(editor.SelectedText.ToLower());
			}
		}
	}
}