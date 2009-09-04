using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.TemplateViewer.Commands
{
	public class RunTemplateCommand
		: CommandBase
	{
		public RunTemplateCommand()
			: base("Run Template")
		{
			SmallImage = ImageResource.script_code;
		}

		public override void Execute()
		{
			ITemplateEditor templateEditor = Services.HostWindow.ActiveChildForm as ITemplateEditor;

			if (templateEditor != null)
			{
				templateEditor.RunTemplate();
			}
		}

		public override bool Enabled
		{
			get
			{
				return Services.HostWindow.ActiveChildForm is ITemplateEditor;
			}
		}
	}
}