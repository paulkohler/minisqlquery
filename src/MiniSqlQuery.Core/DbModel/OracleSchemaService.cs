using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
    public class OracleSchemaService : GenericSchemaService
    {

        protected override int SafeGetInt(DataRow row, string columnName)
        {
            try
            {
                return base.SafeGetInt(row, columnName);
            }
            catch (OverflowException)
            {
                // Coerce to Max.Int32
                return Int32.MaxValue;
            }
        }

        public override DbModelInstance GetDbObjectModel(string connection)
        {

            //_connection = connection;

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
                model.ConnectionString = connection;

                if (tables == null)
                {
                    return model;
                }

                DataView tablesDV = new DataView(tables, "", "OWNER, TABLE_NAME", DataViewRowState.CurrentRows);

                foreach (DataRowView row in tablesDV)
                {
                    string schemaName = SafeGetString(row.Row, "OWNER");
                    string tableName = SafeGetString(row.Row, "TABLE_NAME");

                    DbModelTable dbTable = new DbModelTable { Schema = schemaName, Name = tableName };
                    model.Add(dbTable);

                    DataTable schemaTableKeyInfo = GetTableKeyInfo(dbConn, schemaName, tableName);
                    GetColumnsForTable(dbTable, schemaTableKeyInfo, dbTypes);
                }

                DataTable views = dbConn.GetSchema("Views");
                DataView viewsDV = new DataView(views, "", "OWNER, VIEW_NAME", DataViewRowState.CurrentRows);
                foreach (DataRowView row in viewsDV)
                {
                    string schemaName = SafeGetString(row.Row, "OWNER");
                    string tableName = SafeGetString(row.Row, "VIEW_NAME");

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

            return model;
        }

    }
}
