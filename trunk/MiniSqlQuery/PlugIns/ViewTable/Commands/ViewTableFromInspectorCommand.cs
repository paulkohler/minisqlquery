using System;
using System.Collections;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable.Commands
{
	public class ViewTableFromInspectorCommand
		: CommandBase
	{
		public ViewTableFromInspectorCommand()
			: base("&View table data")
		{
			SmallImage = ImageResource.table_go;
		}

		public override void Execute()
		{
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;
			if (tableName != null)
			{
				IViewTable frm = Services.Resolve<IViewTable>();
				frm.TableName = tableName;
				Services.HostWindow.DisplayDockedForm(frm as DockContent);
			}
		}
	}
}