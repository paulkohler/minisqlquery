using System;
using System.Collections.Generic;
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
            : base("&Open Query File")
        {
            ShortcutKeys = Keys.Control | Keys.O;
			SmallImage = ImageResource.folder_page;
        }

        public override void Execute()
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			openFileDialog.Filter = Services.Settings.DefaultFileFilter;
			openFileDialog.CheckFileExists = true;
			if (openFileDialog.ShowDialog(Services.HostWindow.Instance) == DialogResult.OK)
			{
				//todo: check for file exist file in open windows;

				IQueryEditor editor = Services.Container.Resolve<IQueryEditor>();
				editor.FileName = openFileDialog.FileName;
				editor.LoadFile();
				Services.HostWindow.DisplayDockedForm(editor as DockContent);
			}

        }
    }
}
