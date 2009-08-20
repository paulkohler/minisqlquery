using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// An interface for a "document" that can be navigated with a cursor, e.g. position
	/// at line 1, column 4 etc.
	/// </summary>
	public interface INavigatableDocument : ISupportCursorOffset
	{
		/// <summary>
		/// Sets the cursor position by offset.
		/// </summary>
		/// <param name="offset">The offset.</param>
		bool SetCursorByOffset(int offset);

		/// <summary>
		/// Sets the cursor by <paramref name="line"/> and <paramref name="column"/>. 
		/// </summary>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		bool SetCursorByLocation(int line, int column);

		/// <summary>
		/// The current line the cursor is on.
		/// </summary>
		int CursorLine { get; }

		/// <summary>
		/// The current column the cursor is in.
		/// </summary>
		int CursorColumn { get; }

		/// <summary>
		/// Gets the total number of lines in the editor.
		/// </summary>
		/// <value>The total lines.</value>
		int TotalLines { get; }
	}
}
