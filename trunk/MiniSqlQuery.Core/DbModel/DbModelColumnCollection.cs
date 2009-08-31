using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.DbModel
{
	[Obsolete("Just use List - easier for filtering etc")]
	public class DbModelColumnCollection : List<DbModelColumn>
	{
	}
}