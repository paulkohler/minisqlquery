#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>The batch progress event args.</summary>
	public class BatchProgressEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="BatchProgressEventArgs"/> class.</summary>
		/// <param name="query">The query.</param>
		/// <param name="count">The count.</param>
		/// <param name="index">The index.</param>
		public BatchProgressEventArgs(Query query, int count, int index)
		{
			Query = query;
			Count = count;
			Index = index;
		}

		/// <summary>Gets Count.</summary>
		/// <value>The count.</value>
		public int Count { get; private set; }

		/// <summary>Gets Index.</summary>
		/// <value>The index.</value>
		public int Index { get; private set; }

		/// <summary>Gets Query.</summary>
		/// <value>The query.</value>
		public Query Query { get; private set; }
	}
}