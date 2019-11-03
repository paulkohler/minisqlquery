#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The open connection file command.
	/// </summary>
	public class OpenConnectionFileCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "OpenConnectionFileCommand" /> class.
		/// </summary>
		public OpenConnectionFileCommand()
			: base("Open the connections file")
		{
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
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