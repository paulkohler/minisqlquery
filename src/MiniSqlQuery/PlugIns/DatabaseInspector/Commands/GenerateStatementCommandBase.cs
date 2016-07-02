#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The generate statement command base.</summary>
	public abstract class GenerateStatementCommandBase : CommandBase
	{
		/// <summary>The _sql writer.</summary>
		private ISqlWriter _sqlWriter;

		/// <summary>Initializes a new instance of the <see cref="GenerateStatementCommandBase"/> class.</summary>
		/// <param name="name">The name.</param>
		public GenerateStatementCommandBase(string name)
			: base(name)
		{
		}

		/// <summary>Gets SqlWriter.</summary>
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

		/// <summary>The get table or view by name.</summary>
		/// <param name="model">The model.</param>
		/// <param name="tableName">The table name.</param>
		/// <returns></returns>
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

		/// <summary>The trim trailing comma.</summary>
		/// <param name="sql">The sql.</param>
		/// <returns>The trim trailing comma.</returns>
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