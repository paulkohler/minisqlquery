using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	public class ShowFindTextFormCommand : CommandBase
	{
		public ShowFindTextFormCommand()
			: base("&Find Text...")
		{
			SmallImage = ImageResource.find;
			ShortcutKeys = Keys.Control | Keys.F;
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;

			FindReplaceForm frm = new FindReplaceForm();
			if (editor != null)
			{
				frm.FindString = editor.SelectedText;
			}

			frm.TopMost = true;
			frm.Show(Services.HostWindow.Instance);
		}
	}
}