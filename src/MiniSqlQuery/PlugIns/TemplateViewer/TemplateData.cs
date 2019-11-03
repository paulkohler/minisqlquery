#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
    /// <summary>The template data.</summary>
    public class TemplateData : IDisposable
    {
        /// <summary>The _data tables.</summary>
        private readonly Dictionary<string, DataTable> _dataTables = new Dictionary<string, DataTable>();

        /// <summary>The _db connection.</summary>
        private DbConnection _dbConnection;

        /// <summary>Initializes a new instance of the <see cref="TemplateData"/> class.</summary>
        /// <param name="services">The services.</param>
        public TemplateData(IApplicationServices services)
        {
            Services = services;
        }

        /// <summary>Gets Services.</summary>
        public IApplicationServices Services { get; private set; }

        /// <summary>Helper for getting the value of a row - avoids "get_Item()" usage.</summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The column value.</returns>
        public object ColumnValue(DataRow row, string columnName)
        {
            return row[columnName];
        }

        /// <summary>The get.</summary>
        /// <param name="schema">The schema name.</param>
        /// <param name="viewOrTableName">The view or table name.</param>
        /// <returns></returns>
        public DataTable Get(string schema, string viewOrTableName)
        {
            return Get(Utility.RenderSafeSchemaObjectName(schema, viewOrTableName));
        }

        /// <summary>The get.</summary>
        /// <param name="viewOrTableName">The view or table name.</param>
        /// <returns></returns>
        public DataTable Get(string viewOrTableName)
        {
            DbDataAdapter adapter = null;
            DbCommand cmd = null;
            DataTable dt = null;
            QueryBatch batch = new QueryBatch();
            Query query = new Query("SELECT * FROM " + viewOrTableName);

            if (string.IsNullOrEmpty(viewOrTableName))
            {
                return null;
            }

            if (_dataTables.ContainsKey(viewOrTableName))
            {
                return _dataTables[viewOrTableName];
            }

            try
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection = Services.Settings.GetOpenConnection();
                }

                query.Result = new DataSet(viewOrTableName + " View");
                batch.Clear();
                batch.Add(query);

                adapter = Services.Settings.ProviderFactory.CreateDataAdapter();
                cmd = _dbConnection.CreateCommand();
                cmd.CommandText = query.Sql;
                cmd.CommandType = CommandType.Text;
                adapter.SelectCommand = cmd;
                adapter.Fill(query.Result);
            }

            // catch (Exception exp)
            // {
            // throw;
            // }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            if (query.Result.Tables.Count > 0)
            {
                dt = query.Result.Tables[0];
                _dataTables[viewOrTableName] = dt;
            }

            return dt;
        }

        /// <summary>The query.</summary>
        /// <param name="sql">The sql.</param>
        /// <returns></returns>
        public DataTable Query(string sql)
        {
            DbDataAdapter adapter = null;
            DbCommand cmd = null;
            DataTable dt = null;
            QueryBatch batch = new QueryBatch();
            Query query = new Query(sql);

            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            if (_dataTables.ContainsKey(sql))
            {
                return _dataTables[sql];
            }

            try
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection = Services.Settings.GetOpenConnection();
                }

                string dataSetName = sql;
                query.Result = new DataSet(dataSetName);
                batch.Clear();
                batch.Add(query);

                adapter = Services.Settings.ProviderFactory.CreateDataAdapter();
                cmd = _dbConnection.CreateCommand();
                cmd.CommandText = query.Sql;
                cmd.CommandType = CommandType.Text;
                adapter.SelectCommand = cmd;
                adapter.Fill(query.Result);
            }

            // catch (Exception exp)
            // {
            // throw;
            // }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            if (query.Result.Tables.Count > 0)
            {
                dt = query.Result.Tables[0];
                _dataTables[sql] = dt;
            }

            return dt;
        }

        /// <summary>The dispose.</summary>
        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
                _dbConnection = null;
            }

            foreach (var dataTable in _dataTables)
            {
                dataTable.Value.Dispose();
            }
        }
    }
}