using System;
using System.Data;
using System.Text;
using MiniSqlQuery.Core;

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
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;
			string tableName = Services.HostWindow.DatabaseInspector.RightClickedTableName;
			DataTable schema = Services.HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringBuilder sb = new StringBuilder();
				DataView columnsDv = GetColumnInfoForTable(schema, tableName);
				foreach (DataRowView rowView in columnsDv)
				{
					sb.Append(rowView["Column"]);
					sb.Append(", ");
				}
				string columns = TrimTrailingComma(sb.ToString());

				sb = new StringBuilder();
				foreach (DataRowView rowView in columnsDv)
				{
					sb.AppendFormat("<{0},{1}({2})>", rowView["Column"], rowView["DataType"], rowView["Length"]);
					sb.Append(", ");
				}
				string values = TrimTrailingComma(sb.ToString());

				string text = string.Format(
					"INSERT INTO {0}\r\n\t({1})\r\nVALUES\r\n\t({2})",
					tableName,
					columns,
					values);

				editor.InsertText(text);
			}
		}
	}
}