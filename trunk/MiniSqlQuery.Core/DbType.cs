using System;

namespace MiniSqlQuery.Core
{
	public class DbType
	{
		public DbType(string name, int length)
		{
			Name = name;
			Length = length;
		}

		public virtual string Name { get; set; }
		public virtual int Length { get; set; }
		public virtual object Value { get; set; }

		public virtual string Summary
		{
			get { return string.Format("{0}({1})", Name, Length); }
		}

		public static DbType Create(string name, int length)
		{ 
			// todo - flesh out with type imps
			return new DbType(name, length);
		}
	}
}