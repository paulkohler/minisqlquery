using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.ViewTable.PlugIn.Commands
{
	public class ViewTableFormCommand
        : CommandBase
    {
        public ViewTableFormCommand()
            : base("&View table...")
        {
            ShortcutKeys = Keys.Control | Keys.T;
        }

        public override void Execute()
        {
			IQueryEditor queryForm = Services.HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
            {
				string tableName = queryForm.SelectedText;
				ViewTableForm frm = new ViewTableForm(Services, tableName);
				frm.Text = tableName;
				Services.HostWindow.DisplayDockedForm(frm);
            }
        }
    }
}
