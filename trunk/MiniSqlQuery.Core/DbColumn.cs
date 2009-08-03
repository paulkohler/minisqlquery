using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	public class DbColumnCollection : List<DbColumn>
	{
	}

	public class DbColumn:INamedObject
	{
		public DbColumn()
		{
			Nullable = true;

			// todo - inner class at core level
			Type = new DbType("varchar", 50);
		}

		public DbTable ParentTable { get; internal set; }
		public string Name { get; set; }
		public DbType Type { get; set; }
		public bool Nullable { get; set; }
		public Type SystemType { get; set; }
		public bool IsKey { get; set; }
		public bool IsUnique { get; set; }
		public bool IsRowVersion { get; set; }

		public override string ToString()
		{
			return string.Format("DbColumn - Name: '{0}', Type: '{1}', Nullable: {2}, {3} ({4})", 
				Name, Type.Summary, Nullable, IsRowVersion, SystemType.FullName);
		}
	}
}