#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.Collections;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Interface for viewing table and view data.
	/// </summary>
	public interface IViewTable : IPerformTask, IQueryBatchProvider, INavigatableDocument
	{
		string TableName { get; set; }
		string Text { get; set; }
		bool AutoReload { get; }
	}
}