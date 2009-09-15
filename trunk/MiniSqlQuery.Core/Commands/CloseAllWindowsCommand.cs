using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class CloseAllWindowsCommand
		: CommandBase
	{
		public CloseAllWindowsCommand()
			: base("Close &All Windows")
		{
		}

		public override bool Enabled
		{
			get { return HostWindow.ActiveChildForm != null; }
		}

		public override void Execute()
		{
			Form[] forms = HostWindow.Instance.MdiChildren;
			if (forms != null)
			{
				foreach (Form frm in forms)
				{
					Application.DoEvents();
					frm.Close();
				}
			}
		}
	}
}