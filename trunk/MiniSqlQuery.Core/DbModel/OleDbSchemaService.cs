using System;
using System.Collections.Generic;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
	public class OleDbSchemaService : GenericSchemaService
	{
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