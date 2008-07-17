using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core;
using System.Data.Common;
using System.Data;
using System.Collections.Specialized;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery
{
	public class ApplicationSettings : IApplicationSettings
	{
		DbProviderFactory _dbProviderFactory;
		DbConnection _dbConnection;
	    readonly List<ConnectionDefinition> _connectionDefinitions;
		ConnectionDefinition _connectionDefinition;

		public event EventHandler ConnectionDefinitionsChanged;
		public event EventHandler DatabaseConnectionReset;

		public ApplicationSettings()
		{
			_connectionDefinition = ConnectionDefinition.Default;
			_connectionDefinitions = new List<ConnectionDefinition>();
		}

		public ConnectionDefinition ConnectionDefinition
		{
			get
			{
				return _connectionDefinition;
			}
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

		public string DefaultFileFilter 
		{
			get
			{
				return Settings.Default.FileDialogFilter;
			}
		}

		public ConnectionDefinition[] GetConnectionDefinitions()
		{
			return _connectionDefinitions.ToArray();
		}

		public void SetConnectionDefinitions(ConnectionDefinition[] connections)
		{
			_connectionDefinitions.Clear();
			_connectionDefinitions.AddRange(connections);

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
