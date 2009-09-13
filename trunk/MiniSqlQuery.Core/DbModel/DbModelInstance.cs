using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelInstance
	{
		private List<DbModelTable> _tables;
		private List<DbModelTable> _views;

		public DbModelInstance()
		{
			_tables = new List<DbModelTable>();
			_views = new List<DbModelTable>();
		}

		public virtual ICollection Tables
		{
			get { return _tables; }
		}

		public virtual ICollection Views
		{
			get { return _views; }
		}

		public string ProviderName { get; set; }

		public string ConnectionString { get; set; }

		public Dictionary<string, DbModelType> Types { get; set; }

		public virtual void Add(DbModelTable table)
		{
			table.ParentDb = this;
			_tables.Add(table);
		}

		public virtual void Add(DbModelView view)
		{
			view.ParentDb = this;
			_views.Add(view);
		}

		public virtual DbModelTable FindTable(string tableName)
		{
			return _tables.Find(table => table.FullName.Equals(tableName, StringComparison.InvariantCultureIgnoreCase));
		}

		public virtual DbModelTable FindView(string viewName)
		{
			return _views.Find(view => view.FullName.Equals(viewName, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}