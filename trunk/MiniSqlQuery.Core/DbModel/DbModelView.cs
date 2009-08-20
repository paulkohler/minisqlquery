using System;

namespace MiniSqlQuery.Core.DbModel
{
	public class DbModelView : DbModelTable
	{
		public DbModelView()
		{
			ObjectType = ObjectTypes.View;
		}
	}
}