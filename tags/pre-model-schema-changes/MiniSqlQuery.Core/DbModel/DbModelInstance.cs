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

		public string ProviderName { get; set; }

		public string ConnectionString { get; set; }

		public virtual void Add(DbModelTable table)
		{
			table.ParentDb = this;
			Tables.Add(table);
		}
	}
}