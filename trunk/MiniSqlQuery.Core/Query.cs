using System;
using System.Data;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Represents an SQL query, some timings and the result (if executed).
	/// </summary>
	public class Query
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Query"/> class.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		public Query(string sql)
		{
			Sql = sql;
		}

		/// <summary>
		/// Gets or sets the SQL.
		/// </summary>
		/// <value>The SQL.</value>
		public string Sql { get; private set; }

		/// <summary>
		/// Gets or sets the start time.
		/// </summary>
		/// <value>The start time.</value>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Gets or sets the end time.
		/// </summary>
		/// <value>The end time.</value>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// Gets or sets the result.
		/// </summary>
		/// <value>The result.</value>
		public DataSet Result { get; set; }

#if DEBUG
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "Query: " + Sql;
		}
#endif
	}
}