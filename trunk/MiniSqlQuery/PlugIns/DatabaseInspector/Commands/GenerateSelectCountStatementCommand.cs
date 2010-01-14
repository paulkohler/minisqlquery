using System;
using System.IO;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class GenerateSelectCountStatementCommand : GenerateStatementCommandBase
	{
		public GenerateSelectCountStatementCommand()
			: base("Generate Select COUNT(*) Statement")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = ActiveFormAsSqlQueryEditor;
			string tableName = HostWindow.DatabaseInspector.RightClickedTableName;
			DbModelInstance model = HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringWriter sql = new StringWriter();
				SqlWriter.WriteSelectCount(sql, GetTableOrViewByName(model, tableName));
				editor.InsertText(sql.ToString());
			}
		}
	}
}