using System;
using System.Data;
using System.Data.Common;
using MiniSqlQuery.Core;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
	public class ApplicationSettings : IApplicationSettings
	{
		private DbConnectionDefinition _connectionDefinition;
		private DbConnection _dbConnection;
		private DbProviderFactory _dbProviderFactory;
		private DbConnectionDefinitionList _definitionList;
		private int _untitledDocumentCounter;

		public ApplicationSettings()
		{
			_definitionList = new DbConnectionDefinitionList();
		}

		#region IApplicationSettings Members

		public event EventHandler ConnectionDefinitionsChanged;
		public event EventHandler DatabaseConnectionReset;

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

		public void CloseConnection()
		{
			if (_dbConnection != null &&
			    (_dbConnection.State != ConnectionState.Closed && _dbConnection.State != ConnectionState.Broken))
			{
				_dbConnection.Close();
			}
		}

		public string DefaultFileFilter
		{
			get { return Settings.Default.FileDialogFilter; }
		}

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

		public int GetUntitledDocumentCounter()
		{
			return ++_untitledDocumentCounter;
		}

		public DbConnectionDefinitionList GetConnectionDefinitions()
		{
			return _definitionList;
		}

		public void SetConnectionDefinitions(DbConnectionDefinitionList definitionList)
		{
			_definitionList = definitionList;

			OnPastConnectionStringsChanged(EventArgs.Empty);
		}

		public DbConnection GetOpenConnection()
		{
			DbConnection conn = Connection;
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			return conn;
		}

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

		#endregion

		protected void OnPastConnectionStringsChanged(EventArgs e)
		{
			if (ConnectionDefinitionsChanged != null)
			{
				ConnectionDefinitionsChanged(this, e);
			}
		}

		protected void OnDatabaseConnectionReset(EventArgs e)
		{
			if (DatabaseConnectionReset != null)
			{
				DatabaseConnectionReset(this, e);
			}
		}
	}
}