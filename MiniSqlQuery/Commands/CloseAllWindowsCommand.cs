using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;

namespace MiniSqlQuery.Commands
{
	public class CloseAllWindowsCommand
		: CommandBase
	{
		public CloseAllWindowsCommand()
			: base("Close &All Windows")
		{
		}

		public override void Execute()
		{
			Form[] forms = Services.HostWindow.Instance.MdiChildren;
			if (forms != null)
			{
				foreach (Form frm in forms)
				{
					frm.Close();
				}
			}
		}

		public override bool Enabled
		{
			get
			{
				return Services.HostWindow.ActiveChildForm != null;
			}
		}
	}
}
