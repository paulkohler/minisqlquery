#region License
// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;

using System.Data.Common;

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires SQLCE DB")]
	[Category("Functional")]
	public class SqlCeSchemaService_DataType_Tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			_service = new SqlCeSchemaService();
			DbConnection conn = DbProviderFactories.GetFactory(_providerName).CreateConnection();
			conn.ConnectionString = _connStr;
			conn.Open();
			_dbTypes = _service.GetDbTypes(conn);
		}

		#endregion

		private SqlCeSchemaService _service;
		private string _connStr  = @"data source=|DataDirectory|\sqlce-test.sdf";
		private string _providerName = "System.Data.SqlServerCe.3.5";
		Dictionary<string, DbModelType> _dbTypes;

		[Test]
		public void There_are_at_least_18_types()
		{
			Assert.That(_dbTypes.Count, Is.GreaterThanOrEqualTo(18));
		}

		[Test]
		public void NVarchar_type()
		{
			DbModelType dbType = _dbTypes["nvarchar"];
			Assert.That(dbType.Name, Is.EqualTo("nvarchar").IgnoreCase);
			Assert.That(dbType.Length, Is.EqualTo(4000));
			Assert.That(dbType.CreateFormat, Is.EqualTo("nvarchar({0})"));
			Assert.That(dbType.LiteralPrefix, Is.EqualTo("N'"));
			Assert.That(dbType.LiteralSuffix, Is.EqualTo("'"));
			// todo Assert.That(dbType.SystemType, Is.EqualTo(typeof(string)));
		}

		[Test, Explicit]
		public void Decimal_type()
		{
			DbModelType dbType = _dbTypes["decimal"];
			Assert.That(dbType.Name, Is.EqualTo("decimal").IgnoreCase);
			Assert.That(dbType.CreateFormat, Is.EqualTo("decimal({0}, {1})"));
			// todo Assert.That(dbType.SystemType, Is.EqualTo(typeof(decimal)));
		}

		[Test, Explicit]
		public void Show_all()
		{
			List<DbModelType> ary = new List<DbModelType>(_dbTypes.Values);
			foreach (var dbModelType in ary)
			{
				Console.WriteLine("{0}  ({1})", dbModelType.Summary, dbModelType.SystemType);
			}
		}
	}
}