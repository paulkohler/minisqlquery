using System;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Creates a simplified view of the database schema for a given provider and connection string.
	/// </summary>
	public class DatabaseMetaDataService
	{
		public static IDatabaseSchemaService Create(string providerName)
		{
			switch (providerName)
			{
				case "System.Data.OleDb":
					return new OleDbSchemaService() { ProviderName = providerName };

				case "System.Data.SqlClient":
					return new SqlClientSchemaService { ProviderName = providerName };

				case "System.Data.SqlServerCe.3.5":
					return new SqlCeSchemaService {ProviderName = providerName};

				default:
					return new GenericSchemaService {ProviderName = providerName};
			}
		}
	}
}