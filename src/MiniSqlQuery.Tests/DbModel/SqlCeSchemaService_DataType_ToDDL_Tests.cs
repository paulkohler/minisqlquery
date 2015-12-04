#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System.Collections.Generic;
using System.Data.Common;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;


namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires SQLCE DB", Category = "Functional, SqlCeSchemaService")]
	public class SqlCeSchemaService_DataType_ToDDL_Tests
	{
		#region Setup/Teardown
		private const string _connStr = @"data source=|DataDirectory|\sqlce-test.sdf";
		private const string _providerName = "System.Data.SqlServerCe.3.5";

		[SetUp]
		public void TestSetup()
		{
			using (DbConnection conn = DbProviderFactories.GetFactory(_providerName).CreateConnection())
			{
				conn.ConnectionString = _connStr;
				conn.Open();
				var service = new SqlCeSchemaService();
				_dbTypes = service.GetDbTypes(conn);
			}
		}

		#endregion

		private static Dictionary<string, DbModelType> _dbTypes;

		[Test]
		public void nvarchar_with_value()
		{
			DbModelType str = _dbTypes["nvarchar"].Copy();
			str.Value = "the quick brown fox...";
			Assert.That(str.ToDDLValue(), Is.EqualTo("N'the quick brown fox...'"), "Should take the literals into account");
		}

		[Test]
		public void int_with_value()
		{
			DbModelType num = _dbTypes["int"].Copy();
			num.Value = 55;
			Assert.That(num.ToDDLValue(), Is.EqualTo("55"));
		}

		[Test]
		public void datetime_as_null()
		{
			DbModelType nullDate = _dbTypes["datetime"].Copy();
			nullDate.Value = null;
			Assert.That(nullDate.ToDDLValue(), Is.EqualTo("null"));
		}

		[Test]
		public void nou_null_int_defaults_to_0()
		{
			DbModelType notNullNum = _dbTypes["int"].Copy();
			notNullNum.Value = null;
			Assert.That(notNullNum.ToDDLValue(false), Is.EqualTo("0"));
		}

		[Test]
		public void not_null_nvarchar_defaults_to_empty_quotes()
		{
			DbModelType notNullNvarchar = _dbTypes["nvarchar"].Copy();
			notNullNvarchar.Value = null;
			Assert.That(notNullNvarchar.ToDDLValue(false), Is.EqualTo("N''"));
		}

		[Test]
		public void nou_null_guid_defaults_to_empty_guid()
		{
			DbModelType uniqueidentifier = _dbTypes["uniqueidentifier"].Copy();
			uniqueidentifier.Value = null;
			Assert.That(uniqueidentifier.ToDDLValue(false), Is.EqualTo("'00000000-0000-0000-0000-000000000000'"));
		}

		[Test]
		public void nou_null_VARBINARY_defaults_to_0()
		{
			DbModelType notNullVarbinary = _dbTypes["varbinary"].Copy();
			notNullVarbinary.Value = null;
			Assert.That(notNullVarbinary.ToDDLValue(false), Is.EqualTo("0"));
		}

		[Test]
		public void nou_null_BIT_defaults_to_0()
		{
			DbModelType notNullBit = _dbTypes["bit"].Copy();
			notNullBit.Value = null;
			Assert.That(notNullBit.ToDDLValue(false), Is.EqualTo("0"));
		}
	}
}