using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.TemplateViewer.Commands
{
    public class NewQueryByTemplateCommand
        : CommandBase
    {
		public NewQueryByTemplateCommand()
            : base("New &Query from Template")
        {
			SmallImage = ImageResource.script_code;
		}

        public override void Execute()
        {
			ICommand newQueryFormCommand = CommandManager.GetCommandInstance("NewQueryFormCommand");
			newQueryFormCommand.Execute();

        }
    }
}
