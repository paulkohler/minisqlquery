#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Interface for viewing table and view data.
	/// </summary>
	public interface IViewTable : IPerformTask, IQueryBatchProvider, INavigatableDocument
	{
		/// <summary>
		/// 	Gets a value indicating whether AutoReload.
		/// </summary>
		/// <value>The auto reload.</value>
		bool AutoReload { get; }

		/// <summary>
		/// 	Gets or sets TableName.
		/// </summary>
		/// <value>The table name.</value>
		string TableName { get; set; }

		/// <summary>
		/// 	Gets or sets Text of the window, i.e. the table name.
		/// </summary>
		/// <value>The text of the window.</value>
		string Text { get; set; }
	}
}