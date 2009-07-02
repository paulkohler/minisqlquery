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
				sb.AppendFormat("UPDATE {0}{1}SET{1}", tableName, Environment.NewLine);
				DataView columnsDv = GetColumnInfoForTable(schema, tableName);
				foreach (DataRowView rowView in columnsDv)
				{
					sb.AppendFormat("\t{0} = '{0}',", rowView["Column"]);
					sb.AppendLine();
				}
				sb.Remove(sb.Length - 3, 3); // remove ",\r\n"
				sb.AppendLine("WHERE");
				sb.Append(pkClause);

				editor.InsertText(sb.ToString());
			}
		}
	}
}