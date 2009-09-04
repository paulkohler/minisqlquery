using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
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
			string key = "txt-editor";
			//todo ask!
			IEditor editor = Services.Resolve<IEditor>(key);
			editor.FileName = null;
			Services.HostWindow.DisplayDockedForm(editor as DockContent);
		}
	}
}