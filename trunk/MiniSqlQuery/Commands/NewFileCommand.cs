using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	public class NewFileCommand
		: CommandBase
	{
		public NewFileCommand()
			: base("New &File")
		{
			ShortcutKeys = Keys.Control | Keys.Alt | Keys.N;
			SmallImage = ImageResource.page;
		}

		public override void Execute()
		{
			NewFileForm newFileForm = Services.Resolve<NewFileForm>();

			DialogResult result = newFileForm.ShowDialog();

			if (result == DialogResult.OK)
			{
				var editor = Services.Resolve<IEditor>(newFileForm.FileEditorDescriptor.EditorKeyName);
				editor.FileName = null;
				Services.HostWindow.DisplayDockedForm(editor as DockContent);
			}
		}
	}
}