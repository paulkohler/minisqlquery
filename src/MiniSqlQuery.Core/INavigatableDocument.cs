#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	An interface for a "document" that can be navigated with a cursor, e.g. position at line 1, column 4 etc.
    /// </summary>
    public interface INavigatableDocument : ISupportCursorOffset
    {
        /// <summary>
        /// 	Gets the current column the cursor is in.
        /// </summary>
        /// <value>The cursor column.</value>
        int CursorColumn { get; }

        /// <summary>
        /// 	Gets the current line the cursor is on.
        /// </summary>
        /// <value>The cursor line.</value>
        int CursorLine { get; }

        /// <summary>
        /// 	Gets the the total number of lines in the editor.
        /// </summary>
        /// <value>The total lines.</value>
        int TotalLines { get; }

        /// <summary>
        /// 	Sets the cursor by <paramref name = "line" /> and <paramref name = "column" />.
        /// </summary>
        /// <param name = "line">The line number.</param>
        /// <param name = "column">The column number.</param>
        /// <returns>The set cursor by location.</returns>
        bool SetCursorByLocation(int line, int column);

        /// <summary>
        /// 	Sets the cursor position by offset.
        /// </summary>
        /// <param name = "offset">The offset for the cursor.</param>
        /// <returns>The set cursor by offset.</returns>
        bool SetCursorByOffset(int offset);
    }
}