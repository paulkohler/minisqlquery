using System;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("{GetType()} {Name} (DbModelType.Summary: {DbModelType.Summary}, Nullable: {Nullable}, IsKey: {IsKey})")]
	public class DbModelColumn : DbModelObjectBase
	{
		public DbModelColumn()
		{
			Nullable = true;
			DbType = new DbModelType("varchar", 50);
		}

		public virtual DbModelTable ParentTable { get; internal set; }
		public virtual DbModelType DbType { get; set; }
		public virtual bool Nullable { get; set; }
		public virtual bool IsKey { get; set; }
		public virtual bool IsUnique { get; set; }
		public virtual bool IsRowVersion { get; set; }
	}
}