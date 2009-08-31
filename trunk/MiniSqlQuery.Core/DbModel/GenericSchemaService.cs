using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
	public class GenericSchemaService : IDatabaseSchemaService
	{
		private string _connection;

		public string ProviderName { get; set; }

		/// <summary>
		/// Gets a database object model that represents the items defined by the <paramref name="connection"/>.
		/// </summary>
		/// <param name="connection">The connection string.</param>
		/// <returns></returns>
		public virtual DbModelInstance GetDbObjectModel(string connection)
		{
			_connection = connection;

			DbModelInstance model = new DbModelInstance();
			DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

			using (DbConnection dbConn = factory.CreateConnection())
			{
				dbConn.ConnectionString = connection;
				dbConn.Open();

				DataTable tables = dbConn.GetSchema("Tables");
				Dictionary<string, DbModelType> dbTypes = GetDbTypes(dbConn);
				model.Types = dbTypes;
				model.ProviderName = ProviderName;
				model.ConnectionString = _connection;

				DataView tablesDV = new DataView(tables, "TABLE_TYPE='TABLE' OR TABLE_TYPE='BASE TABLE'", "TABLE_SCHEMA, TABLE_NAME", DataViewRowState.CurrentRows);

				foreach (DataRowView row in tablesDV)
				{
					string schemaName = SafeGetString(row.Row, "TABLE_SCHEMA");
					string tableName = SafeGetString(row.Row, "TABLE_NAME");
					//string schemaName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_SCHEMA"));
					//string tableName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_NAME"));

					DbModelTable dbTable = new DbModelTable { Schema = schemaName, Name = tableName };
					model.Tables.Add(dbTable);

					DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);
					GetColumnsForTable(dbTable, schemaTableKeyInfo, dbTypes);
				}

				DataView viewsDV = new DataView(tables, "TABLE_TYPE='VIEW'", "TABLE_SCHEMA, TABLE_NAME", DataViewRowState.CurrentRows);
				foreach (DataRowView row in viewsDV)
				{
					string schemaName = SafeGetString(row.Row, "TABLE_SCHEMA");
					string tableName = SafeGetString(row.Row, "TABLE_NAME");
					//string schemaName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_SCHEMA"));
					//string tableName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_NAME"));

					DbModelView dbTable = new DbModelView { Schema = schemaName, Name = tableName };
					model.Views.Add(dbTable);

					DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);
					GetColumnsForTable(dbTable, schemaTableKeyInfo, dbTypes);
				}

				model.Tables.ForEach(delegate(DbModelTable t)
				                     	{
				                     		GetForiegnKeyReferencesForTable(dbConn, t);
				                     		ProcessForiegnKeyReferencesForTable(dbConn, t);
				                     	});
				model.Views.ForEach(delegate(DbModelTable t)
				                    	{
				                    		GetForiegnKeyReferencesForTable(dbConn, t);
				                    		ProcessForiegnKeyReferencesForTable(dbConn, t);
				                    	});
			}

			return model;
		}

		protected virtual void GetForiegnKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			//foreach (DbModelColumn column in dbTable.Columns)
			//{
			//    // KF info for DB's varies widley, needs to be implemented by derived class
			//    column.ForiegnKeyReference = DbModelForiegnKeyReference.NullForiegnKeyReference;
			//}
		}

		protected virtual void ProcessForiegnKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
		}

		protected virtual void GetColumnsForTable(DbModelTable dbTable, DataTable schemaTableKeyInfo, Dictionary<string, DbModelType> dbTypes)
		{
			foreach (DataRow columnRow in schemaTableKeyInfo.Rows)
			{
				if (SafeGetBool(columnRow, "IsHidden"))
				{
					continue;
				}

				string columnName = SafeGetString(columnRow, "ColumnName");
				string dataType = GetDataTypeNameForColumn(dbTable, schemaTableKeyInfo, columnRow);

				// note - need a better work around for columns missing the data type info (e.g. access)
				if (string.IsNullOrEmpty(dataType))
				{
					// try using the "ProviderDbType" to match
					string providerDbType = SafeGetString(columnRow, "ProviderType");
					foreach (var type in dbTypes.Values)
					{
						if (type.ProviderDbType == providerDbType)
						{
							dataType = type.Name;
							break;
						}
					}
				}

				DbModelType dbType = DbModelType.Create(
					dbTypes,
					dataType,
					SafeGetInt(columnRow, "ColumnSize"),
					SafeGetInt(columnRow, "Precision"),
					SafeGetInt(columnRow, "Scale"),
					SafeGetString(columnRow, "DataType"));

				// todo - FK info

				DbModelColumn dbColumn = new DbModelColumn
				                         {
				                         	Name = columnName,
				                         	//Name = MakeSqlFriendly(columnName),
				                         	Nullable = SafeGetBool(columnRow, "AllowDBNull"),
				                         	IsKey = SafeGetBool(columnRow, "IsKey"),
				                         	IsUnique = SafeGetBool(columnRow, "IsUnique"),
				                         	IsRowVersion = SafeGetBool(columnRow, "IsRowVersion"),
											IsIdentity = SafeGetBool(columnRow, "IsIdentity"),
											IsAutoIncrement = SafeGetBool(columnRow, "IsAutoIncrement"),
											IsReadOnly = SafeGetBool(columnRow, "IsReadOnly"),
				                         	DbType = dbType,
				                         };
				dbTable.Add(dbColumn);
			}
		}

		protected virtual string GetDataTypeNameForColumn(DbModelTable dbTable, DataTable schemaTableKeyInfo, DataRow columnRow)
		{
			return SafeGetString(columnRow, "DataTypeName");
		}

		protected virtual DataTable GetTableKeyInfo(DbConnection dbConn, string schema, string name)
		{
			DataTable schemaTableKeyInfo;
			using (DbCommand command = dbConn.CreateCommand())
			{
				string tableName = (string.IsNullOrEmpty(schema) ? "" : schema + ".") + name;
				command.CommandText = "SELECT * FROM " + MakeSqlFriendly(tableName);
				using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTableKeyInfo = reader.GetSchemaTable();
				}
			}
			return schemaTableKeyInfo;
		}


		///// <summary>
		///// Gets the description of the data source
		///// </summary>
		///// <returns></returns>
		//public string GetDescription()
		//{
		//    //CheckInputs(factory, connection);
		//    DbConnection dbConn = CreateOpenConnection();

		//    DataTable info = dbConn.GetSchema("DataSourceInformation");
		//    dbConn.Dispose();

		//    string description = ExtractDescription(info);
		//    return description;
		//}

		//private string ExtractDescription(DataTable info)
		//{
		//    string description = string.Empty;

		//    if (info != null && info.Rows.Count > 0)
		//    {
		//        description = string.Format(
		//            "{0} ({1})",
		//            SafeGetString(info.Rows[0], "DataSourceProductName"),
		//            SafeGetString(info.Rows[0], "DataSourceProductVersion"));
		//    }

		//    return description;
		//}

		protected virtual string MakeSqlFriendly(string name)
		{
			return Utility.MakeSqlFriendly(name);
		}


		protected string SafeGetString(DataRow row, string columnName)
		{
			string result = string.Empty;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = row[columnName].ToString();
			}

			return result;
		}

		protected int SafeGetInt(DataRow row, string columnName)
		{
			int result = -1;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = Convert.ToInt32(row[columnName]);
			}

			return result;
		}

		protected bool SafeGetBool(DataRow row, string columnName)
		{
			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				string value = row[columnName].ToString();
				switch (value.ToLower())
				{
					case "no":
					case "false":
						return false;

					case "yes":
					case "true":
						return true;
				}
			}

			return false;
		}

		public virtual Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}

			Dictionary<string, DbModelType> dbTypes = new Dictionary<string, DbModelType>();

			DataTable dataTypes = connection.GetSchema("DataTypes");

			foreach (DataRow row in dataTypes.Rows)
			{
				string typeName = SafeGetString(row, "TypeName");
				int columnSize = SafeGetInt(row, "ColumnSize");
				DbModelType dbType = new DbModelType(typeName, columnSize);
				dbTypes.Add(typeName.ToLower(), dbType);

				dbType.CreateFormat = SafeGetString(row, "CreateFormat");
				dbType.CreateParameters = SafeGetString(row, "CreateParameters");
				dbType.LiteralPrefix = SafeGetString(row, "LiteralPrefix");
				dbType.LiteralSuffix = SafeGetString(row, "LiteralSuffix");
				dbType.SystemType = Type.GetType(SafeGetString(row, "DataType"));
				dbType.ProviderDbType = SafeGetString(row, "ProviderDbType");
			}

			return dbTypes;
		}

		public string GetDescription()
		{
			return "todo";
		}
	}
}