#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class CopyQueryEditorFileNameCommand
		: CommandBase
	{
		public CopyQueryEditorFileNameCommand()
			: base("Copy Filename")
		{
		}

		public override void Execute()
		{
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null && editor.FileName != null)
			{
				Clipboard.SetText(editor.FileName);
			}
		}
	}
}