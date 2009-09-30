using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateData : IDisposable
	{
		private DbConnection _dbConnection;
		readonly Dictionary<string, DataTable> _dataTables = new Dictionary<string, DataTable>();

		public TemplateData(IApplicationServices services)
		{
			Services = services;
		}

		public IApplicationServices Services { get; private set; }

		public DataTable Get(string name)
		{
			DbDataAdapter adapter = null;
			DbCommand cmd = null;
			DataTable dt = null;
			QueryBatch batch = new QueryBatch();
			Query query = new Query("SELECT * FROM " + Utility.MakeSqlFriendly(name));

			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

			if (_dataTables.ContainsKey(name))
			{
				return _dataTables[name];
			}

			try
			{
				if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
				{
					_dbConnection = Services.Settings.GetOpenConnection();
				}

				query.Result = new DataSet(name + " View");
				batch.Clear();
				batch.Add(query);

				adapter = Services.Settings.ProviderFactory.CreateDataAdapter();
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = query.Sql;
				cmd.CommandType = CommandType.Text;
				adapter.SelectCommand = cmd;
				adapter.Fill(query.Result);
			}
			//catch (Exception exp)
			//{
			//    throw;
			//}
			finally
			{
				if (adapter != null)
				{
					adapter.Dispose();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
			}

			if (query.Result.Tables.Count > 0)
			{
				dt = query.Result.Tables[0];
				_dataTables[name] = dt;
			}

			return dt;
		}

		public object ColumnValue(DataRow row, string columnName)
		{
			return row[columnName];
		}

		public void Dispose()
		{
			if (_dbConnection != null)
			{
				_dbConnection.Dispose();
				_dbConnection = null;
			}
		}
	}
}