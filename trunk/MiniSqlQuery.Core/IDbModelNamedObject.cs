#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A database model object, e.g. a column, table etc can implement this interface.
	/// </summary>
	public interface IDbModelNamedObject
	{
		/// <summary>
		/// Gets the full name of the object, e.g. "dbo.FistName".
		/// </summary>
		/// <value>The full name.</value>
		string FullName { get; }

		/// <summary>
		/// Gets the name of the object, e.g. "FistName".
		/// </summary>
		/// <value>The object name.</value>
		string Name { get; }

		/// <summary>
		/// Gets the type of the object, e.g. "VARCHAR".
		/// </summary>
		/// <value>The type of the object.</value>
		string ObjectType { get; }

		/// <summary>
		/// Gets the schema name, e.g. "dbo".
		/// </summary>
		/// <value>The schema name if any.</value>
		string Schema { get; }
	}
}