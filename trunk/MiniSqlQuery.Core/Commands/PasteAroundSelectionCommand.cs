using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// Description of PasteAroundSelectionCommand.
	/// </summary>
	public class PasteAroundSelectionCommand : CommandBase
	{
		public static string LeftText;
		public static string RightText;

		public PasteAroundSelectionCommand()
			: base("Paste &Around Selection")
		{
			ShortcutKeys = Keys.Alt | Keys.A;
			SmallImage = ImageResource.around_text;
		}

		public override void Execute()
		{
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				string newText = string.Concat(LeftText, queryForm.SelectedText, RightText);
				queryForm.InsertText(newText);
			}
		}
	}

	public class SetLeftPasteAroundSelectionCommand : CommandBase
	{
		public SetLeftPasteAroundSelectionCommand()
			: base("Set Left Paste Around Selection text")
		{
			ShortcutKeys = Keys.Alt | Keys.F1;
		}

		public override void Execute()
		{
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				PasteAroundSelectionCommand.LeftText = queryForm.SelectedText;
			}
		}
	}

	public class SetRightPasteAroundSelectionCommand : CommandBase
	{
		public SetRightPasteAroundSelectionCommand()
			: base("Set Right Paste Around Selection text")
		{
			ShortcutKeys = Keys.Alt | Keys.F2;
		}

		public override void Execute()
		{
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				PasteAroundSelectionCommand.RightText = queryForm.SelectedText;
			}
		}
	}
}