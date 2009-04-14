using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	public class ShowGoToLineFormCommand : CommandBase
	{
		public ShowGoToLineFormCommand() 
			: base("Go To...")
		{
			ShortcutKeys = Keys.Control | Keys.G;
		}

		public override bool Enabled
		{
			get { return ApplicationServices.Instance.HostWindow.ActiveChildForm is IQueryEditor; }
		}

		public override void Execute()
		{
			if (Enabled)
			{
				GoToLineForm frm = new GoToLineForm();
				frm.ShowDialog(ApplicationServices.Instance.HostWindow as Form);
			}
		}
	}
}