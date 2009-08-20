using System;
using System.Data;
using System.Text;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class GenerateSelectStatementCommand : GenerateStatementCommandBase
	{
		public GenerateSelectStatementCommand()
			: base("Generate Select Statement")
		{
		}

		public override void Execute()
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;
			DataTable schema = Services.HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringBuilder sb = new StringBuilder("SELECT\r\n");
				DataView columnsDv = GetColumnInfoForTable(schema, tableName);
				foreach (DataRowView rowView in columnsDv)
				{
					sb.Append("\t");
					sb.Append(rowView["Column"]);
					sb.Append(",\r\n");
				}

				sb.Remove(sb.Length - 3, 3); // remove ",\r\n"
				sb.Append("\r\nFROM ");
				if (tableName.Contains(" "))
				{
					sb.AppendFormat("[{0}]", tableName);
				}
				else
				{
					sb.Append(tableName);
				}

				editor.InsertText(sb.ToString());
			}
		}
	}
}