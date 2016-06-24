#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The copy query editor file name command.
	/// </summary>
	public class CopyQueryEditorFileNameCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "CopyQueryEditorFileNameCommand" /> class.
		/// </summary>
		public CopyQueryEditorFileNameCommand()
			: base("Copy Filename")
		{
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null && editor.FileName != null)
			{
				Clipboard.SetText(editor.FileName);
			}
		}
	}
}