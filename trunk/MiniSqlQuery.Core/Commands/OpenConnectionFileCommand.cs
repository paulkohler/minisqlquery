using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	public class OpenConnectionFileCommand
		: CommandBase
	{
		public OpenConnectionFileCommand()
			: base("Open the connections file")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.Container.Resolve<IQueryEditor>();
			editor.FileName = Utility.GetConnectionStringFilename();
			editor.LoadFile();
			HostWindow.DisplayDockedForm(editor as DockContent);
		}

	}
}