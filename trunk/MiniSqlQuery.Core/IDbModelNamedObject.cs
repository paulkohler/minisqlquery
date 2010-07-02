#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>The i db model named object.</summary>
	public interface IDbModelNamedObject
	{
		/// <summary>Gets FullName.</summary>
		/// <value>The full name.</value>
		string FullName { get; }

		/// <summary>Gets Name.</summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>Gets ObjectType.</summary>
		/// <value>The object type.</value>
		string ObjectType { get; }

		/// <summary>Gets Schema.</summary>
		/// <value>The schema.</value>
		string Schema { get; }
	}
}