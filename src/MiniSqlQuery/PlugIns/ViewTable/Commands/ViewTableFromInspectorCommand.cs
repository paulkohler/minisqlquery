#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable.Commands
{
	/// <summary>The view table from inspector command.</summary>
	public class ViewTableFromInspectorCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ViewTableFromInspectorCommand"/> class.</summary>
		public ViewTableFromInspectorCommand()
			: base("&View table data")
		{
			SmallImage = ImageResource.table_go;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			string tableName = HostWindow.DatabaseInspector.RightClickedTableName;
			if (tableName != null)
			{
				IViewTable frm = Services.Resolve<IViewTable>();
				frm.TableName = tableName;
				HostWindow.DisplayDockedForm(frm as DockContent);
			}
		}
	}
}