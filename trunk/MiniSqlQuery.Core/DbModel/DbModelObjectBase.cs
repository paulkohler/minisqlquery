using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelObjectBase : INamedObject
	{
		#region INamedObject Members

		public virtual string Name { get; set; }

		#endregion
	}
}