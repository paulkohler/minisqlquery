#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	/// <summary>The save file command.</summary>
	public class SaveFileCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="SaveFileCommand"/> class.</summary>
		public SaveFileCommand()
			: base("&Save File")
		{
			ShortcutKeys = Keys.Control | Keys.S;
			SmallImage = ImageResource.disk;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		public override bool Enabled
		{
			get
			{
				IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
				if (editor != null)
				{
					return editor.IsDirty;
				}

				return false;
			}
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null)
			{
				if (editor.FileName == null)
				{
					CommandManager.GetCommandInstance<SaveFileAsCommand>().Execute();
				}
				else
				{
					editor.SaveFile();
				}
			}
		}
	}
}