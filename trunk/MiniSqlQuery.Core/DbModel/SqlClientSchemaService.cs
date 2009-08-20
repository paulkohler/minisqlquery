using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Collections;

namespace MiniSqlQuery.Core.DbModel
{
	public class SqlClientSchemaService : GenericSchemaService
	{
		public override Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
		{
			var types = base.GetDbTypes(connection);

			var date = types["datetime"];
			date.LiteralPrefix = "'";
			date.LiteralSuffix = "'";

			return types;
		}
	}
}