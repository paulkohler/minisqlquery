using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Creates a simplified view of the database schema for a given provider and connection string.
	/// </summary>
	public class DatabaseMetaDataService
	{
		private string _connection;
		private DbProviderFactory _factory;

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseMetaDataService"/> class.
		/// </summary>
		/// <param name="factory">The provider factory to use.</param>
		/// <param name="connection">The connection string</param>
		public DatabaseMetaDataService(DbProviderFactory factory, string connection)
		{
			_factory = factory;
			_connection = connection;
		}

		/// <summary>
		/// Gets a simplified schema.
		/// </summary>
		/// <remarks><list type="bullet">
		/// <item>
		/// <term>Schema</term>
		/// <description>string</description>
		/// </item>
		/// <item>
		/// <term>Table</term>
		/// <description>string</description>
		/// </item>
		/// <item>
		/// <term>Column</term>
		/// <description>string</description>
		/// </item>
		/// <item>
		/// <term>DataType</term>
		/// <description>string</description>
		/// </item>
		/// <item>
		/// <term>Length</term>
		/// <description>int</description>
		/// </item>
		/// <item>
		/// <term>IsNullable</term>
		/// <description>bool</description>
		/// </item>
		/// </list>
		/// </remarks>
		/// <returns></returns>
		public DataTable GetSchema()
		{
			//CheckInputs(factory, connection);

			DataTable metadata = ExecGetSchema();

			return metadata;
		}

		private DataTable ExecGetSchema()
		{
			DataTable metadata = new DataTable("DbMetaData");

			metadata.Columns.Add("ObjectType", typeof (string));
			metadata.Columns.Add("Schema", typeof (string));
			metadata.Columns.Add("Table", typeof (string));
			metadata.Columns.Add("Column", typeof (string));
			metadata.Columns.Add("DataType", typeof (string));
			metadata.Columns.Add("Length", typeof (int));
			metadata.Columns.Add("IsNullable", typeof (bool));

			DbConnection dbConn = CreateOpenConnection();
			DataTable tables = dbConn.GetSchema("Tables");
			DataTable columns = dbConn.GetSchema("Columns");
			DataTable dataTypes = dbConn.GetSchema("DataTypes");

			DataView dv = new DataView(tables, "TABLE_TYPE='TABLE' OR TABLE_TYPE='BASE TABLE'", "TABLE_NAME", DataViewRowState.CurrentRows);
			FindObjectByType(metadata, dv, columns, dataTypes, "table");

			dv = new DataView(tables, "TABLE_TYPE='VIEW'", "TABLE_NAME", DataViewRowState.CurrentRows);
			FindObjectByType(metadata, dv, columns, dataTypes, "view");

			dbConn.Dispose();
			return metadata;
		}


		public DbModel.DbModelInstance GetDbObjectModel()
		{
			DbModel.DbModelInstance model = new DbModel.DbModelInstance();

			DbConnection dbConn = CreateOpenConnection();
			DataTable tables = dbConn.GetSchema("Tables");
			DataTable columns = dbConn.GetSchema("Columns");
			Dictionary<string, DbModelType> dbTypes = GetDbTypes(dbConn);


			DataView dv = new DataView(tables, "TABLE_TYPE='TABLE' OR TABLE_TYPE='BASE TABLE'", "TABLE_SCHEMA, TABLE_NAME", DataViewRowState.CurrentRows);

			foreach (DataRowView row in dv)
			{
				DataTable schemaTableKeyInfo;
				string schemaName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_SCHEMA"));
				string tableName = MakeSqlFriendly(SafeGetString(row.Row, "TABLE_NAME"));

				DbModelTable dbTable = new DbModelTable
				{
					Schema = schemaName,
					Name = tableName
				};

				model.Tables.Add(dbTable);
				schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);

				foreach (DataRow columnRow in schemaTableKeyInfo.Rows)
				{
					string columnName = SafeGetString(columnRow, "ColumnName");
					string dataType = SafeGetString(columnRow, "DataTypeName"); // todo, use "DataTypes.CreateFormat"
					string providerType = SafeGetString(columnRow, "ProviderType"); // todo, use "DataTypes.CreateFormat"

					DbModelType dbType = DbModelType.Create(
						dbTypes,
						dataType,
						SafeGetInt(columnRow, "ColumnSize"),
						SafeGetInt(columnRow, "Precision"),
						SafeGetInt(columnRow, "Scale"),
						SafeGetString(columnRow, "DataType"));

					DbModelColumn dbColumn = new DbModelColumn
						{
							Name = MakeSqlFriendly(columnName),
							Nullable = SafeGetBool(columnRow, "AllowDBNull"),
							IsKey = SafeGetBool(columnRow, "IsKey"),
							IsUnique = SafeGetBool(columnRow, "IsUnique"),
							IsRowVersion = SafeGetBool(columnRow, "IsRowVersion"),
							DbType = dbType,
						};
					dbTable.Add(dbColumn);
				}
			}

//#if DEBUG
//            string prefix = _factory.GetType().Name + "-";
//            tables.WriteXml(prefix + @"schema-tables-metadata.xml", XmlWriteMode.WriteSchema);
//            columns.WriteXml(prefix + @"schema-columns-metadata.xml", XmlWriteMode.WriteSchema); // DATA_TYPE ->
//            dataTypes.WriteXml(prefix + @"schema-dataTypes-metadata.xml", XmlWriteMode.WriteSchema); // NativeDataType (TypeName/DataType)
//            dbConn.GetSchema().WriteXml(prefix + @"schema.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("Indexes").WriteXml(prefix + @"schema-Indexes.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("ReservedWords").WriteXml(prefix + @"schema-ReservedWords.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("ForeignKeys").WriteXml(prefix + @"schema-ForeignKeys.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("IndexColumns").WriteXml(prefix + @"schema-IndexColumns.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("DataSourceInformation").WriteXml(prefix + @"schema-DataSourceInformation.xml", XmlWriteMode.WriteSchema);
//            dbConn.GetSchema("Restrictions").WriteXml(prefix + @"schema-Restrictions.xml", XmlWriteMode.WriteSchema);
//#endif

			return model;
		}

		protected DataTable GetTableKeyInfo(DbConnection dbConn, string schema, string name)
		{
			DataTable schemaTableKeyInfo;
			using (DbCommand command = dbConn.CreateCommand())
			{
				string tableName = (string.IsNullOrEmpty(schema) ? "" : schema + ".") + name;
				command.CommandText = "SELECT * FROM " + tableName;
				using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTableKeyInfo = reader.GetSchemaTable();
//#if DEBUG
//                    string fileName = string.Format(@"{0}-schema-table-keyinfo--{1}.xml", _factory.GetType().Name, tableName);
//                    schemaTableKeyInfo.WriteXml(fileName, XmlWriteMode.WriteSchema);
//#endif
				}
			}
			return schemaTableKeyInfo;
		}


		private void FindObjectByType(DataTable metadata, DataView dv, DataTable columns, DataTable dataTypes, string objectType)
		{
			foreach (DataRowView row in dv)
			{
				DataView columnView = new DataView(columns, string.Format("TABLE_NAME='{0}'", row["TABLE_NAME"]), "ORDINAL_POSITION",
				                                   DataViewRowState.CurrentRows);
				foreach (DataRowView columnRow in columnView)
				{
					string columnName = SafeGetString(columnRow.Row, "COLUMN_NAME");
					string dataType = SafeGetString(columnRow.Row, "DATA_TYPE");
					int dataTypeId;
					if (int.TryParse(dataType, out dataTypeId))
					{
						// need to look up the type name (e.g. OLEDB report id's not actual type names)
						DataRow[] dtRows = dataTypes.Select("NativeDataType = " + dataTypeId);
						if (dtRows != null && dtRows.Length > 0)
						{
							dataType = dtRows[0]["TypeName"].ToString();
						}
					}

					string schemaName = SafeGetString(columnRow.Row, "TABLE_SCHEMA");
					string tableName = SafeGetString(columnRow.Row, "TABLE_NAME");

					schemaName = MakeSqlFriendly(schemaName);
					tableName = MakeSqlFriendly(tableName);
					columnName = MakeSqlFriendly(columnName);

					metadata.Rows.Add(
						objectType,
						schemaName,
						tableName,
						columnName,
						dataType,
						SafeGetInt(columnRow.Row, "CHARACTER_MAXIMUM_LENGTH"),
						SafeGetBool(columnRow.Row, "IS_NULLABLE"));
				}
			}
		}

		private string MakeSqlFriendly(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			if (name.Contains(" ") || name.Contains("$"))
			{
				// TODO - reserved wods?
				return string.Concat("[", name, "]");
			}
			return name;
		}

		/// <summary>
		/// Get the data types collection.
		/// </summary>
		/// <returns></returns>
		public DataTable GetDataTypes()
		{
			//CheckInputs(factory, connection);
			DbConnection dbConn = CreateOpenConnection();
			DataTable dataTypes = dbConn.GetSchema("DataTypes");
			dbConn.Dispose();
			return dataTypes;
		}

		public DbConnection CreateOpenConnection()
		{
			DbConnection dbConn = _factory.CreateConnection();
			dbConn.ConnectionString = _connection;
			dbConn.Open();
			return dbConn;
		}

		/// <summary>
		/// Gets the description of the data source
		/// </summary>
		/// <returns></returns>
		public string GetDescription()
		{
			//CheckInputs(factory, connection);
			DbConnection dbConn = CreateOpenConnection();

			DataTable info = dbConn.GetSchema("DataSourceInformation");
			dbConn.Dispose();

			string description = ExtractDescription(info);
			return description;
		}

		private string ExtractDescription(DataTable info)
		{
			string description = string.Empty;

			if (info != null && info.Rows.Count > 0)
			{
				description = string.Format(
					"{0} ({1})",
					SafeGetString(info.Rows[0], "DataSourceProductName"),
					SafeGetString(info.Rows[0], "DataSourceProductVersion"));
			}

			return description;
		}

		private string SafeGetString(DataRow row, string columnName)
		{
			string result = string.Empty;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = row[columnName].ToString();
			}

			return result;
		}

		private int SafeGetInt(DataRow row, string columnName)
		{
			int result = -1;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = Convert.ToInt32(row[columnName]);
			}

			return result;
		}

		private bool SafeGetBool(DataRow row, string columnName)
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

		public Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
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
				int providerDbType = SafeGetInt(row, "ProviderDbType");
				int columnSize = SafeGetInt(row, "ColumnSize");
				DbModelType dbType = new DbModelType(typeName, columnSize);
				dbTypes.Add(typeName, dbType);

				dbType.CreateFormat=SafeGetString(row, "CreateFormat");
				dbType.CreateParameters=SafeGetString(row, "CreateParameters");
				dbType.LiteralPrefix = SafeGetString(row, "LiteralPrefix");
				dbType.LiteralSuffix = SafeGetString(row, "LiteralSuffix");
				dbType.SystemType = Type.GetType(SafeGetString(row, "DataType"));
			}

			return dbTypes;
		}
	}
}