#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	The query batch class represents a series of SQL statements to execute against a database.
    /// </summary>
    public class QueryBatch
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "QueryBatch" /> class.
        /// 	A singular batch query.
        /// </summary>
        /// <param name = "sql">The SQL to create a single query from.</param>
        /// <seealso cref="Parse"/>
        public QueryBatch(string sql)
            : this()
        {
            // OriginalSql = sql;
            Add(new Query(sql));
        }

        /// <summary>
        /// 	Initializes a new instance of the <see cref = "QueryBatch" /> class.
        /// </summary>
        public QueryBatch()
        {
            Queries = new List<Query>();
        }

        /// <summary>
        /// 	Gets or sets the end time of the batch.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 	Gets or sets Messages.
        /// </summary>
        /// <value>The messages.</value>
        public string Messages { get; set; }

        /// <summary>
        /// 	Gets the query list for this batch.
        /// </summary>
        /// <value>The queries.</value>
        public List<Query> Queries { get; private set; }

        /// <summary>
        /// 	Gets or sets the start time of the batch.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 	Parses an <paramref name="sql"/> string creating a <see cref="QueryBatch"/> as a result.
        ///		If query batching is enabled, the <paramref name="sql"/> string is split into multiple <see cref="Query"/> objects.
        /// </summary>
        /// <param name = "sql">The SQL string.</param>
        /// <returns>A <see cref="QueryBatch"/> object with 0, 1 or many <see cref="Query"/> objects.</returns>
        public static QueryBatch Parse(string sql)
        {
            var batch = new QueryBatch();

            // exit if nothing to do
            if (sql == null || sql.Trim().Length == 0)
            {
                return batch;
            }

            foreach (string sqlPart in SplitByBatchIndecator(sql, "GO").Where(sqlPart => !string.IsNullOrEmpty(sqlPart)))
            {
                batch.Add(new Query(sqlPart));
            }

            return batch;
        }

        /// <summary>
        /// 	Adds a query to this batch.
        /// </summary>
        /// <param name = "query">The query.</param>
        public void Add(Query query)
        {
            Queries.Add(query);
        }

        /// <summary>
        /// 	Clears all queries in this batch.
        /// </summary>
        public void Clear()
        {
            Queries.Clear();
        }

        /// <summary>
        /// 	Splits a <paramref name="script"/> by the <paramref name="batchIndicator"/>, typically "GO".
        ///		The batch indicator needs to be on a line by itself.
        /// </summary>
        /// <param name = "script">The SQL script.</param>
        /// <param name = "batchIndicator">The batch indicator, e.g. "GO".</param>
        /// <returns>An enumerable list of stings.</returns>
        private static IEnumerable<string> SplitByBatchIndecator(string script, string batchIndicator)
        {
            string pattern = string.Concat("^\\s*", batchIndicator, "\\s*$");
            RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline;

            foreach (string batch in Regex.Split(script, pattern, options))
            {
                yield return batch.Trim();
            }
        }
    }
}