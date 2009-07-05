using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniSqlQuery.Core
{
	public class QueryBatch
	{
		//public string OriginalSql { get; private set; }
		//public List<DataSet> Results { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryBatch"/> class.
		/// A singular batch query.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		public QueryBatch(string sql)
			: this()
		{
			//OriginalSql = sql;
			Add(new Query(sql));
		}

		public QueryBatch()
		{
			Queries = new List<Query>();
		}

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

		/// <summary>
		/// Gets or sets the end time of the batch.
		/// </summary>
		/// <value>The end time.</value>
		public DateTime EndTime { get; set; }

		public string Messages { get; set; }

		public void Add(Query query)
		{
			Queries.Add(query);
		}

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

		public void Clear()
		{
			Queries.Clear();
		}

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