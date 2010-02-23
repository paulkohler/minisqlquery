#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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

		IDbModelNamedObject RightClickedModelObject { get; }


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

		ContextMenuStrip ColumnMenu { get; }

		/// <summary>
		/// Gets the current database schema info (if any).
		/// </summary>
		DbModelInstance DbSchema { get; }

		void NavigateTo(IDbModelNamedObject modelObject);
	}
}
