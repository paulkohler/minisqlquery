#region License
// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion
using System;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;


// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.DbModel
{
    /// <summary>
    /// Microsoft SQL Server Compact 4.0 tests.
    /// 
    /// To run this fixture you will need SQLCE installed, see:
    ///   https://www.microsoft.com/en-us/download/details.aspx?id=17876
    /// </summary>
	[TestFixture(Description = "Requires SQLCE DB")]
	[Category("Functional")] // todo - build db model manually for unit test to remove db dependency
	public class DbModelTable_tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			_service = new SqlCeSchemaService { ProviderName = _providerName };
			_model = _service.GetDbObjectModel(_connStr);
		}

		#endregion

		private SqlCeSchemaService _service;
		private string _connStr = @"data source=|DataDirectory|\sqlce-test.v4.sdf";
		private string _providerName = "System.Data.SqlServerCe.4.0";

        private DbModelInstance _model;

		[Test]
		public void Can_find_tables_by_name()
		{
			var table = _model.FindTable("[StaffUnit]");
			Assert.That(table, Is.Not.Null);
		}

		[Test]
		public void Returns_null_if_cant_find_table_by_name()
		{
			var table = _model.FindTable("[foo]");
			Assert.That(table, Is.Null);
		}
	}
}