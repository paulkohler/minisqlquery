using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	[DebuggerDisplay("DbModelTable: {FullName} (Columns: {Columns.Count}, PKs: {PrimaryKeyColumns.Count}, FKs: {ForeignKeyColumns.Count})")]
	public class DbModelTable : DbModelObjectBase
	{
		public DbModelTable()
		{
			Columns = new List<DbModelColumn>();
			Constraints = new List<DbModelConstraint>();
			ObjectType = ObjectTypes.Table;
		}

		public virtual DbModelInstance ParentDb { get; internal set; }
		public virtual List<DbModelColumn> Columns { get; internal set; }

		public virtual List<DbModelColumn> PrimaryKeyColumns
		{
			get { return Columns.FindAll(c => c.IsKey); }
		}

		public virtual List<DbModelColumn> ForeignKeyColumns
		{
			get { return Columns.FindAll(c => c.ForeignKeyReference != null); }
		}

		public virtual List<DbModelColumn> NonKeyColumns
		{
			get { return Columns.FindAll(c => !c.IsKey && c.ForeignKeyReference == null); }
		}

		public virtual List<DbModelConstraint> Constraints { get; private set; }

		/// <summary>
		/// Adds the <paramref name="column"/> to the list of <see cref="Columns"/> assigning the 
		/// <see cref="DbModelColumn.ParentTable"/>.
		/// </summary>
		/// <param name="column">The new column.</param>
		public virtual void Add(DbModelColumn column)
		{
			column.ParentTable = this;
			Columns.Add(column);
		}
	}
}