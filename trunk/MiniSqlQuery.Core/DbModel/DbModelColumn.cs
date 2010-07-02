#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model column.</summary>
	[DebuggerDisplay("DbModelColumn: {Name} (DbModelType.Summary: {DbModelType.Summary}, Nullable: {Nullable}, IsKey: {IsKey})")]
	public class DbModelColumn : DbModelObjectBase
	{
		/// <summary>Initializes a new instance of the <see cref="DbModelColumn"/> class.</summary>
		public DbModelColumn()
		{
			Nullable = true;
			DbType = new DbModelType("varchar", 50);
			ObjectType = ObjectTypes.Column;
		}

		/// <summary>Gets or sets DbType.</summary>
		/// <value>The db type.</value>
		public virtual DbModelType DbType { get; set; }

		/// <summary>Gets or sets ForeignKeyReference.</summary>
		/// <value>The foreign key reference.</value>
		public virtual DbModelForeignKeyReference ForeignKeyReference { get; set; }

		/// <summary>Gets FullName.</summary>
		/// <value>The full name.</value>
		public override string FullName
		{
			get { return Name; }
		}

		/// <summary>Gets a value indicating whether HasFK.</summary>
		/// <value>The has fk.</value>
		public virtual bool HasFK
		{
			get { return ForeignKeyReference != null; }
		}

		/// <summary>Gets or sets a value indicating whether IsAutoIncrement.</summary>
		/// <value>The is auto increment.</value>
		public virtual bool IsAutoIncrement { get; set; }

		/// <summary>Gets or sets a value indicating whether IsIdentity.</summary>
		/// <value>The is identity.</value>
		public virtual bool IsIdentity { get; set; }

		/// <summary>Gets or sets a value indicating whether IsKey.</summary>
		/// <value>The is key.</value>
		public virtual bool IsKey { get; set; }

		/// <summary>Gets or sets a value indicating whether IsReadOnly.</summary>
		/// <value>The is read only.</value>
		public virtual bool IsReadOnly { get; set; }

		/// <summary>
		/// Gets or sets a value a concurrency field, such as a timestamp.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is row version, or concurrency field; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRowVersion { get; set; }

		/// <summary>Gets or sets a value indicating whether IsUnique.</summary>
		/// <value>The is unique.</value>
		public virtual bool IsUnique { get; set; }

		/// <summary>Gets a value indicating whether IsWritable.</summary>
		/// <value>The is writable.</value>
		public virtual bool IsWritable
		{
			get { return !IsReadOnly; }
		}

		/// <summary>Gets or sets a value indicating whether Nullable.</summary>
		/// <value>The nullable.</value>
		public virtual bool Nullable { get; set; }

		/// <summary>Gets or sets ParentTable.</summary>
		/// <value>The parent table.</value>
		public virtual DbModelTable ParentTable { get; internal set; }
	}
}