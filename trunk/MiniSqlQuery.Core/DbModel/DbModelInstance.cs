using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelInstance
	{
		public DbModelInstance()
		{
			Tables = new DbModelTableCollection();
			Views = new DbModelTableCollection();
		}

		//conn info etc

		public virtual DbModelTableCollection Tables { get; internal set; }

		public virtual DbModelTableCollection Views { get; internal set; }

		public string ProviderName { get; set; }

		public string ConnectionString { get; set; }

		public Dictionary<string, DbModelType> Types { get; set; }

		public virtual void Add(DbModelTable table)
		{
			table.ParentDb = this;
			Tables.Add(table);
		}

		public virtual void Add(DbModelView view)
		{
			view.ParentDb = this;
			Views.Add(view);
		}
	}
}