using System;
using System.Windows.Forms;
using System.Data;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// The functions of the editing window.
	/// </summary>
	public interface IQueryEditor : IFindReplaceProvider, INavigatableDocument
	{
		/// <summary>
		/// The currently selected text (if any) in the editor.
		/// </summary>
		string SelectedText { get; }

		/// <summary>
		/// The contetnts of the editor.
		/// </summary>
		string AllText { get; set; }

		/// <summary>
		/// The current data displayed in the result window.
		/// </summary>
		DataSet DataSet { get; }

		/// <summary>
		/// Executes the current query.
		/// </summary>
		void ExecuteQuery();

		/// <summary>
		/// Provides access to the actual editor control.
		/// </summary>
		Control EditorControl { get; }

		/// <summary>
		/// Provides access to the actual results control, each tab contains a data grid view object.
		/// </summary>
		TabControl ResultsControl { get; }

		/// <summary>
		/// The filename of the docuemnt being edited (can be null, as in not saved yet).
		/// </summary>
		string FileName { get; set; }

		/// <summary>
		/// Loads the file by the path in <see cref="FileName"/>.
		/// </summary>
		void LoadFile();

		/// <summary>
		/// Saves the file by the path in <see cref="FileName"/>.
		/// </summary>
		void SaveFile();

		/// <summary>
		/// True if the document has unsaved changes.
		/// </summary>
		bool IsDirty { get; }

		/// <summary>
		/// True if a query is being executed.
		/// </summary>
		bool IsBusy { get; }

		/// <summary>
		/// Sets the "status" text for the form.
		/// </summary>
		/// <param name="text">The message to appear in the status bar.</param>
		void SetStatus(string text);

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
	}
}
