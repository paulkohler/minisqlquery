using System;
using System.Data;
using System.Text;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	internal class GenerateUpdateStatementCommand : GenerateStatementCommandBase
	{
		public GenerateUpdateStatementCommand()
			: base("Generate Update Statement")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;
			DataTable schema = Services.HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				string pkClause = "(idToDo = ?)";
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("UPDATE {0}\r\nSET\r\n", tableName);
				DataView columnsDv = GetColumnInfoForTable(schema, tableName);
				foreach (DataRowView rowView in columnsDv)
				{
					sb.Append("\t");
					sb.Append(rowView["Column"]);
					sb.Append(",\r\n");
				}
				sb.Remove(sb.Length - 3, 3); // remove ",\r\n"
				sb.AppendFormat("\r\nWHERE ", tableName);
				sb.Append(pkClause);

				editor.InsertText(sb.ToString());
			}
		}
	}
}