#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	A query batch provider is a class (typically a window) that manages a 
    /// 	batch query and therefore has a data result etc.
    /// </summary>
    public interface IQueryBatchProvider
    {
        /// <summary>
        /// 	Gets a reference to the batch.
        /// </summary>
        /// <value>The query batch.</value>
        QueryBatch Batch { get; }
    }
}