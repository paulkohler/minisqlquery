using System;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("DbModelColumn: {Name} (DbModelType.Summary: {DbModelType.Summary}, Nullable: {Nullable}, IsKey: {IsKey})")]
	public class DbModelColumn : DbModelObjectBase
	{
		public DbModelColumn()
		{
			Nullable = true;
			DbType = new DbModelType("varchar", 50);
			ObjectType = ObjectTypes.Column;
		}

		public override string FullName
		{
			get
			{
				return Name;
			}
		}
		
		public virtual DbModelTable ParentTable { get; internal set; }
		public virtual DbModelType DbType { get; set; }
		public virtual bool Nullable { get; set; }
		public virtual bool IsKey { get; set; }
		public virtual bool IsUnique { get; set; }

		/// <summary>
		/// Gets or sets a value a concurrency field, such as a timestamp.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is row version, or concurrency field; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRowVersion { get; set; }
		public virtual bool IsIdentity { get; set; }
		public virtual bool IsAutoIncrement { get; set; }
		public virtual bool IsReadOnly { get; set; }
		public virtual bool IsWritable { get { return !IsReadOnly; } }
		public virtual DbModelForiegnKeyReference ForiegnKeyReference { get; set; }
	}
}