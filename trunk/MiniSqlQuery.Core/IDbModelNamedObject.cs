using System;

namespace MiniSqlQuery.Core
{
	public interface IDbModelNamedObject
	{
		string Schema { get; }
		string Name { get; }
		string FullName { get; }
		string ObjectType { get; }
	}
}