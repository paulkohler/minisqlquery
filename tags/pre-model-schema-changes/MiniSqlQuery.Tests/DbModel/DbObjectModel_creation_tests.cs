using System;
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires AdventureWorks DB")]
	[Category("Functional")]
	public class DbObjectModel_creation_tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			//_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn);
			_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SQLite"), _sqliteConn);
			//_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SqlServerCe.3.5"), _sqlceConn);
		}

		#endregion

		//private string _conn = @"Server=.\sqlexpress; Database=AdventureWorks; Integrated Security=SSPI";
		private string _conn = @"Server=.; Database=AdventureWorks; Integrated Security=SSPI";
		private string _sqliteConn = @"Data Source=..\..\..\TestDatabases\sqlite.db3";
		private string _sqlceConn = @"data source=|DataDirectory|\sqlce-test.sdf";
		private DatabaseMetaDataService _dataService;

		[Test]
		public void Can_get_all_data_types()
		{
			DbConnection openConnection = _dataService.CreateOpenConnection();

			Dictionary<string, DbModelType> dbTypes = _dataService.GetDbTypes(openConnection);

			//List<DbModelType> ary = new List<DbModelType>(dbTypes.Values);

			Console.WriteLine(dbTypes.Count);
			DbModelType nvarcharType = dbTypes["nvarchar"];
			Assert.That(nvarcharType.Name, Is.EqualTo("nvarchar"));
			Assert.That(nvarcharType.Length, Is.GreaterThan(1024*1024)); // i.e. its big
			Assert.That(nvarcharType.CreateFormat, Is.EqualTo("nvarchar({0})"));
			Assert.That(nvarcharType.CreateParameters, SyntaxHelper.Not.Null);
			Assert.That(nvarcharType.LiteralPrefix, SyntaxHelper.Not.Null);
			Assert.That(nvarcharType.LiteralSuffix, SyntaxHelper.Not.Null);
			Assert.That(nvarcharType.SystemType, Is.EqualTo(typeof(string)));
		}

		[Test]
		public void view_tree()
		{
			DbModelInstance model = _dataService.GetDbObjectModel();

			Assert.That(model.ProviderName, Is.Not.Null);
			Assert.That(model.ConnectionString, Is.Not.Null);


			Console.WriteLine(model.Tables.Count);
			foreach (DbModelTable table in model.Tables)
			{
				Console.WriteLine(table.Name);
				foreach (DbModelColumn column in table.Columns)
				{
					Console.WriteLine("  {0} - type: {1}, null: {2}", column.Name, column.DbType.Summary, column.Nullable);
				}
			}
		}
	}
}