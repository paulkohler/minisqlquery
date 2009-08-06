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
			Console.WriteLine(Environment.CurrentDirectory);
			_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn);
			//_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SQLite"), _sqliteConn);
		}

		#endregion

		private string _conn = @"Server=.\sqlexpress; Database=AdventureWorks; Integrated Security=SSPI";
		private string _sqliteConn = @"Data Source=..\..\..\TestDatabases\sqlite.db3";
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
			/*
    <TypeName>nvarchar</TypeName>
    <ProviderDbType>16</ProviderDbType>
    <ColumnSize>2147483647</ColumnSize>
    <CreateFormat>nvarchar({0})</CreateFormat>
    <CreateParameters>max length</CreateParameters>
    <DataType>System.String</DataType>
    <IsAutoIncrementable>false</IsAutoIncrementable>
    <IsBestMatch>true</IsBestMatch>
    <IsCaseSensitive>false</IsCaseSensitive>
    <IsFixedLength>false</IsFixedLength>
    <IsFixedPrecisionScale>false</IsFixedPrecisionScale>
    <IsLong>false</IsLong>
    <IsNullable>true</IsNullable>
    <IsSearchable>true</IsSearchable>
    <IsSearchableWithLike>true</IsSearchableWithLike>
    <LiteralPrefix>'</LiteralPrefix>
    <LiteralSuffix>'</LiteralSuffix>
                <xs:element name="TypeName" type="xs:string" minOccurs="0" />
                <xs:element name="ProviderDbType" type="xs:int" minOccurs="0" />
                <xs:element name="ColumnSize" type="xs:long" minOccurs="0" />
                <xs:element name="CreateFormat" type="xs:string" minOccurs="0" />
                <xs:element name="CreateParameters" type="xs:string" minOccurs="0" />
                <xs:element name="DataType" type="xs:string" minOccurs="0" />
                <xs:element name="IsAutoIncrementable" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsBestMatch" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsCaseSensitive" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsFixedLength" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsFixedPrecisionScale" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsLong" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsNullable" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsSearchable" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsSearchableWithLike" type="xs:boolean" minOccurs="0" />
                <xs:element name="IsLiteralSupported" type="xs:boolean" minOccurs="0" />
                <xs:element name="LiteralPrefix" type="xs:string" minOccurs="0" />
                <xs:element name="LiteralSuffix" type="xs:string" minOccurs="0" />
                <xs:element name="IsUnsigned" type="xs:boolean" minOccurs="0" />
                <xs:element name="MaximumScale" type="xs:short" minOccurs="0" />
                <xs:element name="MinimumScale" type="xs:short" minOccurs="0" />
                <xs:element name="IsConcurrencyType" type="xs:boolean" minOccurs="0" />
*/
		}

		[Test]
		public void view_tree()
		{
			DbModelInstance model = _dataService.GetDbObjectModel();

			Console.WriteLine(model.Tables.Count);
			foreach (DbModelTable table in model.Tables)
			{
				Console.WriteLine(table.Name);
				foreach (DbModelColumn column in table.Columns)
				{
					Console.WriteLine("  {0} [{1}]", column.Name, column.DbType.Summary);
				}
			}
		}
	}
}