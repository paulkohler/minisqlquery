using System;
using System.Data;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public abstract class GenerateStatementCommandBase : CommandBase
	{
		public GenerateStatementCommandBase(string name)
			: base(name)
		{
		}

		protected DataView GetColumnInfoForTable(DataTable schema, string tableName)
		{
			if (tableName.IndexOf('.') > -1)
			{
				tableName = tableName.Substring(tableName.IndexOf('.') + 1);
			}
			DataView columnsDv = new DataView(schema, string.Format("Table = '{0}'", tableName), null, DataViewRowState.CurrentRows);
			return columnsDv;
		}

		protected string TrimTrailingComma(string sql)
		{
			if (sql != null && sql.TrimEnd().EndsWith(","))
			{
				string tmp = sql.TrimEnd();
				return tmp.Substring(0, tmp.Length - 1);
			}
			return sql;
		}
	}
}