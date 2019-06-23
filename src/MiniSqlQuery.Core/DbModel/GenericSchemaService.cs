#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace MiniSqlQuery.Core.DbModel
{
	/// <summary>
	/// The generic schema service.
	/// </summary>
	public class GenericSchemaService : IDatabaseSchemaService
	{
		/// <summary>The _connection.</summary>
		private string _connection;

		/// <summary>Gets or sets a value indicating whether ForeignKeyInformationAvailable.</summary>
		/// <value>The foreign key information available.</value>
		public bool ForeignKeyInformationAvailable { get; set; }

		/// <summary>Gets or sets ProviderName.</summary>
		/// <value>The provider name.</value>
		public string ProviderName { get; set; }

        /// <summary>Gets a database object model that represents the items defined by the <paramref name="connection"/>.</summary>
        /// <param name="connection">The connection string.</param>
        /// <returns>An instance of <see cref="DbModelInstance"/> describing the database.</returns>
        public virtual DbModelInstance GetDbObjectModel(string connection)
        {

            _connection = connection;

            DbModelInstance model = new DbModelInstance();
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

            try
            {
                using (DbConnection dbConn = factory.CreateConnection())
                {
                    dbConn.ConnectionString = connection;
                    dbConn.Open();

                    DataTable tables = dbConn.GetSchema("Tables");
                    Dictionary<string, DbModelType> dbTypes = GetDbTypes(dbConn);
                    model.Types = dbTypes;
                    model.ProviderName = ProviderName;
                    model.ConnectionString = _connection;

                    if (tables == null)
                    {
                        return model;
                    }
                    
                    DataView tablesDV = new DataView(tables, "TABLE_TYPE='TABLE' OR TABLE_TYPE='BASE TABLE'", "TABLE_SCHEMA, TABLE_NAME", DataViewRowState.CurrentRows);

                    foreach (DataRowView row in tablesDV)
                    {
                        string schemaName = SafeGetString(row.Row, "TABLE_SCHEMA");
                        string tableName = SafeGetString(row.Row, "TABLE_NAME");

                        DbModelTable dbTable = new DbModelTable { Schema = schemaName, Name = tableName };
                        model.Add(dbTable);

                        DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);
                        GetColumnsForTable(dbTable, schemaTableKeyInfo, dbTypes);
                    }

                    DataView viewsDV = new DataView(tables, "TABLE_TYPE='VIEW'", "TABLE_SCHEMA, TABLE_NAME", DataViewRowState.CurrentRows);
                    foreach (DataRowView row in viewsDV)
                    {
                        string schemaName = SafeGetString(row.Row, "TABLE_SCHEMA");
                        string tableName = SafeGetString(row.Row, "TABLE_NAME");

                        DbModelView dbTable = new DbModelView { Schema = schemaName, Name = tableName };
                        model.Add(dbTable);

                        DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);
                        GetColumnsForTable(dbTable, schemaTableKeyInfo, dbTypes);
                    }

                    // build FK relationships 
                    if (model.Tables != null)
                    {
                        foreach (DbModelTable table in model.Tables)
                        {
                            GetForeignKeyReferencesForTable(dbConn, table);
                            ProcessForeignKeyReferencesForTable(dbConn, table);
                        }
                    }

                    // build FK relationships
                    if (model.Views != null)
                    {
                        foreach (DbModelView view in model.Views)
                        {
                            GetForeignKeyReferencesForTable(dbConn, view);
                            ProcessForeignKeyReferencesForTable(dbConn, view);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // catch all for providers that are not implementing the schema info.
                return model;
            }

            return model;
        }

		/// <summary>The get db types.</summary>
		/// <param name="connection">The connection.</param>
		/// <returns>A dictionary of named <see cref="DbModelType"/> objects supported by the database.</returns>
		/// <exception cref="ArgumentNullException">If the <paramref name="connection"/> is null.</exception>
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

                // Some providers may double up on schema info, check first:
                if (!dbTypes.ContainsKey(typeName.ToLower()))
                {
                    dbTypes.Add(typeName.ToLower(), dbType);

                    dbType.CreateFormat = SafeGetString(row, "CreateFormat");
                    dbType.CreateParameters = SafeGetString(row, "CreateParameters");
                    dbType.LiteralPrefix = SafeGetString(row, "LiteralPrefix");
                    dbType.LiteralSuffix = SafeGetString(row, "LiteralSuffix");
                    dbType.SystemType = Type.GetType(SafeGetString(row, "DataType"));
                    dbType.ProviderDbType = SafeGetString(row, "ProviderDbType");
                }
			}

			return dbTypes;
		}

		/// <returns>The get description of the database.</returns>
		public string GetDescription()
		{
			return "todo";
		}

		/// <summary>The get columns for table.</summary>
		/// <param name="dbTable">The db table.</param>
		/// <param name="schemaTableKeyInfo">The schema table key info.</param>
		/// <param name="dbTypes">The db types.</param>
		protected virtual void GetColumnsForTable(DbModelTable dbTable, DataTable schemaTableKeyInfo, Dictionary<string, DbModelType> dbTypes)
		{
			if (schemaTableKeyInfo == null)
			{
				return;
			}

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
// Name = MakeSqlFriendly(columnName),
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

		/// <summary>The get data type name for column.</summary>
		/// <param name="dbTable">The db table.</param>
		/// <param name="schemaTableKeyInfo">The schema table key info.</param>
		/// <param name="columnRow">The column row.</param>
		/// <returns>The get data type name for column.</returns>
		protected virtual string GetDataTypeNameForColumn(DbModelTable dbTable, DataTable schemaTableKeyInfo, DataRow columnRow)
		{
			return SafeGetString(columnRow, "DataTypeName");
		}

		/// <summary>The get foreign key references for table.</summary>
		/// <param name="dbConn">The db conn.</param>
		/// <param name="dbTable">The db table.</param>
		protected virtual void GetForeignKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
			// foreach (DbModelColumn column in dbTable.Columns)
			// {
			// // KF info for DB's varies widley, needs to be implemented by derived class
			// column.ForeignKeyReference = DbModelForeignKeyReference.NullForeignKeyReference;
			// }
		}

		/// <summary>The get table key information.</summary>
		/// <param name="dbConn">The database connection.</param>
		/// <param name="schema">The schema.</param>
		/// <param name="name">The name of the table.</param>
		/// <returns>A <see cref="DataTable"/> describing the tables columns and key information.</returns>
		protected virtual DataTable GetTableKeyInfo(DbConnection dbConn, string schema, string name)
		{
			DataTable schemaTableKeyInfo = null;
			try
			{
				using (DbCommand command = dbConn.CreateCommand())
				{
					string tableName = Utility.RenderSafeSchemaObjectName(schema, name);
					command.CommandText = "SELECT * FROM " + MakeSqlFriendly(tableName);
					using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
					{
						schemaTableKeyInfo = reader.GetSchemaTable();
					}
				}
			}
			catch (Exception exp)
			{
                // Generic catch all - not all provoders stick to the DbException base:
				Debug.WriteLine(GetType().FullName + " ERROR: " + exp.Message);
			}

			return schemaTableKeyInfo;
		}

		/// <summary>The make sql friendly.</summary>
		/// <param name="name">The name.</param>
		/// <returns>The make sql friendly.</returns>
		protected virtual string MakeSqlFriendly(string name)
		{
			return Utility.MakeSqlFriendly(name);
		}

		/// <summary>The process foreign key references for table.</summary>
		/// <param name="dbConn">The db conn.</param>
		/// <param name="dbTable">The db table.</param>
		protected virtual void ProcessForeignKeyReferencesForTable(DbConnection dbConn, DbModelTable dbTable)
		{
		}


		/// <summary>The safe get bool.</summary>
		/// <param name="row">The row.</param>
		/// <param name="columnName">The column name.</param>
		/// <returns>The safe get bool.</returns>
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

		/// <summary>The safe get int.</summary>
		/// <param name="row">The row.</param>
		/// <param name="columnName">The column name.</param>
		/// <returns>The safe get int.</returns>
		protected virtual int SafeGetInt(DataRow row, string columnName)
		{
            try
            {
                int result = -1;

                if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
                {
                    result = Convert.ToInt32(row[columnName]);
                }

                return result;
            }
            catch (OverflowException)
            {
                // In Oracle Maximum size for column is larger than Max Int32, instead of changing return value, just coerce on Max.Int32.

                return Int32.MaxValue;
                
            }

		}

		/// <summary>The safe get string.</summary>
		/// <param name="row">The row.</param>
		/// <param name="columnName">The column name.</param>
		/// <returns>The safe get string.</returns>
		protected string SafeGetString(DataRow row, string columnName)
		{
			string result = string.Empty;

			if (row.Table.Columns.Contains(columnName) && !row.IsNull(columnName))
			{
				result = row[columnName].ToString();
			}

			return result;
		}
	}
}