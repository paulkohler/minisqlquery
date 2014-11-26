#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The ole db schema service.</summary>
	public class OleDbSchemaService : GenericSchemaService
	{
		/// <summary>The get db types.</summary>
		/// <param name="connection">The connection.</param>
		/// <returns></returns>
		public override Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
		{
			var types = base.GetDbTypes(connection);

			foreach (var dbType in types.Values)
			{
				if (dbType.CreateFormat.Length == 0)
				{
					// probably MS Access
					switch (dbType.Name)
					{
						case "VarChar":
							dbType.CreateFormat = "VarChar({0})";
							break;
						case "VarBinary":
							dbType.CreateFormat = "VarBinary({0})";
							break;
					}
				}
			}

			return types;
		}
	}
}