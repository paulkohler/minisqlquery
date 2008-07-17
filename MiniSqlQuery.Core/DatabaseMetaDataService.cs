using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Creates a simplified view of the database schema for a given provider and connection string.
	/// </summary>
	public class DatabaseMetaDataService
	{
		DbProviderFactory _factory;
		string _connection;

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

			metadata.Columns.Add("Schema", typeof(string));
			metadata.Columns.Add("Table", typeof(string));
			metadata.Columns.Add("Column", typeof(string));
			metadata.Columns.Add("DataType", typeof(string));
			metadata.Columns.Add("Length", typeof(int));
			metadata.Columns.Add("IsNullable", typeof(bool));

			DbConnection dbConn = CreateOpenConnection();
			DataTable tables = dbConn.GetSchema("Tables");
			DataTable columns = dbConn.GetSchema("Columns");
			DataTable dataTypes = dbConn.GetSchema("DataTypes");

//#if DEBUG
//            dbConn.GetSchema().WriteXml("schema.xml", XmlWriteMode.WriteSchema);
//            tables.WriteXml("tables-metadata.xml", XmlWriteMode.WriteSchema);
//            columns.WriteXml("columns-metadata.xml", XmlWriteMode.WriteSchema); // DATA_TYPE ->
//            dataTypes.WriteXml("dataTypes-metadata.xml", XmlWriteMode.WriteSchema); // NativeDataType (TypeName/DataType)
//#endif

			DataView dv = new DataView(tables, "TABLE_TYPE='TABLE' OR TABLE_TYPE='BASE TABLE'", "TABLE_NAME", DataViewRowState.CurrentRows);
			foreach (DataRowView row in dv)
			{
				DataView columnView = new DataView(columns, string.Format("TABLE_NAME='{0}'", row["TABLE_NAME"]), "ORDINAL_POSITION", DataViewRowState.CurrentRows);
				foreach (DataRowView columnRow in columnView)
				{
					string columnName = SafeGetString(columnRow.Row, "COLUMN_NAME");
					string dataType = SafeGetString(columnRow.Row, "DATA_TYPE");
					int dataTypeId = 0;
					if (int.TryParse(dataType, out dataTypeId))
					{
						// need to look it up
						DataRow[] dtRows = dataTypes.Select("NativeDataType = " + dataTypeId);
						if (dtRows != null && dtRows.Length == 1)
						{
							dataType = dtRows[0]["TypeName"].ToString();
						}
						else
						{
							if (dataType == "130")//access hack
							{
								dataType = "Text";
							}
							else
							{
								dataType += "?";
							}
						}
					}
					metadata.Rows.Add(
						SafeGetString(columnRow.Row, "TABLE_SCHEMA"),
						SafeGetString(columnRow.Row, "TABLE_NAME"),
						columnName,
						dataType,
						SafeGetInt(columnRow.Row, "CHARACTER_MAXIMUM_LENGTH"),
						SafeGetBool(columnRow.Row, "IS_NULLABLE"));
				}
			}

#if DEBUG
			metadata.WriteXml("db-schema.xml", XmlWriteMode.WriteSchema);
#endif

			dbConn.Dispose();
			return metadata;
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

		private DbConnection CreateOpenConnection()
		{
			DbConnection dbConn = null;
			dbConn = _factory.CreateConnection();
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
			int result = 0;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = Convert.ToInt32(row[columnName]);
			}

			return result;
		}

		private bool SafeGetBool(DataRow row, string columnName)
		{
			bool result = false;

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

			return result;
		}
	}
}
