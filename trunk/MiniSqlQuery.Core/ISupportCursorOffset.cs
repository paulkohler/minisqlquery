#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>A "document" that supports reporting the position of it's cursor</summary>
	public interface ISupportCursorOffset
	{
		/// <summary>
		/// Gets the cursor offset.
		/// </summary>
		/// <value>The cursor offset.</value>
		int CursorOffset { get; }
	}
}