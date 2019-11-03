#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
    /// <summary>Describes a database type.</summary>
    [DebuggerDisplay("DbModelType: {Name} [{Summary,nq}]")]
    public class DbModelType : DbModelObjectBase
    {
        /// <summary>Initializes a new instance of the <see cref="DbModelType"/> class.</summary>
        /// <param name="name">The name of the type, e.g. "varchar".</param>
        /// <param name="length">The length of the type, e.g. 10.</param>
        public DbModelType(string name, int length)
        {
            Name = name;
            Length = length;
        }

        /// <summary>Gets or sets CreateFormat.</summary>
        /// <value>The create format.</value>
        public virtual string CreateFormat { get; set; }

        /// <summary>Gets or sets CreateParameters.</summary>
        /// <value>The create parameters.</value>
        public virtual string CreateParameters { get; set; }

        /// <summary>Gets or sets Length.</summary>
        /// <value>The length.</value>
        public virtual int Length { get; set; }

        /// <summary>Gets or sets LiteralPrefix.</summary>
        /// <value>The literal prefix.</value>
        public virtual string LiteralPrefix { get; set; }

        /// <summary>Gets or sets LiteralSuffix.</summary>
        /// <value>The literal suffix.</value>
        public virtual string LiteralSuffix { get; set; }

        /// <summary>Gets or sets Precision.</summary>
        /// <value>The precision.</value>
        public virtual int Precision { get; set; }

        /// <summary>Gets or sets ProviderDbType.</summary>
        /// <value>The provider db type.</value>
        public virtual string ProviderDbType { get; set; }

        /// <summary>Gets or sets Scale.</summary>
        /// <value>The scale.</value>
        public virtual int Scale { get; set; }

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

        /// <summary>Gets or sets SystemType.</summary>
        /// <value>The system type.</value>
        public virtual Type SystemType { get; set; }

        /// <summary>Gets or sets Value.</summary>
        /// <value>The value.</value>
        public virtual object Value { get; set; }

        // public static DbModelType Create(string name, int length, int precision, int scale, string systemTypeName)
        // {
        // return Create(null, name, length, precision, scale, systemTypeName);
        // }

        /// <summary>Creates an instance of <see cref="DbModelType"/> defined by the parameers.</summary>
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

        /// <summary>Copies this instance.</summary>
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

        /// <summary>Renders this <see cref="DbModelType"/> as basic DDL base on the contents of <see cref="Value"/> (assumes nullable).</summary>
        /// <returns>An SQL string representing the <see cref="Value"/> acording to the database type (assumes nullable).</returns>
        public string ToDDLValue()
        {
            return ToDDLValue(true);
        }

        /// <summary>Renders this <see cref="DbModelType"/> as basic DDL base on the contents of <see cref="Value"/>.</summary>
        /// <param name="nullable">A boolean value indicating the nullability of the column.</param>
        /// <returns>An SQL string representing the <see cref="Value"/> acording to the database type.</returns>
        /// <remarks>If a column is "not null" and the <see cref="Value"/> is null, in the furure this method will attampt to return a default value
        /// rather than throwing an exception etc.</remarks>
        public string ToDDLValue(bool nullable)
        {
            if (Value == null || Value == DBNull.Value)
            {
                if (nullable)
                {
                    return "null";
                }

                // not supposed to be nullable but the Value is. render a default
                switch (SystemType.FullName)
                {
                    case "System.String":
                        return string.Concat(LiteralPrefix, LiteralSuffix);
                    case "System.DateTime":
                        return "'?'";
                    case "System.Guid":
                        return string.Concat("'", Guid.Empty, "'");
                    default:
                        return "0"; // take a punt
                }
            }

            if (SystemType == typeof(string))
            {
                return string.Concat(LiteralPrefix, ((string)Value).Replace("'", "''"), LiteralSuffix);
            }

            if (SystemType == typeof(DateTime))
            {
                return string.Format("{0}{1:yyyy-MM-dd HH:mm:ss}{2}", LiteralPrefix, Value, LiteralSuffix);
            }

            if (SystemType == typeof(bool) && Name.Equals("bit", StringComparison.InvariantCultureIgnoreCase))
            {
                return ((bool)Value) ? "1" : "0";
            }

            if (SystemType == typeof(byte[]))
            {
                return "null /* not supported yet */ ";
            }

            return string.Concat(LiteralPrefix, Value, LiteralSuffix);
        }
    }
}