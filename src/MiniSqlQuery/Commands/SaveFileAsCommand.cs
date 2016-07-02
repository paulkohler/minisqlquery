#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	/// <summary>The save file as command.</summary>
	public class SaveFileAsCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="SaveFileAsCommand"/> class.</summary>
		public SaveFileAsCommand()
			: base("Save File &As...")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null)
			{
				string oldFilename = editor.FileName;
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
				saveFileDialog.Filter = editor.FileFilter;

				if (saveFileDialog.ShowDialog(HostWindow.Instance) == DialogResult.OK)
				{
					// what if this filename covers an existing open window?
					string newFilename = saveFileDialog.FileName;
					editor.FileName = newFilename;
					editor.SaveFile();

					// register the new file and remove old if applicable
					var mostRecentFilesService = Services.Resolve<IMostRecentFilesService>();
					mostRecentFilesService.Register(newFilename);
					if (oldFilename != null && oldFilename.Equals(newFilename, StringComparison.InvariantCultureIgnoreCase))
					{
						mostRecentFilesService.Remove(oldFilename);
					}
				}
			}
		}
	}
}