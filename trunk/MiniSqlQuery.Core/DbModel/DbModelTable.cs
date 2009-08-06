using System;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("{GetType()} {Schema}.{Name} (Columns: {Columns.Count})")]
	public class DbModelTable : DbModelObjectBase
	{
		public DbModelTable()
		{
			Columns = new DbModelColumnCollection();
		}

		public virtual DbModelInstance ParentDb { get; internal set; }
		public virtual string Schema { get; set; }
		public virtual string Name { get; set; }
		public virtual DbModelColumnCollection Columns { get; internal set; }

		public virtual void Add(DbModelColumn column)
		{
			column.ParentTable = this;
			Columns.Add(column);
		}
	}
}