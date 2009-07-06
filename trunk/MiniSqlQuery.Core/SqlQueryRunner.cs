using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace MiniSqlQuery.Core
{
	public class SqlQueryRunner : QueryRunner
	{
		public SqlQueryRunner(DbProviderFactory factory, string connectionString, bool enableQueryBatching)
			: base(factory, connectionString, enableQueryBatching)
		{
		}

		protected override void HandleBatchException(DbException dbException)
		{
			Exception = dbException;
			SqlException exp = dbException as SqlException;
			if (exp != null)
			{
				foreach (SqlError error in exp.Errors)
				{
					Messages += string.Format("Error line {0}: {1}.{2}", error.LineNumber, error.Message, Environment.NewLine);
				}
			}
		}

		protected override void SubscribeToMessages(DbConnection connection)
		{
			SqlConnection conn = connection as SqlConnection;
			if (conn!=null)
			{
				conn.InfoMessage += ConnectionInfoMessage;
			}
		}

		protected override void UnsubscribeFromMessages(DbConnection connection)
		{
			SqlConnection conn = connection as SqlConnection;
			if (conn != null)
			{
				conn.InfoMessage -= ConnectionInfoMessage;
			}
		}

		void ConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			Messages += e.Message + Environment.NewLine;
		}
	}
}