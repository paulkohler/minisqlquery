using System;
using System.Windows.Forms;
using System.Data;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// An interface to the query windows database inspector.
	/// </summary>
	public interface IDatabaseInspector
	{
		/// <summary>
		/// The name of the curent table (or view) with schema if applicable in the tree view that is being clicked.
		/// </summary>
		/// <value>The table or view name or null if none selected.</value>
		string RightClickedTableName { get; }

		/// <summary>
		/// Reloads the meta-data and re-builds the tree.
		/// </summary>
		void LoadDatabaseDetails();

		/// <summary>
		/// Close the window.
		/// </summary>
		void Close();

		/// <summary>
		/// Provides access to the Table context menu strip.
		/// </summary>
		ContextMenuStrip TableMenu { get; }

		/// <summary>
		/// Gets the current database schema info (if any).
		/// </summary>
		DbModelInstance DbSchema { get; }
	}
}
