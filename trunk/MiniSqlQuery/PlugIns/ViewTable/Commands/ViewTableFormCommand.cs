using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable.Commands
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
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				string tableName = queryForm.SelectedText;
				IViewTable frm = Services.Resolve<IViewTable>();
				frm.TableName = tableName;
				HostWindow.DisplayDockedForm(frm as DockContent);
			}
		}
	}
}