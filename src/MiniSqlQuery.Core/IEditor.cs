#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	The editor interface. Defines the core behaviours for interacting with the core application.
	/// </summary>
	public interface IEditor
	{
		/// <summary>
		/// 	Gets or sets the contetnts of the editor.
		/// </summary>
		/// <value>All the text in the window.</value>
		string AllText { get; set; }

		/// <summary>
		/// 	Gets the file filter for this editor (e.g. "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*").
		/// </summary>
		/// <value>The file filter.</value>
		string FileFilter { get; }

		/// <summary>
		/// 	Gets or sets the filename of the docuemnt being edited (can be null, as in not saved yet).
		/// </summary>
		/// <value>The file name.</value>
		string FileName { get; set; }

		/// <summary>
		/// 	Gets a value indicating whether this instance is dirty or not.
		/// </summary>
		/// <value>The value of <c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
		bool IsDirty { get; }

		/// <summary>
		/// 	Gets the currently selected text (if any) in the editor.
		/// </summary>
		/// <value>The selected text.</value>
		string SelectedText { get; }

		/// <summary>
		/// 	Clears the selection (deletes selected text if any).
		/// </summary>
		void ClearSelection();

		/// <summary>
		/// 	Highlights the string starting at <paramref name = "offset" /> for <paramref name = "length" /> characters.
		/// </summary>
		/// <param name = "offset">The offset to start at.</param>
		/// <param name = "length">The length.</param>
		void HighlightString(int offset, int length);

		/// <summary>
		/// 	Inserts <paramref name = "text" /> at the current cursor position (selected text is overwritten).
		/// </summary>
		/// <param name = "text">The text to insert at the current position.</param>
		void InsertText(string text);

		/// <summary>
		/// 	Loads the file by the path in <see cref = "FileName" />.
		/// </summary>
		void LoadFile();

		/// <summary>
		/// 	Saves the file by the path in <see cref = "FileName" />.
		/// </summary>
		void SaveFile();

		/// <summary>
		/// 	Sets the syntax mode off the editor.
		/// </summary>
		/// <param name = "syntaxName">The mode, e.g. "sql", "cs", "txt" etc.</param>
		void SetSyntax(string syntaxName);
	}
}