#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>The i database schema service.</summary>
	public interface IDatabaseSchemaService
	{
		/// <summary>Gets or sets ProviderName.</summary>
		/// <value>The provider name.</value>
		string ProviderName { get; set; }

		/// <summary>Gets a database object model that represents the items defined by the <paramref name="connection"/>.</summary>
		/// <param name="connection">The connection string.</param>
		/// <returns></returns>
		DbModelInstance GetDbObjectModel(string connection);

		/// <summary>The get db types.</summary>
		/// <param name="connection">The connection.</param>
		/// <returns></returns>
		Dictionary<string, DbModelType> GetDbTypes(DbConnection connection);

		/// <summary>The get description.</summary>
		/// <returns>The get description.</returns>
		string GetDescription();
	}
}