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
			string xmlFile = Utility.GetConnectionStringFilename();
			IEditor editor = Services.Resolve<IFileEditorResolver>().ResolveEditorInstance(xmlFile);
			editor.FileName = xmlFile;
			editor.LoadFile();
			HostWindow.DisplayDockedForm(editor as DockContent);
		}

	}
}