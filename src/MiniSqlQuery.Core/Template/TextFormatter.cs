#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Collections.Generic;

namespace MiniSqlQuery.Core.Template
{
    /// <summary>The i text formatter.</summary>
    public interface ITextFormatter
    {
        /// <summary>The format.</summary>
        /// <param name="text">The text.</param>
        /// <param name="items">The items.</param>
        /// <returns>The format.</returns>
        string Format(string text, Dictionary<string, object> items);
    }
}