#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System.Windows.Forms;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	The functions of the editing window.
    /// </summary>
    public interface IQueryEditor : IPerformTask, IFindReplaceProvider, INavigatableDocument, IQueryBatchProvider, IEditor
    {
        /// <summary>
        /// 	Gets a reference to the actual editor control.
        /// </summary>
        /// <value>The editor control.</value>
        Control EditorControl { get; }

        /// <summary>
        /// 	Sets the "status" text for the form.
        /// </summary>
        /// <param name = "text">The message to appear in the status bar.</param>
        void SetStatus(string text);

        /// <summary>
        /// Access to the code completion window.
        /// </summary>
        CodeCompletionWindow CodeCompletionWindow { get; set; }
    }
}