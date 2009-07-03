using System;
using System.Data;
using System.Data.Common;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Helper class to encapsulate query execution
	/// </summary>
	public class QueryRunner
	{
		private readonly string _connectionString;
		private readonly bool _enableQueryBatching;
		private readonly DbProviderFactory _factory;

		public QueryRunner(DbProviderFactory factory, string connectionString, bool enableQueryBatching)
		{
			_factory = factory;
			_connectionString = connectionString;
			_enableQueryBatching = enableQueryBatching;
			Messages = string.Empty;
		}

		public QueryBatch Batch { get; private set; }
		public string Messages { get; private set; }
		public bool IsBusy { get; set; }

		public void ExecuteQuery(string sql)
		{
			ValidateState();

			DbConnection dbConnection;
			DbDataAdapter adapter = null;
			DbCommand cmd = null;

			try
			{
				IsBusy = true;

				dbConnection = _factory.CreateConnection();
				dbConnection.ConnectionString = _connectionString;
				dbConnection.Open();

				Messages = string.Empty;
				//if (dbConnection is System.Data.SqlClient.SqlConnection)
				//{
				//    // todo - inprogress - support various InfoMessage events
				//    ((System.Data.SqlClient.SqlConnection)dbConnection).InfoMessage += SqlClienInfoMessage;
				//}

				if (_enableQueryBatching)
				{
					Batch = QueryBatch.Parse(sql);
				}
				else
				{
					Batch = new QueryBatch(sql);
				}

				adapter = _factory.CreateDataAdapter();
				cmd = dbConnection.CreateCommand();
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;

				for (int i = 0; i < Batch.Queries.Count; i++)
				{
					Query query = Batch.Queries[i];
					cmd.CommandText = query.Sql;
					query.Result = new DataSet("Batch " + (i + 1));
					query.StartTime = DateTime.Now;
					adapter.Fill(query.Result);
					query.EndTime = DateTime.Now;
				}
			}
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
				IsBusy = false;
				//if (dbConnection is System.Data.SqlClient.SqlConnection)
				//{
				//    ((System.Data.SqlClient.SqlConnection)dbConnection).InfoMessage -= SqlClienInfoMessage;
				//}
			}
		}

		private void ValidateState()
		{
			if (string.IsNullOrEmpty(_connectionString))
			{
				throw new InvalidOperationException("Supply a connection.");
			}

			if (_factory == null)
			{
				throw new InvalidOperationException("Supply a provider.");
			}
		}

		/// <summary>
		/// Tests the database connection using the specified provider.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <returns></returns>
		public static Exception TestDbConnection(string providerName, string connectionString)
		{
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				using (DbConnection connection = factory.CreateConnection())
				{
					connection.ConnectionString = connectionString;
					connection.Open();
					if (connection.State == ConnectionState.Open)
					{
						return null;
					}
				}
			}
			catch (Exception e)
			{
				return e;
			}
			throw new InvalidOperationException("Connection test failed");
		}
	}
}