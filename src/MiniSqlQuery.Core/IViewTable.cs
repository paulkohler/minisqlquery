#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


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