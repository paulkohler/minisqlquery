#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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