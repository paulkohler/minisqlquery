#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>The db model view.</summary>
	public class DbModelView : DbModelTable
	{
		/// <summary>Initializes a new instance of the <see cref="DbModelView"/> class.</summary>
		public DbModelView()
		{
			ObjectType = ObjectTypes.View;
		}
	}
}