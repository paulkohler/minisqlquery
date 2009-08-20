using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelObjectBase : IDbModelNamedObject
	{
		#region IDbModelNamedObject Members

		public virtual string Schema { get; set; }
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets the full name of the object which may include the <see cref="IDbModelNamedObject.Schema"/> for example.
		/// </summary>
		/// <value>The full name.</value>

		public virtual string FullName
		{
			get
			{
				if (string.IsNullOrEmpty(Schema))
				{
					return Name;
				}
				return string.Concat(Schema, ".", Name);
			}
		}

		public virtual string ObjectType { get; set; }

		#endregion
	}
}