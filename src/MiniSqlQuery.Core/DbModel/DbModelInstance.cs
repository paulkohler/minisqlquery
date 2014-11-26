#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model instance.</summary>
	public class DbModelInstance
	{
		/// <summary>The _tables.</summary>
		private readonly List<DbModelTable> _tables;

		/// <summary>The _views.</summary>
		private readonly List<DbModelView> _views;

		/// <summary>Initializes a new instance of the <see cref="DbModelInstance"/> class.</summary>
		public DbModelInstance()
		{
			_tables = new List<DbModelTable>();
			_views = new List<DbModelView>();
		}

		/// <summary>Gets or sets ConnectionString.</summary>
		/// <value>The connection string.</value>
		public string ConnectionString { get; set; }

		/// <summary>Gets or sets ProviderName.</summary>
		/// <value>The provider name.</value>
		public string ProviderName { get; set; }

		/// <summary>Gets Tables.</summary>
		/// <value>The tables.</value>
		public virtual ICollection<DbModelTable> Tables
		{
			get { return _tables; }
		}

		/// <summary>Gets or sets Types.</summary>
		/// <value>The types.</value>
		public Dictionary<string, DbModelType> Types { get; set; }

		/// <summary>Gets Views.</summary>
		/// <value>The views.</value>
		public virtual ICollection<DbModelView> Views
		{
			get { return _views; }
		}

		/// <summary>The add.</summary>
		/// <param name="table">The table.</param>
		public virtual void Add(DbModelTable table)
		{
			table.ParentDb = this;
			_tables.Add(table);
		}

		/// <summary>The add.</summary>
		/// <param name="view">The view.</param>
		public virtual void Add(DbModelView view)
		{
			view.ParentDb = this;
			_views.Add(view);
		}

		/// <summary>The find table.</summary>
		/// <param name="tableName">The table name.</param>
		/// <returns></returns>
		public virtual DbModelTable FindTable(string tableName)
		{
			return _tables.Find(table => table.FullName.Equals(tableName, StringComparison.InvariantCultureIgnoreCase));
		}

		/// <summary>The find table or view.</summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public virtual DbModelTable FindTableOrView(string name)
		{
			var obj = _tables.Find(table => table.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			if (obj == null)
			{
				obj = _views.Find(view => view.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			}

			return obj;
		}

		/// <summary>The find view.</summary>
		/// <param name="viewName">The view name.</param>
		/// <returns></returns>
		public virtual DbModelTable FindView(string viewName)
		{
			return _views.Find(view => view.FullName.Equals(viewName, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}