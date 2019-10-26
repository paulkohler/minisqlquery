#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	/// <summary>The new file command.</summary>
	public class NewFileCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="NewFileCommand"/> class.</summary>
		public NewFileCommand()
			: base("New &File")
		{
			ShortcutKeys = Keys.Control | Keys.Alt | Keys.N;
			SmallImage = ImageResource.page;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			NewFileForm newFileForm = Services.Resolve<NewFileForm>();

			DialogResult result = newFileForm.ShowDialog();

			if (result == DialogResult.OK)
			{
				var editor = Services.Resolve<IEditor>(newFileForm.FileEditorDescriptor.EditorKeyName);
				editor.FileName = null;
				HostWindow.DisplayDockedForm(editor as DockContent);
			}
		}
	}
}