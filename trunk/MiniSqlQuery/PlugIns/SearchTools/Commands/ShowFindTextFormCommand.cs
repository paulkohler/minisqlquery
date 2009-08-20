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


		public override bool Enabled
		{
			// todo - support: get { return ApplicationServices.Instance.HostWindow.ActiveChildForm is INavigatableDocument; }
			get { return ApplicationServices.Instance.HostWindow.ActiveChildForm is IQueryEditor; }
		}

		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			// if the window is an editor, grab the highlighted text
			IQueryEditor editor = ActiveFormAsEditor;

			if (FindReplaceWindow == null || FindReplaceWindow.IsDisposed)
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