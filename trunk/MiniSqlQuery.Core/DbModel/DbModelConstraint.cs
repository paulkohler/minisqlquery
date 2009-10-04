#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelConstraint
	{
		public string ConstraintTableSchema { get; set; }
		public string ConstraintTableName { get; set; }
		public string ConstraintName { get; set; }
		public string ColumnName { get; set; }
		public string UniqueConstraintTableSchema { get; set; }
		public string UniqueConstraintTableName { get; set; }
		public string UniqueConstraintName { get; set; }
		public string UniqueColumnName { get; set; }
		public string DeleteRule { get; set; }
		public string UpdateRule { get; set; }
	}
}