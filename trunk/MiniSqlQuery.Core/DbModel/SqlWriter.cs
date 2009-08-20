using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core.DbModel
{
	public class SqlWriter : ISqlWriter
	{
		public SqlWriter()
		{
			// todo - format options?
		}

		public virtual void WriteCreate(TextWriter writer, DbModelColumn column)
		{
			writer.Write("{0} {1} ", column.Name, column.DbType.Summary);

			if (!column.Nullable)
			{
				writer.Write("not ");
			}
			writer.Write("null");
		}

		public void WriteSummary(TextWriter writer, DbModelColumn column)
		{
			writer.Write("{0} ({1} ", column.Name, column.DbType.Summary);

			if (!column.Nullable)
			{
				writer.Write("not ");
			}
			writer.Write("null)");
		}

		public virtual void WriteSelect(TextWriter writer, DbModelTable tableOrView)
		{
			writer.Write("SELECT");
			writer.WriteLine();
			for (int i = 0; i < tableOrView.Columns.Count; i++)
			{
				writer.Write("\t");
				writer.Write(tableOrView.Columns[i].Name);
				if (i < tableOrView.Columns.Count - 1)
				{
					writer.Write(",");
					writer.WriteLine();
				}
			}

			writer.WriteLine();
			writer.Write("FROM {0}", MakeSqlFriendly(tableOrView.FullName));
			writer.WriteLine();
		}

		public virtual void WriteInsert(TextWriter writer, DbModelTable tableOrView)
		{
			writer.Write("INSERT INTO ");
			writer.WriteLine(MakeSqlFriendly(tableOrView.FullName));
			writer.Write("\t(");

			// get all columns that are "writable" including PKs that are not auto generated
			var writableColumns = tableOrView.Columns.FindAll(c => c.IsWritable);

			for (int i = 0; i < writableColumns.Count; i++)
			{
				var column = writableColumns[i];
				writer.Write(MakeSqlFriendly(column.Name));
				if (i < writableColumns.Count - 1)
				{
					writer.WriteLine(",");
					writer.Write("\t");
				}
			}

			writer.WriteLine(")");
			writer.WriteLine("VALUES");
			writer.Write("\t(");

			for (int i = 0; i < writableColumns.Count; i++)
			{
				var column = writableColumns[i];
				writer.Write("{0} /*{1}*/", column.DbType.ToDDLValue(column.Nullable), column.DbType.Summary);
				if (i < writableColumns.Count - 1)
				{
					writer.WriteLine(",");
					writer.Write("\t");
				}
			}
			writer.WriteLine(")");
		}

		public virtual void WriteUpdate(TextWriter writer, DbModelTable tableOrView)
		{
			writer.Write("UPDATE ");
			writer.WriteLine(MakeSqlFriendly(tableOrView.FullName));
			writer.WriteLine("SET");

			// get all columns that are "writable" excluding keys that are not auto generated
			var writableColumns = tableOrView.Columns.FindAll(c => c.IsWritable && !c.IsKey);
			for (int i = 0; i < writableColumns.Count; i++)
			{
				var column = writableColumns[i];
				writer.Write("\t{0} = {1}", column.Name, column.DbType.ToDDLValue(column.Nullable));
				if (i < writableColumns.Count - 1)
				{
					writer.Write(",");
					writer.WriteLine();
				}
			}

			writer.WriteLine();
			writer.WriteLine("WHERE");

			for (int i = 0; i < tableOrView.PrimaryKeyColumns.Count; i++)
			{
				var column = tableOrView.PrimaryKeyColumns[i];
				writer.Write("\t{0} = ", column.Name);
				if (i < tableOrView.PrimaryKeyColumns.Count - 1)
				{
					writer.Write(" /*value:{0},{1}*/ AND", column.Name, column.DbType.Summary);
					writer.WriteLine();
				}
				else
				{
					writer.Write("/*value:{0},{1}*/", column.Name, column.DbType.Summary);
				}
			}
			writer.WriteLine();
		}

		public virtual void WriteDelete(TextWriter writer, DbModelTable tableOrView)
		{
			writer.WriteLine("DELETE FROM");
			writer.Write("\t");
			writer.WriteLine(MakeSqlFriendly(tableOrView.FullName));
			writer.WriteLine("WHERE");

			for (int i = 0; i < tableOrView.PrimaryKeyColumns.Count; i++)
			{
				var column = tableOrView.PrimaryKeyColumns[i];
				writer.Write("\t{0} = ", column.Name);
				if (i < tableOrView.PrimaryKeyColumns.Count - 1)
				{
					writer.Write(" /*value:{0}*/ AND", column.Name);
					writer.WriteLine();
				}
				else
				{
					writer.Write("/*value:{0}*/", column.Name);
				}
			}

			writer.WriteLine();
		}

		protected string MakeSqlFriendly(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			if (name.Contains(" ") || name.Contains("$"))
			{
				// TODO - reserved wods?
				return string.Concat("[", name, "]");
			}
			return name;
		}
	}
}