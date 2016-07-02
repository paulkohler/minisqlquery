#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model table.</summary>
	[DebuggerDisplay("DbModelTable: {FullName} (Columns: {Columns.Count}, PKs: {PrimaryKeyColumns.Count}, FKs: {ForeignKeyColumns.Count})")]
	public class DbModelTable : DbModelObjectBase
	{
		/// <summary>Initializes a new instance of the <see cref="DbModelTable"/> class.</summary>
		public DbModelTable()
		{
			Columns = new List<DbModelColumn>();
			Constraints = new List<DbModelConstraint>();
			ObjectType = ObjectTypes.Table;
		}

		/// <summary>Gets the Columns for this table.</summary>
		/// <value>The columns.</value>
		public virtual List<DbModelColumn> Columns { get; internal set; }

		/// <summary>Gets Constraints.</summary>
		/// <value>The constraints.</value>
		public virtual List<DbModelConstraint> Constraints { get; private set; }

		/// <summary>Gets ForeignKeyColumns.</summary>
		/// <value>The foreign key columns.</value>
		public virtual List<DbModelColumn> ForeignKeyColumns
		{
			get { return Columns.FindAll(c => c.ForeignKeyReference != null); }
		}

		/// <summary>Gets NonKeyColumns.</summary>
		/// <value>The non key columns.</value>
		public virtual List<DbModelColumn> NonKeyColumns
		{
			get { return Columns.FindAll(c => !c.IsKey && c.ForeignKeyReference == null); }
		}

		/// <summary>Gets a reference to the parent database instance.</summary>
		/// <value>The parent model instance object.</value>
		public virtual DbModelInstance ParentDb { get; internal set; }

		/// <summary>Gets PrimaryKeyColumns.</summary>
		/// <value>The primary key columns.</value>
		public virtual List<DbModelColumn> PrimaryKeyColumns
		{
			get { return Columns.FindAll(c => c.IsKey); }
		}

		/// <summary>Adds the <paramref name="column"/> to the list of <see cref="Columns"/> assigning the <see cref="DbModelColumn.ParentTable"/>.</summary>
		/// <param name="column">The new column.</param>
		public virtual void Add(DbModelColumn column)
		{
			column.ParentTable = this;
			Columns.Add(column);
		}
	}
}