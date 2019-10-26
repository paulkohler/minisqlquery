#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A "document" that supports reporting the position of it's cursor.
	/// </summary>
	public interface ISupportCursorOffset
	{
		/// <summary>
		/// 	Gets the cursor offset.
		/// </summary>
		/// <value>The cursor offset.</value>
		int CursorOffset { get; }
	}
}