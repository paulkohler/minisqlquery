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
		private string _connStr = @"data source=|DataDirectory|\Northwind.sdf";
		private string _providerName = "System.Data.SqlServerCe.3.5";
		private DbModelInstance _model;

		[Test]
		public void Has_Customers_table()
		{
			Assert.That(_model.Tables.Count(t => t.Name == "Customers"), Is.EqualTo(1));
		}

		[Test]
		public void Has_Categories_table()
		{
			Assert.That(_model.Tables.Count(t => t.Name == "Categories"), Is.EqualTo(1));
		}

		[Test]
		public void Customers_table_has_columns()
		{
			var table = _model.Tables.First(t => t.Name == "Customers");
			Assert.That(table.Columns[0].Name, Is.EqualTo("Customer ID"));
			Assert.That(table.Columns[0].DbType.Name, Is.EqualTo("nvarchar"));
			Assert.That(table.Columns[0].DbType.Length, Is.EqualTo(5));

			Assert.That(table.PrimaryKeyColumns.Count, Is.EqualTo(1));
		}

		[Test]
		public void Check_FK_refs_for_Products()
		{
			var table = _model.Tables.First(t => t.Name == "Products");
			var supplierIdColumn = table.Columns[1];
			var categoryIdColumn = table.Columns[2];
			var productNameColumn = table.Columns[3];

			Assert.That(table.Columns.Count, Is.EqualTo(11));
			Assert.That(table.PrimaryKeyColumns.Count, Is.EqualTo(1));
			Assert.That(table.ForiegnKeyColumns.Count, Is.EqualTo(2));
			Assert.That(table.NonKeyColumns.Count, Is.EqualTo(8));

			Assert.That(supplierIdColumn.ForiegnKeyReference, Is.Not.Null);
			Assert.That(categoryIdColumn.ForiegnKeyReference, Is.Not.Null);
			
			Assert.That(supplierIdColumn.ForiegnKeyReference.OwningColumn.Name, Is.EqualTo("Supplier ID"));
			Assert.That(supplierIdColumn.ForiegnKeyReference.ReferenceTable.Name, Is.EqualTo("Suppliers"));
			Assert.That(supplierIdColumn.ForiegnKeyReference.ReferenceColumn.Name, Is.EqualTo("Supplier ID"));
			Assert.That(supplierIdColumn.ForiegnKeyReference.UpdateRule, Is.EqualTo("CASCADE"));
			Assert.That(supplierIdColumn.ForiegnKeyReference.DeleteRule, Is.EqualTo("NO ACTION"));
			Assert.That(supplierIdColumn.HasFK, Is.True);

			Assert.That(categoryIdColumn.ForiegnKeyReference.OwningColumn.Name, Is.EqualTo("Category ID"));
			Assert.That(categoryIdColumn.ForiegnKeyReference.ReferenceTable.Name, Is.EqualTo("Categories"));
			Assert.That(categoryIdColumn.ForiegnKeyReference.ReferenceColumn.Name, Is.EqualTo("Category ID"));
			Assert.That(categoryIdColumn.ForiegnKeyReference.UpdateRule, Is.EqualTo("CASCADE"));
			Assert.That(categoryIdColumn.ForiegnKeyReference.DeleteRule, Is.EqualTo("NO ACTION"));

			Assert.That(productNameColumn.HasFK, Is.False);
			Assert.That(productNameColumn.Name, Is.EqualTo("Product Name"));
		}
	}
}