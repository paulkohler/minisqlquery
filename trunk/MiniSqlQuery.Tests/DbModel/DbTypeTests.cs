using System;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture]
	public class DbTypeTests
	{
		[Test]
		public void Summary_renders_an_int()
		{
			DbModelType dbType = new DbModelType("int", 4) { CreateFormat = "int" };
			Assert.That(dbType.Summary, Is.EqualTo("int"));
		}

		[Test]
		public void Summary_renders_a_varchar_10()
		{
			DbModelType dbType = new DbModelType("varchar", 10) { CreateFormat = "varchar({0})" };
			Assert.That(dbType.Summary, Is.EqualTo("varchar(10)"));
		}

		[Test]
		public void Summary_renders_a_decimal_8_6()
		{
			DbModelType dbType = new DbModelType("decimal", 8+6) { Precision = 8, Scale = 6, CreateFormat = "decimal({0}, {1})" };
			Assert.That(dbType.Summary, Is.EqualTo("decimal(8, 6)"));
		}

		[Test]
		public void Summary_renders_a_decimal_as_default()
		{
			DbModelType dbType = new DbModelType("decimal", 26) { Precision = -1, Scale = -1, CreateFormat = "decimal({0}, {1})" };
			Assert.That(dbType.Summary, Is.EqualTo("decimal"));
		}

		[Test]
		public void Summary_renders_uses_CreateFormat_over_name_if_present()
		{
			DbModelType dbType = new DbModelType("mytype", 100) { CreateFormat = "MyType" };
			Assert.That(dbType.Summary, Is.EqualTo("MyType"));
		}

		[Test]
		public void Summary_renders_a_the_type_name_if_there_is_no_CreateFormat()
		{
			DbModelType dbType = new DbModelType("decimal", 26);
			Assert.That(dbType.Summary, Is.EqualTo("decimal"));
		}

		/*
		 * consider nvarchar(MAX)
		 * 
BillOfMaterials
  BillOfMaterialsID [int]
  ProductAssemblyID [int]
  ComponentID [int]
  StartDate [datetime]
  EndDate [datetime]
  UnitMeasureCode [nchar(3)]
  BOMLevel [smallint]
  PerAssemblyQty [decimal(-1, -1)]
  ModifiedDate [datetime]
		 * */
	}
}