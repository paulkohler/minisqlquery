using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	public class SaveFileCommand
        : CommandBase
    {
		public SaveFileCommand()
            : base("&Save File")
        {
			ShortcutKeys = Keys.Control | Keys.S;
			SmallImage = ImageResource.disk;
        }

        public override void Execute()
        {
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null)
			{
				if (editor.FileName == null)
				{
					CommandManager.GetCommandInstance<SaveFileAsCommand>().Execute();
				}
				else
				{
					editor.SaveFile();
				}
			}
		}

		public override bool Enabled
		{
			get
			{
				IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
				if (editor != null)
				{
					return editor.IsDirty;
				}
				return false;
			}
		}
    }
}
