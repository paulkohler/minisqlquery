#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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

		public event EventHandler<BatchProgressEventArgs> BatchProgress;

		protected void OnBatchProgress(BatchProgressEventArgs e)
		{
			EventHandler<BatchProgressEventArgs> progress = BatchProgress;
			if (progress != null)
			{
				progress(this, e);
			}
		}

		public QueryRunner(DbProviderFactory factory, string connectionString, bool enableQueryBatching)
		{
			_factory = factory;
			_connectionString = connectionString;
			_enableQueryBatching = enableQueryBatching;
			Messages = string.Empty;
		}

		public QueryBatch Batch { get; protected set; }
		public string Messages { get; protected set; }
		public bool IsBusy { get; set; }
		public DbException Exception { get; protected set; }

		public static QueryRunner Create(DbProviderFactory factory, string connectionString, bool enableQueryBatching)
		{
			if (factory.GetType().Name == "SqlClientFactory")
			{
				return new SqlQueryRunner(factory, connectionString, enableQueryBatching);
			}
			
			// otherwise ise the default
			return new QueryRunner(factory, connectionString, enableQueryBatching);
		}

		public void ExecuteQuery(string sql)
		{
			ValidateState();

			DbConnection dbConnection = null;
			DbDataAdapter adapter = null;
			DbCommand cmd = null;
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
				cmd = dbConnection.CreateCommand();
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;

				int queryCount = Batch.Queries.Count;
				for (int i = 0; i < queryCount; i++)
				{
					query = Batch.Queries[i];
					cmd.CommandText = query.Sql;
					query.Result = new DataSet("Batch " + (i + 1));
					query.StartTime = DateTime.Now;
					adapter.Fill(query.Result);
					query.EndTime = DateTime.Now;
					OnBatchProgress(new BatchProgressEventArgs(query, queryCount, (i+1)));
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
				if (cmd != null)
				{
					cmd.Dispose();
				}
				IsBusy = false;
				UnsubscribeFromMessages(dbConnection);
			}
			if (Batch != null)
			{
				Batch.Messages = Messages;
			}
		}

		protected virtual void HandleBatchException(DbException dbException)
		{
			Exception = dbException;
			Messages += dbException.Message + Environment.NewLine;
		}

		protected virtual void SubscribeToMessages(DbConnection connection)
		{
		}

		protected virtual void UnsubscribeFromMessages(DbConnection connection)
		{
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