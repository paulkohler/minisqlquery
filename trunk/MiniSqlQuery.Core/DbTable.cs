using System;

namespace MiniSqlQuery.Core
{
	public class DbTable : INamedObject
	{
		public DbTable()
		{
			Columns = new DbColumnCollection();
		}

		public virtual DbObjectModel ParentDb { get; internal set; }
		public string Schema { get; set; }
		public virtual string Name { get; set; }
		public virtual DbColumnCollection Columns { get; internal set; }

		public virtual void Add(DbColumn column)
		{
			column.ParentTable = this;
			Columns.Add(column);
		}

		public override string ToString()
		{
			return string.Format("DbTable - Name: {0}.{1}", Schema, Name);
		}
	}
}