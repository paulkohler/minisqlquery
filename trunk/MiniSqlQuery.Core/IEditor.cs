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
		/// Sets the syntax mode off the editor.
		/// </summary>
		/// <param name="syntaxName">The mode, e.g. "sql", "cs", "txt" etc.</param>
		void SetSyntax(string syntaxName);
	}
}