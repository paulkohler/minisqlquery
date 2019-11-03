#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model constraint.</summary>
	public class DbModelConstraint
	{
		/// <summary>Gets or sets ColumnName.</summary>
		/// <value>The column name.</value>
		public string ColumnName { get; set; }

		/// <summary>Gets or sets ConstraintName.</summary>
		/// <value>The constraint name.</value>
		public string ConstraintName { get; set; }

		/// <summary>Gets or sets ConstraintTableName.</summary>
		/// <value>The constraint table name.</value>
		public string ConstraintTableName { get; set; }

		/// <summary>Gets or sets ConstraintTableSchema.</summary>
		/// <value>The constraint table schema.</value>
		public string ConstraintTableSchema { get; set; }

		/// <summary>Gets or sets DeleteRule.</summary>
		/// <value>The delete rule.</value>
		public string DeleteRule { get; set; }

		/// <summary>Gets or sets UniqueColumnName.</summary>
		/// <value>The unique column name.</value>
		public string UniqueColumnName { get; set; }

		/// <summary>Gets or sets UniqueConstraintName.</summary>
		/// <value>The unique constraint name.</value>
		public string UniqueConstraintName { get; set; }

		/// <summary>Gets or sets UniqueConstraintTableName.</summary>
		/// <value>The unique constraint table name.</value>
		public string UniqueConstraintTableName { get; set; }

		/// <summary>Gets or sets UniqueConstraintTableSchema.</summary>
		/// <value>The unique constraint table schema.</value>
		public string UniqueConstraintTableSchema { get; set; }

		/// <summary>Gets or sets UpdateRule.</summary>
		/// <value>The update rule.</value>
		public string UpdateRule { get; set; }
	}
}