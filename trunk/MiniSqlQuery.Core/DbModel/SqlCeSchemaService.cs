using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>
	/// SQL Compact Edition schema service.
	/// Made possible with contributions form ExportSQLCE project.
	/// </summary>
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

				// build all table info
				foreach (DbModelTable table in model.Tables)
				{
					DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, null, table.Name);
					GetColumnsForTable(table, schemaTableKeyInfo, dbTypes);
				}

				// build FK relationships
				foreach (DbModelTable table in model.Tables)
				{
					GetForiegnKeyReferencesForTable(dbConn, table);
					ProcessForiegnKeyReferencesForTable(dbConn, table);
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
						table.Name = (string) reader["table_name"];
						model.Add(table);
					}
				}
			}
		}

		protected override void GetForiegnKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			ForiegnKeyInformationAvailable = true;
			try
			{
				using (var cmd = dbConn.CreateCommand())
				{
					cmd.CommandText =
						string.Format(
							@"SELECT 
	KCU1.TABLE_NAME AS FK_TABLE_NAME,  
	KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME, 
	KCU1.COLUMN_NAME AS FK_COLUMN_NAME,
	KCU2.TABLE_NAME AS UQ_TABLE_NAME, 
	KCU2.CONSTRAINT_NAME AS UQ_CONSTRAINT_NAME, 
	KCU2.COLUMN_NAME AS UQ_COLUMN_NAME, 
	RC.UPDATE_RULE, 
	RC.DELETE_RULE, 
	KCU2.ORDINAL_POSITION AS UQ_ORDINAL_POSITION, 
	KCU1.ORDINAL_POSITION AS FK_ORDINAL_POSITION
FROM 
	INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC 
		JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 ON KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 
			JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 ON  KCU2.CONSTRAINT_NAME =  RC.UNIQUE_CONSTRAINT_NAME AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION AND KCU2.TABLE_NAME = RC.UNIQUE_CONSTRAINT_TABLE_NAME 
WHERE KCU1.TABLE_NAME = '{0}'
ORDER BY 
	FK_TABLE_NAME, 
	FK_CONSTRAINT_NAME, 
	FK_ORDINAL_POSITION
",
							dbTable.Name);
					cmd.CommandType = CommandType.Text;
					using (var dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							dbTable.Constraints.Add(new DbModelConstraint
							                        {
							                        	ConstraintTableName = dr.GetString(0),
							                        	ConstraintName = dr.GetString(1),
							                        	ColumnName = dr.GetString(2),
							                        	UniqueConstraintTableName = dr.GetString(3),
							                        	UniqueConstraintName = dr.GetString(4),
							                        	UniqueColumnName = dr.GetString(5),
							                        	UpdateRule = dr.GetString(6),
							                        	DeleteRule = dr.GetString(7)
							                        });
						}
					}
				}
			}
			catch (DbException)
			{
				ForiegnKeyInformationAvailable = false;
			}
		}

		protected override void ProcessForiegnKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			// todo - check GetGroupForeingKeys
			foreach (DbModelConstraint constraint in dbTable.Constraints)
			{
				var column = dbTable.Columns.Find(c => c.Name == constraint.ColumnName);
				var refTable = dbTable.ParentDb.FindTable(constraint.UniqueConstraintTableName);
				var refColumn = refTable.Columns.Find(c => c.Name == constraint.UniqueColumnName);
				DbModelForiegnKeyReference fk = new DbModelForiegnKeyReference(column, refTable, refColumn);
				fk.UpdateRule = constraint.UpdateRule;
				fk.DeleteRule = constraint.DeleteRule;
				column.ForiegnKeyReference = fk;
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
					dbType.Length = MAX_NTEXT_COLUMN_SIZE;
					break;
				case "binary":
				case "varbinary":
					dbType.Length = MAX_BINARY_COLUMN_SIZE;
					break;
				case "image":
					dbType.Length = MAX_IMAGE_COLUMN_SIZE;
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