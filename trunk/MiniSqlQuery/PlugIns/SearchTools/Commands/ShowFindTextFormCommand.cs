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
			get { return ApplicationServices.Instance.HostWindow.ActiveChildForm is IFindReplaceProvider; }
		}

		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			// if the window is an editor, grab the highlighted text
			IFindReplaceProvider findReplaceProvider = Services.HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (FindReplaceWindow == null || FindReplaceWindow.IsDisposed)
			{
				FindReplaceWindow = new FindReplaceForm(Services);
			}

			if (findReplaceProvider is IEditor)
			{
				FindReplaceWindow.FindString = ((IEditor)findReplaceProvider).SelectedText;
			}

			FindReplaceWindow.TopMost = true;

			if (!FindReplaceWindow.Visible)
			{
				FindReplaceWindow.Show(Services.HostWindow.Instance);
			}
		}
	}
}