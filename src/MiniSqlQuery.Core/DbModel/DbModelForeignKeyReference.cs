#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model foreign key reference.</summary>
	public class DbModelForeignKeyReference
	{
		/// <summary>Initializes a new instance of the <see cref="DbModelForeignKeyReference"/> class.</summary>
		public DbModelForeignKeyReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="DbModelForeignKeyReference"/> class.</summary>
		/// <param name="owningColumn">The owning column.</param>
		/// <param name="fkTable">The fk table.</param>
		/// <param name="fkColumn">The fk column.</param>
		public DbModelForeignKeyReference(DbModelColumn owningColumn, DbModelTable fkTable, DbModelColumn fkColumn)
		{
			OwningColumn = owningColumn;
			ReferenceTable = fkTable;
			ReferenceColumn = fkColumn;
		}

		/// <summary>Gets or sets ConstraintName.</summary>
		/// <value>The constraint name.</value>
		public virtual string ConstraintName { get; set; }

		/// <summary>Gets or sets DeleteRule.</summary>
		/// <value>The delete rule.</value>
		public string DeleteRule { get; set; }

		/// <summary>Gets or sets OwningColumn.</summary>
		/// <value>The owning column.</value>
		public DbModelColumn OwningColumn { get; set; }

		/// <summary>Gets or sets ReferenceColumn.</summary>
		/// <value>The reference column.</value>
		public DbModelColumn ReferenceColumn { get; set; }

		/// <summary>Gets or sets ReferenceTable.</summary>
		/// <value>The reference table.</value>
		public DbModelTable ReferenceTable { get; set; }

		/// <summary>Gets or sets UpdateRule.</summary>
		/// <value>The update rule.</value>
		public string UpdateRule { get; set; }
	}
}