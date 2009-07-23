using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class CloseActiveWindowCommand
		: CommandBase
	{
		public CloseActiveWindowCommand()
			: base("&Close")
		{
		}

		public override bool Enabled
		{
			get { return Services.HostWindow.ActiveChildForm != null; }
		}

		public override void Execute()
		{
			Form frm = Services.HostWindow.ActiveChildForm;
			if (frm != null)
			{
				frm.Close();
			}
		}
	}
}