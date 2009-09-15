using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>
	/// Describes a database type.
	/// </summary>
	[DebuggerDisplay("DbModelType: {Name} [{Summary,nq}]")]
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
		public virtual string ProviderDbType { get; set; }

		/// <summary>
		/// Gets the summary of the SQL type using the <see cref="CreateFormat"/> if applicable.
		/// </summary>
		/// <value>The summary.</value>
		public virtual string Summary
		{
			get
			{
				if (!string.IsNullOrEmpty(CreateFormat))
				{
					if (CreateFormat.Contains("{1}") && (Precision != -1 && Scale != -1))
					{
						return string.Format(CreateFormat, Precision, Scale);
					}
					if (CreateFormat.Contains("{0}") && !CreateFormat.Contains("{1}") && (Length != -1))
					{
						return string.Format(CreateFormat, Length);
					}
					if (CreateFormat.Contains("{0}"))
					{
						// err...
						return Name;
					}
					return CreateFormat;
				}
				return Name;
			}
		}

		/// <summary>
		/// Copies this instance.
		/// </summary>
		/// <returns>A copy of this instance.</returns>
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

		//public static DbModelType Create(string name, int length, int precision, int scale, string systemTypeName)
		//{
		//    return Create(null, name, length, precision, scale, systemTypeName);
		//}

		/// <summary>
		/// Creates an instance of <see cref="DbModelType"/> defined by the parameers.
		/// </summary>
		/// <param name="dbTypes">The db types list, if the <paramref name="name"/> is in the list it is used as a base copy of the type.</param>
		/// <param name="name">The name of the type, e.g. "int", "nvarchar" etc.</param>
		/// <param name="length">The length of the type.</param>
		/// <param name="precision">The precision.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="systemTypeName">Name of the system type, e.g. "System.String".</param>
		/// <returns>An instance of <see cref="DbModelType"/> defined by the parameers</returns>
		public static DbModelType Create(Dictionary<string, DbModelType> dbTypes, string name, int length, int precision, int scale, string systemTypeName)
		{
			DbModelType baseType;

			string key = name.ToLower();
			if (dbTypes != null && dbTypes.ContainsKey(key))
			{
				// the type should be here, this is used as a baseline for new instances
				baseType = dbTypes[key].Copy();
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

		/// <summary>
		/// Renders this <see cref="DbModelType"/> as basic DDL base on the contents of <see cref="Value"/> (assumes nullable).
		/// </summary>
		/// <returns>An SQL string representing the <see cref="Value"/> acording to the database type (assumes nullable).</returns>
		public string ToDDLValue()
		{
			return ToDDLValue(true);
		}

		/// <summary>
		/// Renders this <see cref="DbModelType"/> as basic DDL base on the contents of <see cref="Value"/>.
		/// </summary>
		/// <param name="nullable">A boolean value indicating the nullability of the column.</param>
		/// <returns>
		/// An SQL string representing the <see cref="Value"/> acording to the database type.
		/// </returns>
		/// <remarks>
		/// If a column is "not null" and the <see cref="Value"/> is null, in the furure this method will attampt to return a default value
		/// rather than throwing an exception etc.
		/// </remarks>
		public string ToDDLValue(bool nullable)
		{
			if (nullable && (Value == null || Value == DBNull.Value))
			{
				return "null";
			}
			if (!nullable && (Value == null || Value == DBNull.Value))
			{
				// not supposed to be nullable but the Value is. render a default
				switch (SystemType.Name)
				{
					case"String":
						return string.Concat(LiteralPrefix, LiteralSuffix);
					case"DateTime":
						return "'?'";
					case"Guid":
						return string.Concat("'", Guid.Empty, "'");
					default:
						return "0";
				}
			}
			return string.Concat(LiteralPrefix, Value, LiteralSuffix);
		}

	}
}