#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System.Windows.Forms;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	An interface for the form that provides the find replace functionality.
    /// </summary>
    public interface IFindReplaceWindow
    {
        /// <summary>
        /// 	Gets or sets the "find string".
        /// </summary>
        /// <value>The find string.</value>
        string FindString { get; set; }

        /// <summary>
        /// 	Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        bool IsDisposed { get; }

        /// <summary>
        /// 	Gets or sets the "replace string".
        /// </summary>
        /// <value>The replace string.</value>
        string ReplaceString { get; set; }

        /// <summary>
        /// 	Gets or sets a value indicating whether the form is "top most" in the window stack.
        /// </summary>
        /// <value><c>true</c> if it's top most; otherwise, <c>false</c>.</value>
        bool TopMost { get; set; }

        /// <summary>
        /// 	Gets or sets a value indicating whether this form is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; set; }

        /// <summary>
        /// 	Shows the window.
        /// </summary>
        /// <param name = "owner">The owner form.</param>
        void Show(IWin32Window owner);
    }
}