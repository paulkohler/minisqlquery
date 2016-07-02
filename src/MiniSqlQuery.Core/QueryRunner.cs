#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Helper class to encapsulate query execution
	/// </summary>
	public class QueryRunner
	{
		/// <summary>
		/// 	The connection string value.
		/// </summary>
		private readonly string _connectionString;

		/// <summary>
		/// 	The enable query batching value.
		/// </summary>
		private readonly bool _enableQueryBatching;

		private readonly int _commandTimeout;

		/// <summary>
		/// 	The provider factory.
		/// </summary>
		private readonly DbProviderFactory _factory;

		private DbCommand _command;

	    /// <summary>
		/// 	Initializes a new instance of the <see cref = "QueryRunner" /> class.
		/// </summary>
		/// <param name = "factory">The factory.</param>
		/// <param name = "connectionString">The connection string.</param>
		/// <param name = "enableQueryBatching">The enable query batching.</param>
		/// <param name="commandTimeout"></param>
		public QueryRunner(DbProviderFactory factory, string connectionString, bool enableQueryBatching, int commandTimeout)
		{
			_factory = factory;
			_connectionString = connectionString;
			_enableQueryBatching = enableQueryBatching;
			_commandTimeout = commandTimeout;
			Messages = string.Empty;
		}

		/// <summary>
		/// 	The batch progress.
		/// </summary>
		public event EventHandler<BatchProgressEventArgs> BatchProgress;

		/// <summary>
		/// 	Gets or sets the <see cref="QueryBatch"/> for this query.
		/// </summary>
		/// <value>The query batch.</value>
		public QueryBatch Batch { get; protected set; }

		/// <summary>
		/// 	Gets or sets Exception if any.
		/// </summary>
		/// <value>The exception.</value>
		public DbException Exception { get; protected set; }

		/// <summary>
		/// 	Gets or sets a value indicating whether the query runner is busy.
		/// </summary>
		/// <value>The is busy value.</value>
		public bool IsBusy { get; set; }

		/// <summary>
		/// 	Gets or sets the messages if any.
		/// </summary>
		/// <value>The messages.</value>
		public string Messages { get; protected set; }

		/// <summary>
		/// Creates an instance of a query runner for the specified database.
		/// </summary>
		/// <param name="factory">The factory.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="enableQueryBatching">The enable query batching.</param>
		/// <param name="commandTimeout">The command timeout.</param>
		/// <returns>
		/// A <see cref="QueryRunner"/> instance acording to the parameters.
		/// </returns>
		/// <remarks>
		/// 	<example>
		/// var runner = QueryRunner.Create(DbProviderFactories.GetFactory("System.Data.SqlClient"), connStr, true);
		/// runner.ExecuteQuery("select * from Employees\r\nGO\r\nSelect * from Products");
		/// // runner.Batch.Queries.Count == 2 //
		/// </example>
		/// </remarks>
		public static QueryRunner Create(DbProviderFactory factory, string connectionString, bool enableQueryBatching, int commandTimeout)
		{
			if (factory.GetType().Name == "SqlClientFactory")
			{
				return new SqlQueryRunner(factory, connectionString, enableQueryBatching, commandTimeout);
			}

			// otherwise ise the default
			return new QueryRunner(factory, connectionString, enableQueryBatching, commandTimeout);
		}

		/// <summary>
		/// 	Tests the database connection using the specified provider.
		/// </summary>
		/// <param name = "providerName">Name of the provider.</param>
		/// <param name = "connectionString">The connection string.</param>
		/// <returns>If the connection was successful, null; otherwise the exception object.</returns>
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

		/// <summary>
		/// 	Executes the <paramref name = "sql" /> query.
		/// </summary>
		/// <param name = "sql">The SQL to execute.</param>
		public void ExecuteQuery(string sql)
		{
			ValidateState();

			DbConnection dbConnection = null;
			DbDataAdapter adapter = null;
			_command = null;
			Query query;

			try
			{
				IsBusy = true;

				dbConnection = _factory.CreateConnection();
				dbConnection.ConnectionString = _connectionString;
				dbConnection.Open();

				Messages = string.Empty;
				SubscribeToMessages(dbConnection);

				if (_enableQueryBatching)
				{
					Batch = QueryBatch.Parse(sql);
				}
				else
				{
					Batch = new QueryBatch(sql);
				}

				Batch.StartTime = DateTime.Now;
				adapter = _factory.CreateDataAdapter();
				_command = dbConnection.CreateCommand();
				_command.CommandType = CommandType.Text;
				SetCommandTimeout(_command, _commandTimeout);
				adapter.SelectCommand = _command;

				int queryCount = Batch.Queries.Count;
				for (int i = 0; i < queryCount; i++)
				{
					query = Batch.Queries[i];
					_command.CommandText = query.Sql;
					query.Result = new DataSet("Batch " + (i + 1));
					query.StartTime = DateTime.Now;
					adapter.Fill(query.Result);
					query.EndTime = DateTime.Now;
					OnBatchProgress(new BatchProgressEventArgs(query, queryCount, i + 1));
				}
			}
			catch (DbException dbException)
			{
				HandleBatchException(dbException);
			}
			finally
			{
				if (Batch != null)
				{
					Batch.EndTime = DateTime.Now;
				}

				if (adapter != null)
				{
					adapter.Dispose();
				}

				if (_command != null)
				{
					_command.Dispose();
				}

				IsBusy = false;
				UnsubscribeFromMessages(dbConnection);
			}

			if (Batch != null)
			{
				Batch.Messages = Messages;
			}
		}

        /// <summary>
        /// Cancel the executing command (if busy).
        /// </summary>
        /// <remarks>
        /// Note that this relies on the implementation of the DbCommand.Cancel operation.
        /// </remarks>
	    public void Cancel()
	    {
            if (IsBusy && _command != null)
	        {
                _command.Cancel();
	        }
	    }

	    /// <summary>
		/// Sets the command timeout, currently only tested against MSSQL.
		/// </summary>
		/// <param name="cmd">The command.</param>
		/// <param name="commandTimeout">The command timeout.</param>
		private void SetCommandTimeout(IDbCommand cmd, int commandTimeout)
		{
			if (_factory is SqlClientFactory)
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				cmd.CommandTimeout = commandTimeout;
			}
			else
			{
				Trace.WriteLine("Command Timeout only supported by SQL Client (so far)");
			}
		}

		/// <summary>
		/// 	The handle batch exception.
		/// </summary>
		/// <param name = "dbException">The db exception.</param>
		protected virtual void HandleBatchException(DbException dbException)
		{
			Exception = dbException;
			Messages += dbException.Message + Environment.NewLine;
		}

		/// <summary>
		/// 	The on batch progress.
		/// </summary>
		/// <param name = "e">The events.</param>
		protected void OnBatchProgress(BatchProgressEventArgs e)
		{
			EventHandler<BatchProgressEventArgs> progress = BatchProgress;
			if (progress != null)
			{
				progress(this, e);
			}
		}

		/// <summary>
		/// 	The subscribe to messages.
		/// </summary>
		/// <param name = "connection">The connection.</param>
		protected virtual void SubscribeToMessages(DbConnection connection)
		{
		}

		/// <summary>
		/// 	The unsubscribe from messages.
		/// </summary>
		/// <param name = "connection">The connection.</param>
		protected virtual void UnsubscribeFromMessages(DbConnection connection)
		{
		}

		/// <summary>
		/// 	Ensures that there is enough information available to the class to execute a query.
		/// </summary>
		/// <exception cref = "InvalidOperationException">If there is no connection, "Supply a connection."</exception>
		/// <exception cref = "InvalidOperationException">If there is no connection, "Supply a provider."</exception>
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
	}
}