#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	Tracks a list of filenames with promotion etc.
    /// </summary>
    public interface IMostRecentFilesService
    {
        /// <summary>
        /// Occurs when a change to the most recent files list is made.
        /// </summary>
        event EventHandler<MostRecentFilesChangedEventArgs> MostRecentFilesChanged;

        /// <summary>
        /// Gets the filenames on the MRU list.
        /// </summary>
        /// <value>The filenames.</value>
        IList<string> Filenames { get; }

        /// <summary>
        /// Gets the maximum number of MRU commands.
        /// </summary>
        /// <value>The maximum number of MRU commands.</value>
        int MaxCommands { get; }

        /// <summary>
        /// Registers the specified <paramref name="filename"/>.
        /// </summary>
        /// <param name="filename">The <paramref name="filename"/> to register.</param>
        void Register(string filename);

        /// <summary>
        /// Removes the specified <paramref name="filename"/> from the list.
        /// </summary>
        /// <param name="filename">The <paramref name="filename"/> to remove.</param>
        void Remove(string filename);
    }
}