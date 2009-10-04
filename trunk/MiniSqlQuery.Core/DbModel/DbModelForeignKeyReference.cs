#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelForeignKeyReference
	{
		public DbModelForeignKeyReference()
		{
		}

		public DbModelForeignKeyReference(DbModelColumn owningColumn, DbModelTable fkTable, DbModelColumn fkColumn)
		{
			OwningColumn = owningColumn;
			ReferenceTable = fkTable;
			ReferenceColumn = fkColumn;
		}

		public virtual string ConstraintName { get; set; }
		public DbModelColumn OwningColumn { get; set; }
		public DbModelTable ReferenceTable { get; set; }
		public DbModelColumn ReferenceColumn { get; set; }
		public string UpdateRule { get; set; }
		public string DeleteRule { get; set; }
	}
}