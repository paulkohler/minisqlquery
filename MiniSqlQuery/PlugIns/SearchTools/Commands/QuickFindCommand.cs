using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	public class QuickFindCommand : CommandBase
	{
		public QuickFindCommand() : base("&Quick Find String")
		{
			SmallImage = ImageResource.plugin_go;
		}

		public override void Execute()
		{
			//FindReplaceForm frm = new FindReplaceForm();
			//frm.ShowDialog(Services.HostWindow.Instance);
		}
	}
}
