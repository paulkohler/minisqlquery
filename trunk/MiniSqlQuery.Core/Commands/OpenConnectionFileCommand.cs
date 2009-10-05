#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	public class OpenConnectionFileCommand
		: CommandBase
	{
		public OpenConnectionFileCommand()
			: base("Open the connections file")
		{
		}

		public override void Execute()
		{
			string xmlFile = Utility.GetConnectionStringFilename();
			IEditor editor = Services.Resolve<IFileEditorResolver>().ResolveEditorInstance(xmlFile);
			editor.FileName = xmlFile;
			editor.LoadFile();
			HostWindow.DisplayDockedForm(editor as DockContent);
		}

	}
}