using System;
using System.Windows.Forms;
using System.Data;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Intended as a window level task such as executing a query.
	/// </summary>
	public interface IPerformTask
	{
		/// <summary>
		/// Executes the current task.
		/// </summary>
		void ExecuteTask();

		/// <summary>
		/// Cancels the current task.
		/// </summary>
		void CancelTask();

		/// <summary>
		/// True if a task is being executed.
		/// </summary>
		bool IsBusy { get; }
	}
	/// <summary>
	/// The functions of the editing window.
	/// </summary>
	public interface IQueryEditor : IPerformTask, IFindReplaceProvider, INavigatableDocument, IQueryBatchProvider, IEditor
	{

		/// <summary>
		/// Obsolete in favour of <see cref="IQueryBatchProvider.Batch"/>. The current data displayed in the result window.
		/// </summary>
		[Obsolete]
		DataSet DataSet { get; }

		/// <summary>
		/// Provides access to the actual editor control.
		/// </summary>
		Control EditorControl { get; }

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
