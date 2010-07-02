#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>The i editor.</summary>
	public interface IEditor
	{
		/// <summary>The contetnts of the editor.</summary>
		/// <value>The all text.</value>
		string AllText { get; set; }

		/// <summary>
		/// Gets the file filter for this editor (e.g. "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*").
		/// </summary>
		/// <value>The file filter.</value>
		string FileFilter { get; }

		/// <summary>The filename of the docuemnt being edited (can be null, as in not saved yet).</summary>
		/// <value>The file name.</value>
		string FileName { get; set; }

		/// <summary>True if the document has unsaved changes.</summary>
		/// <value>The is dirty.</value>
		bool IsDirty { get; }

		/// <summary>The currently selected text (if any) in the editor.</summary>
		/// <value>The selected text.</value>
		string SelectedText { get; }

		/// <summary>Clears the selection (deletes selected text if any).</summary>
		void ClearSelection();

		/// <summary>Highlights the string starting at <paramref name="offset"/> for <paramref name="length"/> characters.</summary>
		/// <param name="offset">The offset to start at.</param>
		/// <param name="length">The length.</param>
		void HighlightString(int offset, int length);

		/// <summary>Inserts <paramref name="text"/> at the current cursor position (selected text is overwritten).</summary>
		/// <param name="text"></param>
		void InsertText(string text);

		/// <summary>Loads the file by the path in <see cref="FileName"/>.</summary>
		void LoadFile();

		/// <summary>Saves the file by the path in <see cref="FileName"/>.</summary>
		void SaveFile();

		/// <summary>Sets the syntax mode off the editor.</summary>
		/// <param name="syntaxName">The mode, e.g. "sql", "cs", "txt" etc.</param>
		void SetSyntax(string syntaxName);
	}
}