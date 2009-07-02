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