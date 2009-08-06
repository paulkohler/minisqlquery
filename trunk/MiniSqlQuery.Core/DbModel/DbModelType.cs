using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("{GetType()} {Name} [{Length},{Precision},{Scale} : {CreateFormat}, {CreateParameters}]")]
	public class DbModelType : DbModelObjectBase
	{
		public DbModelType(string name, int length)
		{
			Name = name;
			Length = length;
		}

		public virtual int Length { get; set; }
		public virtual int Precision { get; set; }
		public virtual int Scale { get; set; }
		public virtual object Value { get; set; }
		public virtual Type SystemType { get; set; }
		public virtual string CreateFormat { get; set; }
		public virtual string CreateParameters { get; set; }
		public virtual string LiteralPrefix { get; set; }
		public virtual string LiteralSuffix { get; set; }

		public virtual string Summary
		{
			get
			{
				if (!string.IsNullOrEmpty(CreateFormat))
				{
					if (CreateFormat.Contains("{1}"))
					{
						return string.Format(CreateFormat, Precision, Scale);
					}
					if (CreateFormat.Contains("{0}"))
					{
						return string.Format(CreateFormat, Length);
					}
					return CreateFormat;
				}
				return Name;
			}
		}

		public DbModelType Copy()
		{
			DbModelType copy = new DbModelType(Name, Length)
			              {
			              	CreateFormat = CreateFormat,
			              	CreateParameters = CreateParameters,
			              	LiteralPrefix = LiteralPrefix,
			              	LiteralSuffix = LiteralSuffix,
			              	Precision = Precision,
			              	Scale = Scale,
			              	SystemType = SystemType,
			              };
			return copy;
		}

		public static DbModelType Create(string name, int length, int precision, int scale, string systemTypeName)
		{
			return Create(null, name, length, precision, scale, systemTypeName);
		}

		public static DbModelType Create(Dictionary<string, DbModelType> dbTypes, string name, int length, int precision, int scale, string systemTypeName)
		{
			// todo - flesh out with type imps if required
			DbModelType baseType = null;

			if (dbTypes != null && dbTypes.ContainsKey(name))
			{
				// the type should be here, this is used as a baseline for new instances
				baseType = dbTypes[name].Copy();
				baseType.Length = length;
			}
			else
			{
				baseType = new DbModelType(name, length);
				baseType.SystemType = Type.GetType(systemTypeName);
			}

			baseType.Precision = precision;
			baseType.Scale = scale;

			return baseType;
		}
	}
}