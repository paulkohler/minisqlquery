#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>An interface for a "document" that can be navigated with a cursor, e.g. position
	/// at line 1, column 4 etc.</summary>
	public interface INavigatableDocument : ISupportCursorOffset
	{
		/// <summary>The current column the cursor is in.</summary>
		/// <value>The cursor column.</value>
		int CursorColumn { get; }

		/// <summary>The current line the cursor is on.</summary>
		/// <value>The cursor line.</value>
		int CursorLine { get; }

		/// <summary>
		/// Gets the total number of lines in the editor.
		/// </summary>
		/// <value>The total lines.</value>
		int TotalLines { get; }

		/// <summary>Sets the cursor by <paramref name="line"/> and <paramref name="column"/>. </summary>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <returns>The set cursor by location.</returns>
		bool SetCursorByLocation(int line, int column);

		/// <summary>Sets the cursor position by offset.</summary>
		/// <param name="offset">The offset.</param>
		/// <returns>The set cursor by offset.</returns>
		bool SetCursorByOffset(int offset);
	}
}