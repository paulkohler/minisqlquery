#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Data.Common;
using MiniSqlQuery.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests
{
	[TestFixture(Description = "Requires AdventureWorks DB")]
	[Category("Functional")]
	public class QueryRunnerTests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetUp()
		{
			_runner = QueryRunner.Create(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn, true);
		}

		#endregion

		private string _conn = @"Server=.; Database=AdventureWorks; Integrated Security=SSPI";
		private QueryRunner _runner;

		[Test]
		public void Check_defaults()
		{
			Assert.That(_runner.Messages, Is.Empty);
			Assert.That(_runner.Batch, Is.Null);
			Assert.That(_runner.IsBusy, Is.EqualTo(false));
		}

		[Test]
		public void Run_a_basic_query()
		{
			string sql = "SELECT EmployeeID, BirthDate FROM HumanResources.Employee";
			_runner.ExecuteQuery(sql);
			
			Assert.That(_runner.Batch, Is.Not.Null);
			Assert.That(_runner.Batch.Queries.Count, Is.EqualTo(1));
			Assert.That(_runner.Batch.Queries[0].Result, Is.Not.Null);
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("EmployeeID"));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("BirthDate"));
		}

		[Test]
		public void Run_a_batched_query()
		{
			string sql = @"
				-- batch 1, 2 queries
				SELECT EmployeeID, BirthDate FROM HumanResources.Employee
				SELECT c.ContactID, c.Title FROM   Person.Contact c
				GO

				-- batch 2, 1 query
				SELECT c.ContactID AS id, c.Title, c.FirstName, c.LastName, c.EmailAddress, c.Phone, e.BirthDate
				FROM   Person.Contact c INNER JOIN HumanResources.Employee e ON c.ContactID = e.ContactID
				WHERE  e.BirthDate >= '1 jan 1975' ";
			_runner.ExecuteQuery(sql);
			
			Assert.That(_runner.Batch, Is.Not.Null);
			Assert.That(_runner.Batch.Queries.Count, Is.EqualTo(2));
			Assert.That(_runner.Batch.Queries[0].Result, Is.Not.Null);

			Assert.That(_runner.Batch.Queries[0].Result.Tables.Count, Is.EqualTo(2));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("EmployeeID"));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("BirthDate"));

			Assert.That(_runner.Batch.Queries[1].Result.Tables.Count, Is.EqualTo(1));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("id"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("Title"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[2].ColumnName, Is.EqualTo("FirstName"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[3].ColumnName, Is.EqualTo("LastName"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[4].ColumnName, Is.EqualTo("EmailAddress"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[5].ColumnName, Is.EqualTo("Phone"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[6].ColumnName, Is.EqualTo("BirthDate"));
		}

		[Test]
		public void Bad_SQL_expects_an_error()
		{
			_runner.ExecuteQuery("SELECT foo FROM bar ");
			Console.WriteLine(_runner.Messages);
			Assert.That(_runner.Messages, Is.Not.Empty);
		}

		[Test]
		public void Execute_a_comment_is_OK()
		{
			_runner.ExecuteQuery("-- should be OK");
			_runner.ExecuteQuery("/* should be OK */");
			_runner.ExecuteQuery(@"/*
				should be OK 
				*/");
		}

		[Test]
		public void Can_PRINT_messages()
		{
			_runner.ExecuteQuery("PRINT 'test data'");
			Assert.That(_runner.Messages, Text.StartsWith("test data"));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Supply a connection.")]
		public void No_connection_expects_error_on_Execute()
		{
			_runner = new QueryRunner(DbProviderFactories.GetFactory("System.Data.SqlClient"), null, true);
			_runner.ExecuteQuery(" ");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Supply a provider.")]
		public void No_provider_expects_error_on_Execute()
		{
			_runner = new QueryRunner(null, _conn, true);
			_runner.ExecuteQuery(" ");
		}
	}

	[TestFixture]
	public class CustomHighlightTests
	{

		[SetUp]
		public void TestSetUp()
		{

		}


		[Test]
		public void test_name()
		{
			
		}
	}	
}