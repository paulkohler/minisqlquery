using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
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
			Services.HostWindow.DisplayDockedForm(editor as DockContent);
        }
    }
}
