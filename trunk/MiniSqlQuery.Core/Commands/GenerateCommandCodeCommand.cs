using System;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	public class GenerateCommandCodeCommand
		: CommandBase
	{
		public GenerateCommandCodeCommand()
			: base("Generate Command Code")
		{
			SmallImage = ImageResource.cog;
		}

		public override void Execute()
		{
			string template = @"    public class $name$Command
        : CommandBase
    {
        public $name$Command()
            : base(""$desc$"")
        {
            //ShortcutKeys = Keys.Control | Keys.?;
			//SmallImage = ImageResource.?;
		}

        public override void Execute()
        {
			
        }
    }";

			string code = template
				.Replace("$name$", "OI")
				.Replace("$desc$", "a thing");


			IQueryEditor editor = Services.Container.Resolve<IQueryEditor>();
			editor.AllText = code;
			editor.SetSyntax("C#");

			HostWindow.DisplayDockedForm(editor as DockContent);
		}
	}
}