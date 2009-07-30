using System;

namespace MiniSqlQuery.Core
{
	public class DbObjectModel
	{
		public DbObjectModel()
		{
			Tables = new DbTableCollection();
		}

		//conn info etc

		public virtual DbTableCollection Tables { get; internal set; }

		public virtual void Add(DbTable table)
		{
			table.ParentDb = this;
			Tables.Add(table);
		}
	}
}