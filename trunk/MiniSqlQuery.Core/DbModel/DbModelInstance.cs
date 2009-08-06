using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelInstance
	{
		public DbModelInstance()
		{
			Tables = new DbModelTableCollection();
		}

		//conn info etc

		public virtual DbModelTableCollection Tables { get; internal set; }

		public virtual void Add(DbModelTable table)
		{
			table.ParentDb = this;
			Tables.Add(table);
		}
	}
}