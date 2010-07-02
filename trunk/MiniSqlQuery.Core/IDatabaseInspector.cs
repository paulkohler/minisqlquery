#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>An interface to the query windows database inspector.</summary>
	public interface IDatabaseInspector
	{
		/// <summary>Gets ColumnMenu.</summary>
		/// <value>The column menu.</value>
		ContextMenuStrip ColumnMenu { get; }

		/// <summary>Gets the current database schema info (if any).</summary>
		/// <value>The db schema.</value>
		DbModelInstance DbSchema { get; }

		/// <summary>Gets RightClickedModelObject.</summary>
		/// <value>The right clicked model object.</value>
		IDbModelNamedObject RightClickedModelObject { get; }

		/// <summary>
		/// The name of the curent table (or view) with schema if applicable in the tree view that is being clicked.
		/// </summary>
		/// <value>The table or view name or null if none selected.</value>
		string RightClickedTableName { get; }

		/// <summary>Provides access to the Table context menu strip.</summary>
		/// <value>The table menu.</value>
		ContextMenuStrip TableMenu { get; }

		/// <summary>Close the window.</summary>
		void Close();

		/// <summary>Reloads the meta-data and re-builds the tree.</summary>
		void LoadDatabaseDetails();

		/// <summary>The navigate to.</summary>
		/// <param name="modelObject">The model object.</param>
		void NavigateTo(IDbModelNamedObject modelObject);
	}
}