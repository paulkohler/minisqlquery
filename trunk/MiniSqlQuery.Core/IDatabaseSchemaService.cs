using System;
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	public interface IDatabaseSchemaService
	{
		string ProviderName { get; set; }

		/// <summary>
		/// Gets a database object model that represents the items defined by the <paramref name="connection"/>.
		/// </summary>
		/// <param name="connection">The connection string.</param>
		/// <returns></returns>
		DbModelInstance GetDbObjectModel(string connection);

		Dictionary<string, DbModelType> GetDbTypes(DbConnection connection);
		string GetDescription();
	}
}