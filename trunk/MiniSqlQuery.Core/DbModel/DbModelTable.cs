using System;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("DbModelTable: {FullName} (Columns: {Columns.Count})")]
	public class DbModelTable : DbModelObjectBase
	{
		public DbModelTable()
		{
			Columns = new DbModelColumnCollection();
			PrimaryKeyColumns = new DbModelColumnCollection();
			NonKeyColumns = new DbModelColumnCollection();
			ObjectType = ObjectTypes.Table;
		}

		public virtual DbModelInstance ParentDb { get; internal set; }
		public virtual DbModelColumnCollection Columns { get; internal set; }
		public virtual DbModelColumnCollection PrimaryKeyColumns { get; private set; }
		public virtual DbModelColumnCollection NonKeyColumns { get; private set; }

		public virtual void Add(DbModelColumn column)
		{
			column.ParentTable = this;
			Columns.Add(column);

			if (column.IsKey)
			{
				PrimaryKeyColumns.Add(column);
			}
			else // todo and not FK...
			{
				NonKeyColumns.Add(column);
			}
		}
	}
}