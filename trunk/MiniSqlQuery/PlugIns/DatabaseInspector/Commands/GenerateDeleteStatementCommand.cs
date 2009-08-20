using System;
using System.Data;
using System.IO;
using System.Text;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class GenerateDeleteStatementCommand : GenerateStatementCommandBase
	{
		public GenerateDeleteStatementCommand()
			: base("Generate Delete Statement")
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
				SqlWriter.WriteDelete(sql, GetTableByName(model, tableName));
				editor.InsertText(sql.ToString());
			}
		}
	}
}