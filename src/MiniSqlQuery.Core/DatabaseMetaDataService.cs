#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Creates <see cref="IDatabaseSchemaService"/> instance to provide a simplified view of the database schema.
	/// </summary>
	public class DatabaseMetaDataService
	{
		/// <summary>
		/// 	Creates a schema service for a database depending on the <paramref name = "providerName" />.
		/// 	Currently has specific providers for MSSQL, MSSQL CE and OLE DB.
		/// 	The <see cref="GenericSchemaService"/> is the fallback option.
		/// </summary>
		/// <param name = "providerName">The provider name, e.g. "System.Data.SqlClient".</param>
		/// <returns>
		/// A schema serivce for the based on the <paramref name = "providerName" />. 
		/// The default is <see cref = "GenericSchemaService" />.
		/// </returns>
		public static IDatabaseSchemaService Create(string providerName)
		{            
			switch (providerName)
			{
				case "System.Data.OleDb":
					return new OleDbSchemaService {ProviderName = providerName};

				case "System.Data.SqlClient":
					return new SqlClientSchemaService {ProviderName = providerName};
                case "Oracle.DataAccess.Client":
                    return new OracleSchemaService { ProviderName = providerName };
				default:
					// The SQL CE types tend to include the version number within the provider name, hence "StartsWith"
					if (providerName.StartsWith("System.Data.SqlServerCe."))
					{
						return new SqlCeSchemaService {ProviderName = providerName};
					}

					return new GenericSchemaService {ProviderName = providerName};
			}
		}
	}
}