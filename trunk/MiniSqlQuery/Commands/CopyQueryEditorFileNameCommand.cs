using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class CopyQueryEditorFileNameCommand
		: CommandBase
	{
		public CopyQueryEditorFileNameCommand()
			: base("Copy Filename")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;
			if (editor != null && editor.FileName != null)
			{
				Clipboard.SetText(editor.FileName);
			}
		}
	}
}
