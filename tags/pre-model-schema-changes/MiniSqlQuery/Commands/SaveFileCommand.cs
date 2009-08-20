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
			IQueryEditor queryForm = Services.HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				if (queryForm.FileName == null)
				{
					CommandManager.GetCommandInstance<SaveFileAsCommand>().Execute();
				}
				else
				{
					queryForm.SaveFile();
				}
			}
		}

		public override bool Enabled
		{
			get
			{
				IQueryEditor queryForm = Services.HostWindow.Instance.ActiveMdiChild as IQueryEditor;
				if (queryForm != null)
				{
					return queryForm.IsDirty;
				}
				return false;
			}
		}
    }
}
