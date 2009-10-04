#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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