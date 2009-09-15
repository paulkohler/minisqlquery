using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	public class NewQueryFormCommand
		: CommandBase
	{
		public NewQueryFormCommand()
			: base("New &Query Window")
		{
			ShortcutKeys = Keys.Control | Keys.N;
			SmallImage = ImageResource.page_white;
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.Container.Resolve<IQueryEditor>();
			editor.FileName = null;
			HostWindow.DisplayDockedForm(editor as DockContent);
		}
	}
}