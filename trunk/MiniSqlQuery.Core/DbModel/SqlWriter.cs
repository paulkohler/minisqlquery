#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core.DbModel
{
	public class SqlWriter : ISqlWriter
	{
		public bool IncludeComments { get; set; }
		public bool InsertLineBreaksBetweenColumns { get; set; }

		public SqlWriter()
		{
			// todo - format options?
			IncludeComments = true;
			InsertLineBreaksBetweenColumns = true;
		}

		public virtual void WriteCreate(TextWriter writer, DbModelColumn column)
		{
			writer.Write("{0} {1} ", MakeSqlFriendly(column.Name), column.DbType.Summary);

			if (!column.Nullable)
			{
				writer.Write("not ");
			}
			writer.Write("null");
		}

		public void WriteSummary(TextWriter writer, DbModelColumn column)
		{
			writer.Write("{0} ({1} ", MakeSqlFriendly(column.Name), column.DbType.Summary);

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
				writer.Write(MakeSqlFriendly(tableOrView.Columns[i].Name));
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

		public virtual void WriteSelectCount(TextWriter writer, DbModelTable tableOrView)
		{
			writer.Write("SELECT COUNT(*) FROM {0}", MakeSqlFriendly(tableOrView.FullName));
			writer.WriteLine();
		}

		public virtual void WriteInsert(TextWriter writer, DbModelTable tableOrView)
		{
			writer.Write("INSERT INTO ");
			writer.Write(MakeSqlFriendly(tableOrView.FullName));
			if (InsertLineBreaksBetweenColumns)
			{
				writer.WriteLine();
				writer.Write("\t");
			}
			writer.Write("(");

			// get all columns that are "writable" including PKs that are not auto generated
			var writableColumns = tableOrView.Columns.FindAll(c => c.IsWritable);

			for (int i = 0; i < writableColumns.Count; i++)
			{
				var column = writableColumns[i];
				writer.Write(MakeSqlFriendly(column.Name));
				if (i < writableColumns.Count - 1)
				{
					if (InsertLineBreaksBetweenColumns)
					{
						writer.WriteLine(",");
						writer.Write("\t");
					}
					else
					{
						writer.Write(", ");
					}
				}
			}

			writer.WriteLine(")");
			writer.Write("VALUES");
			if (InsertLineBreaksBetweenColumns)
			{
				writer.WriteLine();
				writer.Write("\t");
			}
			writer.Write("(");

			for (int i = 0; i < writableColumns.Count; i++)
			{
				var column = writableColumns[i];
				writer.Write(column.DbType.ToDDLValue(column.Nullable));
				if (IncludeComments)
				{
					writer.Write(" /*{0},{1}*/", column.Name, column.DbType.Summary);
				}
				if (i < writableColumns.Count - 1)
				{
					if (InsertLineBreaksBetweenColumns)
					{
						writer.WriteLine(",");
						writer.Write("\t");
					}
					else
					{
						writer.Write(", ");
					}
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
				writer.Write("\t{0} = {1}", MakeSqlFriendly(column.Name), column.DbType.ToDDLValue(column.Nullable));
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
				writer.Write("\t{0} = ", MakeSqlFriendly(column.Name));
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
				writer.Write("\t{0} = ", MakeSqlFriendly(column.Name));
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
			return Utility.MakeSqlFriendly(name);
		}
	}
}