using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	public class SqlCeSchemaService : GenericSchemaService
	{
		internal static readonly int MAX_BINARY_COLUMN_SIZE = 8000;
		internal static readonly int MAX_IMAGE_COLUMN_SIZE = 1073741823;
		internal static readonly int MAX_NCHAR_COLUMN_SIZE = 4000;
		internal static readonly int MAX_NTEXT_COLUMN_SIZE = 536870911;

		private string _connection;

		public override DbModelInstance GetDbObjectModel(string connection)
		{
			_connection = connection;

			DbModelInstance model = new DbModelInstance();
			DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

			using (DbConnection dbConn = factory.CreateConnection())
			{
				dbConn.ConnectionString = connection;
				dbConn.Open();

				QueryTableNames(dbConn, model);
				Dictionary<string, DbModelType> dbTypes = GetDbTypes(dbConn);
				model.Types = dbTypes;
				model.ProviderName = ProviderName;
				model.ConnectionString = _connection;

				foreach (DbModelTable table in model.Tables)
				{
					DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, null, table.Name);
					GetColumnsForTable(table, schemaTableKeyInfo, dbTypes);
				}

				return model;
			}
		}

		protected override string GetDataTypeNameForColumn(DbModelTable dbTable, DataTable schemaTableKeyInfo, DataRow columnRow)
		{
			return SafeGetString(columnRow, "ProviderType");
		}

		private void QueryTableNames(DbConnection dbConn, DbModelInstance model)
		{
			using (var cmd = dbConn.CreateCommand())
			{
				cmd.CommandText = "SELECT table_name FROM information_schema.tables WHERE TABLE_TYPE = N'TABLE'";
				cmd.CommandType = CommandType.Text;
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						DbModelTable table = new DbModelTable();
						//table.Name = MakeSqlFriendly((string)reader["table_name"]);
						table.Name = (string)reader["table_name"];
						model.Add(table);
					}
				}
			}
		}

		/// <summary>
		/// Gets the db types for the SQL CE provider.
		/// </summary>
		/// <param name="connection">The connection (not required).</param>
		/// <returns></returns>
		public override Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
		{
			Dictionary<string, DbModelType> dbTypes = new Dictionary<string, DbModelType>();
			string dataTypesSql = "SELECT * FROM information_schema.provider_types";
			using (var cmd = connection.CreateCommand())
			{
				cmd.CommandText = dataTypesSql;
				cmd.CommandType = CommandType.Text;
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string typeName = (string) reader["TYPE_NAME"];
						int columnSize = Convert.ToInt32(reader["COLUMN_SIZE"]);
						DbModelType dbType = new DbModelType(typeName, columnSize);

						dbType.CreateParameters = Convert.ToString(reader["CREATE_PARAMS"]);
						dbType.LiteralPrefix = Convert.ToString(reader["LITERAL_PREFIX"]);
						dbType.LiteralSuffix = Convert.ToString(reader["LITERAL_SUFFIX"]);
						dbType.ProviderDbType = Convert.ToString(reader["DATA_TYPE"]);

						FixCreateFormat(dbType);
						FixMaxLengths(dbType);
						AssignSystemTypes(dbType);

						dbTypes.Add(typeName, dbType);
					}
				}
			}
			return dbTypes;
		}

		private void FixMaxLengths(DbModelType dbType)
		{
			switch (dbType.Name.ToLower())
			{
				case "nchar":
				case "nvarchar":
					dbType.Length = MAX_NCHAR_COLUMN_SIZE;
					break;
				case "ntext":
					dbType.Length =MAX_NTEXT_COLUMN_SIZE;
					break;
				case "binary":
				case "varbinary":
					dbType.Length = MAX_BINARY_COLUMN_SIZE;
					break;
				case "image":
					dbType.Length =MAX_IMAGE_COLUMN_SIZE;
					break;
			}
		}
		
		private void FixCreateFormat(DbModelType dbType)
		{
			switch (dbType.Name.ToLower())
			{
				case "nchar":
				case "nvarchar":
				case "binary":
				case "varbinary":
					dbType.CreateFormat = dbType.Name.ToLower() + "({0})";
					break;

				case "decimal":
				case "numeric":
					dbType.CreateFormat = dbType.Name.ToLower() + "({0}, {1})";
					break;
			}
		}

		private void AssignSystemTypes(DbModelType dbType)
		{
			switch (dbType.Name.ToLower())
			{
				case "smallint":
					dbType.SystemType = typeof(byte);
					break;

				case "int":
					dbType.SystemType = typeof(int);
					break;

				case "tinyint":
					dbType.SystemType = typeof(byte);
					break;

				case "bigint":
					dbType.SystemType = typeof(long);
					break;

				case "float":
					dbType.SystemType = typeof(double); // yes, float is double ;-)
					break;

				case "numeric":
				case "money":
				case "real":
					dbType.SystemType = typeof(decimal);
					break;

				case "bit":
					dbType.SystemType = typeof(bool);
					break;

				case "uniqueidentifier":
					dbType.SystemType = typeof(Guid);
					break;

				case "nvarchar":
				case "nchar":
				case "ntext":
					dbType.SystemType = typeof(string);
					break;

				case "datetime":
					dbType.SystemType = typeof(DateTime);
					break;

				case "varbinary":
				case "binary":
				case "image":
				case "rowversion":
					dbType.SystemType = typeof(byte[]);
					break;
			}
		}
	}
}