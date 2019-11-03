#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System.Drawing.Printing;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	If implemented it signals that the class supports printing of the "contents" of the object.
    /// </summary>
    public interface IPrintableContent
    {
        /// <summary>
        /// 	Gets the "document" to print (or null if not supported in the current context).
        /// </summary>
        /// <value>The print document.</value>
        PrintDocument PrintDocument { get; }
    }
}