using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	public class OpenFileCommand
        : CommandBase
    {
		public OpenFileCommand()
            : base("&Open File")
        {
            ShortcutKeys = Keys.Control | Keys.O;
			SmallImage = ImageResource.folder_page;
        }

        public override void Execute()
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			openFileDialog.Filter = Settings.DefaultFileFilter;
			openFileDialog.CheckFileExists = true;
			if (openFileDialog.ShowDialog(HostWindow.Instance) == DialogResult.OK)
			{
				//todo: check for file exist file in open windows;

				IFileEditorResolver resolver = Services.Resolve<IFileEditorResolver>();
				IEditor editor = resolver.ResolveEditorInstance(openFileDialog.FileName);
				editor.FileName = openFileDialog.FileName;
				editor.LoadFile();
				HostWindow.DisplayDockedForm(editor as DockContent);
			}
        }
	}
}
