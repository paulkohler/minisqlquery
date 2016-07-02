#region License
// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;


// ReSharper disable InconsistentNaming

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
			Assert.That(_model.FindTable("Customers"), Is.Not.Null);
		}

		[Test]
		public void Has_Categories_table()
		{
			Assert.That(_model.FindTable("Categories"), Is.Not.Null);
		}

		[Test]
		public void Customers_table_has_columns()
		{
			var table = _model.FindTable("Customers");
			Assert.That(table.Columns[0].Name, Is.EqualTo("Customer ID"));
			Assert.That(table.Columns[0].DbType.Name, Is.EqualTo("nvarchar"));
			Assert.That(table.Columns[0].DbType.Length, Is.EqualTo(5));

			Assert.That(table.PrimaryKeyColumns.Count, Is.EqualTo(1));
		}

		[Test]
		public void Check_FK_refs_for_Products()
		{
			var table = _model.FindTable("Products");
			var supplierIdColumn = table.Columns[1];
			var categoryIdColumn = table.Columns[2];
			var productNameColumn = table.Columns[3];

			Assert.That(table.Columns.Count, Is.EqualTo(11));
			Assert.That(table.PrimaryKeyColumns.Count, Is.EqualTo(1));
			Assert.That(table.ForeignKeyColumns.Count, Is.EqualTo(2));
			Assert.That(table.NonKeyColumns.Count, Is.EqualTo(8));

			Assert.That(supplierIdColumn.ForeignKeyReference, Is.Not.Null);
			Assert.That(categoryIdColumn.ForeignKeyReference, Is.Not.Null);
			
			Assert.That(supplierIdColumn.ForeignKeyReference.OwningColumn.Name, Is.EqualTo("Supplier ID"));
			Assert.That(supplierIdColumn.ForeignKeyReference.ReferenceTable.Name, Is.EqualTo("Suppliers"));
			Assert.That(supplierIdColumn.ForeignKeyReference.ReferenceColumn.Name, Is.EqualTo("Supplier ID"));
			Assert.That(supplierIdColumn.ForeignKeyReference.UpdateRule, Is.EqualTo("CASCADE"));
			Assert.That(supplierIdColumn.ForeignKeyReference.DeleteRule, Is.EqualTo("NO ACTION"));
			Assert.That(supplierIdColumn.HasFK, Is.True);

			Assert.That(categoryIdColumn.ForeignKeyReference.OwningColumn.Name, Is.EqualTo("Category ID"));
			Assert.That(categoryIdColumn.ForeignKeyReference.ReferenceTable.Name, Is.EqualTo("Categories"));
			Assert.That(categoryIdColumn.ForeignKeyReference.ReferenceColumn.Name, Is.EqualTo("Category ID"));
			Assert.That(categoryIdColumn.ForeignKeyReference.UpdateRule, Is.EqualTo("CASCADE"));
			Assert.That(categoryIdColumn.ForeignKeyReference.DeleteRule, Is.EqualTo("NO ACTION"));

			Assert.That(productNameColumn.HasFK, Is.False);
			Assert.That(productNameColumn.Name, Is.EqualTo("Product Name"));
		}


		[Test]
		public void Build_dependency_tree_from_model()
		{
			DbModelDependencyWalker dependencyWalker = new DbModelDependencyWalker(_model);
			var tables = dependencyWalker.SortTablesByForeignKeyReferences();

			DisplayTableDetails(tables);
			
			Assert.That(tables[0].Name, Is.EqualTo("Categories"));
			Assert.That(tables[1].Name, Is.EqualTo("Customers"));
			Assert.That(tables[2].Name, Is.EqualTo("Employees"));
			Assert.That(tables[3].Name, Is.EqualTo("Shippers"));
			Assert.That(tables[4].Name, Is.EqualTo("Suppliers"));
			Assert.That(tables[5].Name, Is.EqualTo("Orders"), "Order is dependent on Customers, Employees, Shippers");
			Assert.That(tables[6].Name, Is.EqualTo("Products"), "Products is dependent on Suppliers, Categories");
			Assert.That(tables[7].Name, Is.EqualTo("Order Details"), "Order Details is dependent on Orders, Products");
		}

		private void DisplayTableDetails(DbModelTable[] tablesList)
		{
			foreach (DbModelTable table in tablesList)
			{
				Console.WriteLine(string.Format("{0} (fks:{1})", table.Name, table.ForeignKeyColumns.Count));
				if (table.ForeignKeyColumns.Count > 0)
				{
					foreach (DbModelColumn fk in table.ForeignKeyColumns)
					{
						Console.WriteLine("  (FK --> {0}.{1})", fk.ForeignKeyReference.ReferenceTable.Name, fk.ForeignKeyReference.ReferenceColumn.Name);
				
					}
				}
			}
		}
	}
}