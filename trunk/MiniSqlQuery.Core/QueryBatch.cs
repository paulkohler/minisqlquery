#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniSqlQuery.Core
{
	/// <summary>The query batch.</summary>
	public class QueryBatch
	{
		// public string OriginalSql { get; private set; }
		// public List<DataSet> Results { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="QueryBatch"/> class.
		/// A singular batch query.</summary>
		/// <param name="sql">The SQL.</param>
		public QueryBatch(string sql)
			: this()
		{
			// OriginalSql = sql;
			Add(new Query(sql));
		}

		/// <summary>Initializes a new instance of the <see cref="QueryBatch"/> class.</summary>
		public QueryBatch()
		{
			Queries = new List<Query>();
		}

		/// <summary>
		/// Gets or sets the end time of the batch.
		/// </summary>
		/// <value>The end time.</value>
		public DateTime EndTime { get; set; }

		/// <summary>Gets or sets Messages.</summary>
		/// <value>The messages.</value>
		public string Messages { get; set; }

		/// <summary>
		/// Gets the query list for this batch.
		/// </summary>
		/// <value>The queries.</value>
		public List<Query> Queries { get; private set; }

		/// <summary>
		/// Gets or sets the start time of the batch.
		/// </summary>
		/// <value>The start time.</value>
		public DateTime StartTime { get; set; }

		/// <summary>The parse.</summary>
		/// <param name="sql">The sql.</param>
		/// <returns></returns>
		public static QueryBatch Parse(string sql)
		{
			QueryBatch batch = new QueryBatch();

			// exit if nothing to do
			if (sql == null || sql.Trim().Length == 0)
			{
				return batch;
			}

			foreach (string sqlPart in SplitByBatchIndecator(sql, "GO"))
			{
				if (!string.IsNullOrEmpty(sqlPart))
				{
					batch.Add(new Query(sqlPart));
				}
			}

			return batch;
		}

		/// <summary>The add.</summary>
		/// <param name="query">The query.</param>
		public void Add(Query query)
		{
			Queries.Add(query);
		}

		/// <summary>The clear.</summary>
		public void Clear()
		{
			Queries.Clear();
		}

		/// <summary>The split by batch indecator.</summary>
		/// <param name="script">The script.</param>
		/// <param name="batchIndicator">The batch indicator.</param>
		/// <returns></returns>
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