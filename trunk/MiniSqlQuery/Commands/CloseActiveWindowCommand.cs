using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;

namespace MiniSqlQuery.Commands
{
	public class CloseActiveWindowCommand
		: CommandBase
	{
		public CloseActiveWindowCommand()
			: base("&Close")
		{
		}

		public override void Execute()
		{
			Form frm = Services.HostWindow.ActiveChildForm;
			if (frm != null)
			{
				frm.Close();
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
