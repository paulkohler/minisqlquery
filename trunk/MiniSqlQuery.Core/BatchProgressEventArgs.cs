#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core
{
	public class BatchProgressEventArgs : EventArgs 
	{
		public BatchProgressEventArgs(Query query, int count, int index)
		{
			Query = query;
			Count = count;
			Index = index;
		}

		public Query Query { get; private set; }
		public int Count { get; private set; }
		public int Index { get; private set; }
	}
	
}