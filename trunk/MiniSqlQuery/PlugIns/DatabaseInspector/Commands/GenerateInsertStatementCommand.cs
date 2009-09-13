using System;
using System.Data;
using System.IO;
using System.Text;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	internal class GenerateInsertStatementCommand : GenerateStatementCommandBase
	{
		public GenerateInsertStatementCommand()
			: base("Generate Insert Statement")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = ActiveFormAsEditor;
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;
			DbModelInstance model = Services.HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringWriter sql = new StringWriter();
				SqlWriter.WriteInsert(sql, GetTableOrViewByName(model, tableName));
				editor.InsertText(sql.ToString());
			}
		}
	}
}