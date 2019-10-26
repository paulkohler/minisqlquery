#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.IO;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The generate select statement command.</summary>
	public class GenerateSelectStatementCommand : GenerateStatementCommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="GenerateSelectStatementCommand"/> class.</summary>
		public GenerateSelectStatementCommand()
			: base("Generate Select Statement")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IQueryEditor editor = ActiveFormAsSqlQueryEditor;
			string tableName = HostWindow.DatabaseInspector.RightClickedTableName;
			DbModelInstance model = HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringWriter sql = new StringWriter();
				SqlWriter.WriteSelect(sql, GetTableOrViewByName(model, tableName));
				editor.InsertText(sql.ToString());
			}
		}
	}
}