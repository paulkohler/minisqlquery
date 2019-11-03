#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	Intended as a window level task such as executing a query (like applying ICommand to a window).
    /// </summary>
    public interface IPerformTask
    {
        /// <summary>
        /// 	Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>True if busy.</value>
        bool IsBusy { get; }

        /// <summary>
        /// 	Cancels the current task.
        /// </summary>
        void CancelTask();

        /// <summary>
        /// 	Executes the current task.
        /// </summary>
        void ExecuteTask();
    }
}