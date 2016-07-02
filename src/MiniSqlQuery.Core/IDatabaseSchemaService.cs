#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	The database schema service interface.
	/// </summary>
	public interface IDatabaseSchemaService
	{
		/// <summary>
		/// 	Gets or sets ProviderName.
		/// </summary>
		/// <value>The provider name.</value>
		string ProviderName { get; set; }

		/// <summary>
		/// 	Gets a database object model that represents the items defined by the <paramref name = "connection" />.
		/// </summary>
		/// <param name = "connection">The connection string.</param>
		/// <returns>A database model instance object describing the database.</returns>
		DbModelInstance GetDbObjectModel(string connection);

		/// <summary>
		/// 	Gets database types by querying the schema.
		/// </summary>
		/// <param name = "connection">The database connection.</param>
		/// <returns>A dictionary of database types, the key is the SQL type and the value is the full detail of the type.</returns>
		Dictionary<string, DbModelType> GetDbTypes(DbConnection connection);

		/// <summary>
		/// 	Get the description of the database.
		/// </summary>
		/// <returns>The database description.</returns>
		string GetDescription();
	}
}