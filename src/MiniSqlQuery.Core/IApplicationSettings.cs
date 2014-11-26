#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Specialized;
using System.Data.Common;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	An interface for the application settings.
	/// </summary>
	public interface IApplicationSettings
	{
		/// <summary>
		/// 	Fired when the list of connection definitions is modified.
		/// </summary>
		/// <seealso cref = "SetConnectionDefinitions" />
		/// <seealso cref = "GetConnectionDefinitions" />
		event EventHandler ConnectionDefinitionsChanged;

		/// <summary>
		/// 	Fired when the database connection (provider and/or connection string) are modified.
		/// </summary>
		/// <seealso cref = "ResetConnection" />
		event EventHandler DatabaseConnectionReset;

		/// <summary>
		/// 	Gets an instance of <see cref = "DbConnection" /> depending on the value of <see cref = "ConnectionDefinition" />.
		/// </summary>
		/// <value>The connection.</value>
		DbConnection Connection { get; }

		/// <summary>
		/// 	Gets or sets a reference to the current connection definiton class.
		/// </summary>
		/// <value>The connection definition.</value>
		DbConnectionDefinition ConnectionDefinition { get; set; }

		/// <summary>
		/// 	Gets or sets the date time format for grid item results (e.g. "yyyy-MM-dd HH:mm:ss.fff").
		/// </summary>
		/// <value>The date time format.</value>
		string DateTimeFormat { get; set; }

		/// <summary>
		/// 	Gets or sets the default connection definition filename. I blank the default is the users profile area.
		/// </summary>
		/// <value>The default connection definition filename.</value>
		string DefaultConnectionDefinitionFilename { get; set; }

		/// <summary>
		/// 	Gets the default filter string for dialog boxes.
		/// </summary>
		/// <value>The default file filter.</value>
		string DefaultFileFilter { get; }

		/// <summary>
		/// 	Gets or sets a value indicating whether to enable query batching using the "GO" keyword.
		/// </summary>
		/// <value><c>true</c> if query batching is enabled; otherwise, <c>false</c>.</value>
		bool EnableQueryBatching { get; set; }

		/// <summary>
		///		Gets or sets a value indicating the command timeout.
		/// </summary>
		/// <value>The command timeout.</value>
		int CommandTimeout { get; set; }

		/// <summary>
		/// 	Gets or sets a value indicating whether to include read-only columns in the export SQL.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if including read-only columns in the export; otherwise, <c>false</c>.
		/// </value>
		bool IncludeReadOnlyColumnsInExport { get; set; }

		/// <summary>
		/// 	Gets or sets a value indicating whether to load plugins or not.
		/// </summary>
		/// <value><c>true</c> if [load plugins]; otherwise, <c>false</c>. The default is <c>true</c>.</value>
		bool LoadExternalPlugins { get; set; }

		/// <summary>
		/// 	Gets or sets the null text of a result (e.g. "&lt;NULL&gt;").
		/// </summary>
		/// <value>The null text.</value>
		string NullText { get; set; }

		/// <summary>
		/// 	Gets or sets the plug in file filter for finding the external plugins (e.g. "*.plugin.dll").
		/// </summary>
		/// <value>The plug in file filter.</value>
		string PlugInFileFilter { get; set; }

		/// <summary>
		/// Gets or sets the most recent files.
		/// </summary>
		/// <value>The most recent files.</value>
		StringCollection MostRecentFiles { get; set; }

		/// <summary>
		/// 	Gets an instance of <see cref = "DbProviderFactory" /> depending on the value of <see cref = "ConnectionDefinition" />.
		/// </summary>
		/// <value>The provider factory.</value>
		DbProviderFactory ProviderFactory { get; }

		/// <summary>
		/// 	Closes the current connection (if any).
		/// </summary>
		void CloseConnection();

		/// <summary>
		/// 	Gets the current connection definitions for this user.
		/// </summary>
		/// <returns>Connection definitions.</returns>
		DbConnectionDefinitionList GetConnectionDefinitions();

		/// <summary>
		/// 	Helper method to get an open connection.
		/// </summary>
		/// <returns>A <see cref = "DbConnection" /> object.</returns>
		DbConnection GetOpenConnection();

		/// <summary>
		/// 	Gets, and increments, the "untitled document counter" starting at 1 for the "session".
		/// </summary>
		/// <value>The untitled document value.</value>
		/// <returns>The get untitled document counter.</returns>
		int GetUntitledDocumentCounter();

		/// <summary>
		/// 	Resets the connection details firing the <see cref = "DatabaseConnectionReset" /> event.
		/// </summary>
		/// <seealso cref = "DatabaseConnectionReset" />
		void ResetConnection();

		/// <summary>
		/// 	Resets the list of connection definitions that are stored in the user profile.
		/// </summary>
		/// <param name = "definitionList">The definition List.</param>
		void SetConnectionDefinitions(DbConnectionDefinitionList definitionList);
	}
}