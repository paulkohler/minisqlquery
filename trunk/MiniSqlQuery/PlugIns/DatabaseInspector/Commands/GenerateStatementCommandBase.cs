using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public abstract class GenerateStatementCommandBase : CommandBase
	{
		private ISqlWriter _sqlWriter;

		public GenerateStatementCommandBase(string name)
			: base(name)
		{
		}

		protected ISqlWriter SqlWriter
		{
			get
			{
				if (_sqlWriter == null)
				{
					_sqlWriter = Services.Resolve<ISqlWriter>();
				}
				return _sqlWriter;
			}
		}

		protected DbModelTable GetTableOrViewByName(DbModelInstance model, string tableName)
		{
			DbModelTable tableOrView = model.FindTable(tableName);
			if (tableOrView == null)
			{
				// check the views
				tableOrView = model.FindView(tableName);
			}
			return tableOrView;
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