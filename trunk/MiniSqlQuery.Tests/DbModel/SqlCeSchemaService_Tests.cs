using System;
using System.Linq;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires SQLCE DB")]
	[Category("Functional")]
	public class SqlCeSchemaService_Tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			_service = new SqlCeSchemaService {ProviderName = _providerName};
			_model = _service.GetDbObjectModel(_connStr);
		}

		#endregion

		private SqlCeSchemaService _service;
		private string _connStr = @"data source=|DataDirectory|\sqlce-test.sdf";
		private string _providerName = "System.Data.SqlServerCe.3.5";
		private DbModelInstance _model;

		[Test]
		public void Has_Person_table()
		{
			Assert.That(_model.Tables.Count(t => t.Name == "Person"), Is.EqualTo(1));
		}

		[Test]
		public void Has_StaffUnit_table()
		{
			Assert.That(_model.Tables.Count(t => t.Name == "StaffUnit"), Is.EqualTo(1));
		}

		[Test]
		public void Person_table_has_columns()
		{
			var table = _model.Tables.First(t => t.Name == "Person");
			Assert.That(table.Columns[0].Name, Is.EqualTo("ID"));
			Assert.That(table.Columns[0].DbType.Name, Is.EqualTo("int"));
		}
	}
}