#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Data;
using System.Data.Common;
using MiniSqlQuery.Core;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
	/// <summary>The application settings.</summary>
	public class ApplicationSettings : IApplicationSettings
	{
		/// <summary>The _connection definition.</summary>
		private DbConnectionDefinition _connectionDefinition;

		/// <summary>The _db connection.</summary>
		private DbConnection _dbConnection;

		/// <summary>The _db provider factory.</summary>
		private DbProviderFactory _dbProviderFactory;

		/// <summary>The _definition list.</summary>
		private DbConnectionDefinitionList _definitionList;

		/// <summary>The _untitled document counter.</summary>
		private int _untitledDocumentCounter;

		/// <summary>Initializes a new instance of the <see cref="ApplicationSettings"/> class.</summary>
		public ApplicationSettings()
		{
			_definitionList = new DbConnectionDefinitionList();
		}

		/// <summary>The connection definitions changed.</summary>
		public event EventHandler ConnectionDefinitionsChanged;

		/// <summary>The database connection reset.</summary>
		public event EventHandler DatabaseConnectionReset;

		/// <summary>Gets Connection.</summary>
		public DbConnection Connection
		{
			get
			{
				if (_dbConnection == null)
				{
					_dbConnection = ProviderFactory.CreateConnection();
					_dbConnection.ConnectionString = _connectionDefinition.ConnectionString;
				}

				return _dbConnection;
			}
		}

		/// <summary>Gets or sets ConnectionDefinition.</summary>
		public DbConnectionDefinition ConnectionDefinition
		{
			get { return _connectionDefinition; }
			set
			{
				if (_connectionDefinition != value)
				{
					_connectionDefinition = value;
					ResetConnection();
				}
			}
		}

		/// <summary>Gets or sets DateTimeFormat.</summary>
		public string DateTimeFormat
		{
			get { return Settings.Default.DateTimeFormat; }
			set
			{
				if (Settings.Default.DateTimeFormat != value)
				{
					Settings.Default.DateTimeFormat = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets or sets DefaultConnectionDefinitionFilename.</summary>
		public string DefaultConnectionDefinitionFilename
		{
			get { return Settings.Default.DefaultConnectionDefinitionFilename; }
			set
			{
				if (Settings.Default.DefaultConnectionDefinitionFilename != value)
				{
					Settings.Default.DefaultConnectionDefinitionFilename = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets DefaultFileFilter.</summary>
		public string DefaultFileFilter
		{
			get { return Settings.Default.FileDialogFilter; }
		}

		/// <summary>Gets or sets a value indicating whether EnableQueryBatching.</summary>
		public bool EnableQueryBatching
		{
			get { return Settings.Default.EnableQueryBatching; }
			set
			{
				if (Settings.Default.EnableQueryBatching != value)
				{
					Settings.Default.EnableQueryBatching = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether LoadExternalPlugins.</summary>
		public bool LoadExternalPlugins
		{
			get { return Settings.Default.LoadExternalPlugins; }
			set
			{
				if (Settings.Default.LoadExternalPlugins != value)
				{
					Settings.Default.LoadExternalPlugins = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets or sets NullText.</summary>
		public string NullText
		{
			get { return Settings.Default.NullText; }
			set
			{
				if (Settings.Default.NullText != value)
				{
					Settings.Default.NullText = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets or sets PlugInFileFilter.</summary>
		public string PlugInFileFilter
		{
			get { return Settings.Default.PlugInFileFilter; }
			set
			{
				if (Settings.Default.PlugInFileFilter != value)
				{
					Settings.Default.PlugInFileFilter = value;
					Settings.Default.Save();
				}
			}
		}

		public bool IncludeReadOnlyColumnsInExport
		{
			get { return Settings.Default.IncludeReadOnlyColumnsInExport; }
			set
			{
				if (Settings.Default.IncludeReadOnlyColumnsInExport != value)
				{
					Settings.Default.IncludeReadOnlyColumnsInExport = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>Gets ProviderFactory.</summary>
		public DbProviderFactory ProviderFactory
		{
			get
			{
				if (_dbProviderFactory == null)
				{
					_dbProviderFactory = DbProviderFactories.GetFactory(_connectionDefinition.ProviderName);
				}

				return _dbProviderFactory;
			}
		}

		/// <summary>The close connection.</summary>
		public void CloseConnection()
		{
			if (_dbConnection != null &&
			    (_dbConnection.State != ConnectionState.Closed && _dbConnection.State != ConnectionState.Broken))
			{
				_dbConnection.Close();
			}
		}

		/// <summary>The get connection definitions.</summary>
		/// <returns></returns>
		public DbConnectionDefinitionList GetConnectionDefinitions()
		{
			return _definitionList;
		}

		/// <summary>The get open connection.</summary>
		/// <returns></returns>
		public DbConnection GetOpenConnection()
		{
			DbConnection conn = Connection;
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}

			return conn;
		}

		/// <summary>The get untitled document counter.</summary>
		/// <returns>The get untitled document counter.</returns>
		public int GetUntitledDocumentCounter()
		{
			return ++_untitledDocumentCounter;
		}

		/// <summary>The reset connection.</summary>
		public void ResetConnection()
		{
			if (_dbConnection != null)
			{
				_dbConnection.Dispose();
			}

			_dbProviderFactory = null;
			_dbConnection = null;

			OnDatabaseConnectionReset(EventArgs.Empty);
		}

		/// <summary>The set connection definitions.</summary>
		/// <param name="definitionList">The definition list.</param>
		public void SetConnectionDefinitions(DbConnectionDefinitionList definitionList)
		{
			_definitionList = definitionList;

			OnPastConnectionStringsChanged(EventArgs.Empty);
		}

		/// <summary>The on database connection reset.</summary>
		/// <param name="e">The e.</param>
		protected void OnDatabaseConnectionReset(EventArgs e)
		{
			if (DatabaseConnectionReset != null)
			{
				DatabaseConnectionReset(this, e);
			}
		}

		/// <summary>The on past connection strings changed.</summary>
		/// <param name="e">The e.</param>
		protected void OnPastConnectionStringsChanged(EventArgs e)
		{
			if (ConnectionDefinitionsChanged != null)
			{
				ConnectionDefinitionsChanged(this, e);
			}
		}
	}
}