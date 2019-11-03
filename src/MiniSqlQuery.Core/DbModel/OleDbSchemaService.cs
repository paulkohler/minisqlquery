#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
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