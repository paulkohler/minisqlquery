using System;

namespace MiniSqlQuery.Core
{
	public interface IEditor
	{
		/// <summary>
		/// The filename of the docuemnt being edited (can be null, as in not saved yet).
		/// </summary>
		string FileName { get; set; }

		/// <summary>
		/// Gets the file filter for this editor (e.g. "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*").
		/// </summary>
		/// <value>The file filter.</value>
		string FileFilter { get; }

		/// <summary>
		/// True if the document has unsaved changes.
		/// </summary>
		bool IsDirty { get; }

		/// <summary>
		/// Loads the file by the path in <see cref="FileName"/>.
		/// </summary>
		void LoadFile();

		/// <summary>
		/// Saves the file by the path in <see cref="FileName"/>.
		/// </summary>
		void SaveFile();

		/// <summary>
		/// The currently selected text (if any) in the editor.
		/// </summary>
		string SelectedText { get; }

		/// <summary>
		/// The contetnts of the editor.
		/// </summary>
		string AllText { get; set; }

		/// <summary>
		/// Inserts <paramref name="text"/> at the current cursor position (selected text is overwritten).
		/// </summary>
		/// <param name="text"></param>
		void InsertText(string text);

		/// <summary>
		/// Clears the selection (deletes selected text if any).
		/// </summary>
		void ClearSelection();

		/// <summary>
		/// Highlights the string starting at <paramref name="offset"/> for <paramref name="length"/> characters.
		/// </summary>
		/// <param name="offset">The offset to start at.</param>
		/// <param name="length">The length.</param>
		void HighlightString(int offset, int length);

		/// <summary>
		/// Sets the syntax mode off the editor.
		/// </summary>
		/// <param name="syntaxName">The mode, e.g. "sql", "cs", "txt" etc.</param>
		void SetSyntax(string syntaxName);
	}
}