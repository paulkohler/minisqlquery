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

		public IFindReplaceWindow FindReplaceWindow { get; private set; }

		public override void Execute()
		{
			// if the window is an editor, grab the highlighted text
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;

			if (FindReplaceWindow == null)
			{
				FindReplaceWindow = new FindReplaceForm(Services);
			}

			if (editor != null)
			{
				FindReplaceWindow.FindString = editor.SelectedText;
			}

			FindReplaceWindow.TopMost = true;

			if (!FindReplaceWindow.Visible)
			{
				FindReplaceWindow.Show(Services.HostWindow.Instance);
			}
		}
	}
}