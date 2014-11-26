#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.IO;
using System.Linq;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;


// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.DbModel
{
	[TestFixture(Description = "Requires SQLCE DB")]
	[Category("Functional")] // todo - build db model manually for unit test to remove db dependency
	public class SqlWriterTests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetup()
		{
			_service = new SqlCeSchemaService {ProviderName = _providerName};
			_model = _service.GetDbObjectModel(_connStr);
			_sqlSW = new StringWriter();
			_sqlWriter = new SqlWriter();
		}

		#endregion

		private SqlCeSchemaService _service;
		private string _connStr = @"data source=|DataDirectory|\sqlce-test.sdf";
		private string _providerName = "System.Data.SqlServerCe.3.5";
		private DbModelInstance _model;
		ISqlWriter _sqlWriter;
		StringWriter _sqlSW;

		[Test]
		public void will_render_sql_with_types_and_nullability()
		{
			var table = _model.FindTable("Person");
			// build sql sample...
			_sqlWriter.WriteCreate(_sqlSW, table.Columns[0]);
			_sqlSW.WriteLine(",");
			_sqlWriter.WriteCreate(_sqlSW, table.Columns[2]);

			Assert.That(_sqlSW.ToString(), Is.EqualTo("ID int not null,\r\nName nvarchar(100) not null"));
		}

		[Test]
		public void will_render_sql_WriteSummary()
		{
			var table = _model.FindTable("Person");

			_sqlWriter.WriteSummary(_sqlSW, table.Columns[0]);
			Assert.That(_sqlSW.ToString(), Is.EqualTo("ID (int not null)"));

			_sqlSW = new StringWriter();
			_sqlWriter.WriteSummary(_sqlSW, table.Columns[2]);
			Assert.That(_sqlSW.ToString(), Is.EqualTo("Name (nvarchar(100) not null)"));
		}

		[Test]
		public void will_render_sql_select_for_Person()
		{
			var table = _model.FindTable("Person");
			_sqlWriter.WriteSelect(_sqlSW, table);
			Assert.That(_sqlSW.ToString(), Is.EqualTo(@"SELECT
	ID,
	StaffUnitID,
	Name,
	DOB
FROM Person
"));
		}

		[Test]
		public void will_render_sql_select_COUNT_for_Person()
		{
			var table = _model.FindTable("Person");
			_sqlWriter.WriteSelectCount(_sqlSW, table);
			Assert.That(_sqlSW.ToString().Replace(Environment.NewLine, ""), Is.EqualTo(@"SELECT COUNT(*) FROM Person"));
		}

		[Test]
		public void will_render_insert_sql_for_StaffUnit()
		{
			var table = _model.FindTable("StaffUnit");
			_sqlWriter.WriteInsert(_sqlSW, table);

			Console.WriteLine(_sqlSW.ToString());
			Assert.That(_sqlSW.ToString(), Is.EqualTo(@"INSERT INTO StaffUnit
	(Name,
	Description)
VALUES
	(N'' /*Name,nvarchar(100)*/,
	null /*Description,nvarchar(500)*/)
"));
		}


		[Test]
		public void will_render_insert_sql_for_StaffUnit_including_PK()
		{
			_sqlWriter.IncludeReadOnlyColumnsInExport = true;
			
			var table = _model.FindTable("StaffUnit");
			_sqlWriter.WriteInsert(_sqlSW, table);

			Console.WriteLine(_sqlSW.ToString());
			Assert.That(_sqlSW.ToString(), Is.EqualTo(@"INSERT INTO StaffUnit
	(ID,
	Name,
	Description)
VALUES
	(0 /*ID,int*/,
	N'' /*Name,nvarchar(100)*/,
	null /*Description,nvarchar(500)*/)
"));
		}


		[Test]
		public void will_render_update_sql_for_StaffUnit()
		{
			var table = _model.FindTable("StaffUnit");
			_sqlWriter.WriteUpdate(_sqlSW, table);

			Console.WriteLine(_sqlSW.ToString());
			Assert.That(_sqlSW.ToString(), Is.EqualTo(@"UPDATE StaffUnit
SET
	Name = N'',
	Description = null
WHERE
	ID = /*value:ID,int*/
"));
		}

		[Test]
		public void will_render_delete_sql_for_StaffUnit()
		{
			var table = _model.FindTable("StaffUnit");
			_sqlSW = new StringWriter();
			_sqlWriter.WriteDelete(_sqlSW, table);
			//Console.WriteLine(sql.ToString());
			Assert.That(_sqlSW.ToString(), Is.EqualTo(@"DELETE FROM
	StaffUnit
WHERE
	ID = /*value:ID*/
"));

		}
	}
}