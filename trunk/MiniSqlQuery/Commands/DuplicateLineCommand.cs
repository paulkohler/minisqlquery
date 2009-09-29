using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	/// <summary>
	/// Whatever line the cursor is on this command will duplicate that line (its a resharper this that I use alot!)
	/// </summary>
	public class DuplicateLineCommand
		: CommandBase
	{
		public DuplicateLineCommand()
			: base("Duplicate Line")
		{
			ShortcutKeys = Keys.Control | Keys.D;
			//todo SmallImage = ImageResource.?;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ICommand"/> is enabled.
		/// The window needs to implement the <see cref="IFindReplaceProvider"/> and 
		/// support replacing text (<see cref="IFindReplaceProvider.CanReplaceText"/>).
		/// </summary>
		/// <value>
		/// 	<c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		public override bool Enabled
		{
			get
			{
				IFindReplaceProvider findReplaceProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;
				if (findReplaceProvider == null || !findReplaceProvider.CanReplaceText)
				{
					return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Finds the current line position and duplicates that line.
		/// </summary>
		public override void Execute()
		{
			IFindReplaceProvider findReplaceProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (findReplaceProvider == null || !findReplaceProvider.CanReplaceText)
			{
				return;
			}

			// todo!

			int offset = findReplaceProvider.CursorOffset;
			int originalLineStartOffset = 0;
			int lineLength = 0;

			string line = "?";
			// find current text "line", back to start or \n and find next \n or eof

			line = line + Environment.NewLine + line;

			findReplaceProvider.ReplaceString(line, 0, 0);
		}
	}
}