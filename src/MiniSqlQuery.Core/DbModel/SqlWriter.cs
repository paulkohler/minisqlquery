#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Collections.Generic;
using System.IO;

namespace MiniSqlQuery.Core.DbModel
{
    /// <summary>The sql writer.</summary>
    public class SqlWriter : ISqlWriter
    {
        /// <summary>Initializes a new instance of the <see cref="SqlWriter"/> class.</summary>
        public SqlWriter()
        {
            // todo - format options?
            IncludeComments = true;
            InsertLineBreaksBetweenColumns = true;
        }

        /// <summary>Gets or sets a value indicating whether IncludeComments.</summary>
        /// <value>The include comments.</value>
        public bool IncludeComments { get; set; }

        /// <summary>Gets or sets a value indicating whether InsertLineBreaksBetweenColumns.</summary>
        /// <value>The insert line breaks between columns.</value>
        public bool InsertLineBreaksBetweenColumns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include read-only columns in the export SQL.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if including read-only columns in the export; otherwise, <c>false</c>.
        /// </value>
		public bool IncludeReadOnlyColumnsInExport { get; set; }

        /// <summary>The write create.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="column">The column.</param>
        public virtual void WriteCreate(TextWriter writer, DbModelColumn column)
        {
            writer.Write("{0} {1} ", MakeSqlFriendly(column.Name), column.DbType.Summary);

            if (!column.Nullable)
            {
                writer.Write("not ");
            }

            writer.Write("null");
        }

        /// <summary>The write delete.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableOrView">The table or view.</param>
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

        /// <summary>The write insert.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableOrView">The table or view.</param>
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

            // get all columns that are "writable" including PKs that are not auto generated (unless specified)
            List<DbModelColumn> writableColumns = null;
            if (IncludeReadOnlyColumnsInExport)
            {
                writableColumns = tableOrView.Columns;
            }
            else
            {
                writableColumns = tableOrView.Columns.FindAll(c => c.IsWritable);
            }

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

        /// <summary>The write select.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableOrView">The table or view.</param>
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

        /// <summary>The write select count.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableOrView">The table or view.</param>
        public virtual void WriteSelectCount(TextWriter writer, DbModelTable tableOrView)
        {
            writer.Write("SELECT COUNT(*) FROM {0}", MakeSqlFriendly(tableOrView.FullName));
            writer.WriteLine();
        }

        /// <summary>The write summary.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="column">The column.</param>
        public void WriteSummary(TextWriter writer, DbModelColumn column)
        {
            writer.Write("{0} ({1} ", MakeSqlFriendly(column.Name), column.DbType.Summary);

            if (!column.Nullable)
            {
                writer.Write("not ");
            }

            writer.Write("null)");
        }

        /// <summary>The write update.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableOrView">The table or view.</param>
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

        /// <summary>The make the sql friendly, e.g. "[TableName]".</summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>The make sql friendly name.</returns>
        protected string MakeSqlFriendly(string name)
        {
            return Utility.MakeSqlFriendly(name);
        }
    }
}