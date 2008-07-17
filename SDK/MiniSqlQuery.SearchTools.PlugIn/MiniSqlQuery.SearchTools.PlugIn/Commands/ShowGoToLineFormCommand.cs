using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.SearchTools.PlugIn.Commands
{
	public class ShowGoToLineFormCommand : CommandBase
	{
		public ShowGoToLineFormCommand() : base("Go To...")
		{
			ShortcutKeys = Keys.Control | Keys.G;
		}

		public override void Execute()
		{
			if (Enabled)
			{
				GoToLineForm frm = new GoToLineForm();
				frm.ShowDialog(ApplicationServices.Instance.HostWindow as Form);
			}
		}

		public override bool Enabled
		{
			get
			{
				return ApplicationServices.Instance.HostWindow.ActiveChildForm is IQueryEditor;
			}
		}
	}
}
