#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelInstance
	{
		private readonly List<DbModelTable> _tables;
		private readonly List<DbModelView> _views;

		public DbModelInstance()
		{
			_tables = new List<DbModelTable>();
			_views = new List<DbModelView>();
		}

		public virtual ICollection<DbModelTable> Tables
		{
			get { return _tables; }
		}

		public virtual ICollection<DbModelView> Views
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

		public virtual DbModelTable FindTableOrView(string name)
		{
			var obj = _tables.Find(table => table.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			if (obj == null)
			{
				obj = _views.Find(view => view.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			}
			return obj;
		}

	}
}