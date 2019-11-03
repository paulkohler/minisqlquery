#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
    /// <summary>Examins a <see cref="DbModelInstance"/> providing sort methods.</summary>
    public class DbModelDependencyWalker
    {
        /// <summary>The _model.</summary>
        private readonly DbModelInstance _model;

        /// <summary>Initializes a new instance of the <see cref="DbModelDependencyWalker"/> class.</summary>
        /// <param name="model">The database model instance.</param>
        public DbModelDependencyWalker(DbModelInstance model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            _model = model;
        }

        /// <summary>Sorts the tables by checking the foreign key references recursivly building a list of tables in order.</summary>
        /// <returns>An array of tables in dependency order.</returns>
        public DbModelTable[] SortTablesByForeignKeyReferences()
        {
            List<DbModelTable> tables = new List<DbModelTable>();

            // add tables with no FKs
            foreach (DbModelTable table in _model.Tables)
            {
                if (table.ForeignKeyColumns.Count == 0)
                {
                    tables.Add(table);
                }
            }

            foreach (DbModelTable table in _model.Tables)
            {
                ProcessForeignKeyReferences(1, tables, table);
            }

            return tables.ToArray();
        }

        /// <summary>The process foreign key references.</summary>
        /// <param name="level">The level.</param>
        /// <param name="tablesList">The tables list.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void ProcessForeignKeyReferences(int level, List<DbModelTable> tablesList, DbModelTable table)
        {
            if (tablesList.Contains(table))
            {
                return;
            }

            // recursive insurance ;-)
            level++;
            if (level > 1000)
            {
                throw new InvalidOperationException(string.Format("FK processor exceeded recursive level of 1000 at '{0}'.", table.Name));
            }

            // if there are FK refs, add the refered tables first
            if (table.ForeignKeyColumns.Count > 0)
            {
                // if the table is not already in the list....
                foreach (DbModelColumn fkColumn in table.ForeignKeyColumns)
                {
                    // ensure its not a self referencing table
                    if (fkColumn.ForeignKeyReference.ReferenceTable != table)
                    {
                        ProcessForeignKeyReferences(++level, tablesList, fkColumn.ForeignKeyReference.ReferenceTable);
                    }
                }

                // now add the table if not in the list yet
                if (!tablesList.Contains(table))
                {
                    tablesList.Add(table);
                }
            }
        }
    }
}