#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	A text finding serice interface. A window can implement this interface and sit will allow searching of its text.
    /// </summary>
    public interface ITextFindService
    {
        /// <summary>
        /// 	Finds the next string of text depending on the contrnts of the <paramref name = "request" />.
        /// </summary>
        /// <param name = "request">The request.</param>
        /// <returns>A find request with position updated.</returns>
        FindTextRequest FindNext(FindTextRequest request);
    }
}