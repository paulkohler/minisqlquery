using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelConstraint
	{
		public string ConstraintTableName { get; set; }
		public string ConstraintName { get; set; }
		public string ColumnName { get; set; }
		public string UniqueConstraintTableName { get; set; }
		public string UniqueConstraintName { get; set; }
		public string UniqueColumnName { get; set; }
		public string DeleteRule { get; set; }
		public string UpdateRule { get; set; }
	}
}