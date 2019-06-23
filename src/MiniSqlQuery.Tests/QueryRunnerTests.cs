#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Data.Common;
using FluentAssertions;
using MiniSqlQuery.Core;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests
{
	[TestFixture(Description = "Requires AdventureWorks DB")]
	[Category("Functional")]
	public class QueryRunnerTests
	{
        /// <summary>
        /// See "AdventureWorks (OLTP) full database backups":
        /// https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks
        /// </summary>
		private string _conn = @"Server=.; Database=AdventureWorks2014; Integrated Security=SSPI";
		private QueryRunner _runner;

		[Test]
		public void Bad_SQL_expects_an_error()
		{
			_runner.ExecuteQuery("SELECT foo FROM bar ");
			Console.WriteLine(_runner.Messages);
			Assert.That(_runner.Messages, Is.Not.Empty);
		}

		[Test]
		public void Can_PRINT_messages()
		{
			_runner.ExecuteQuery("PRINT 'test data'");
			_runner.Messages.Should().StartWith("test data");
		}

		[Test]
		public void Check_defaults()
		{
			Assert.That(_runner.Messages, Is.Empty);
			Assert.That(_runner.Batch, Is.Null);
			Assert.That(_runner.IsBusy, Is.EqualTo(false));
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
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Supply a connection.")]
		public void No_connection_expects_error_on_Execute()
		{
			_runner = new QueryRunner(DbProviderFactories.GetFactory("System.Data.SqlClient"), null, true, 30);
			_runner.ExecuteQuery(" ");
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Supply a provider.")]
		public void No_provider_expects_error_on_Execute()
		{
			_runner = new QueryRunner(null, _conn, true, 30);
			_runner.ExecuteQuery(" ");
		}

		[Test]
		public void Run_a_basic_query()
		{
			string sql = "SELECT BusinessEntityID, BirthDate FROM HumanResources.Employee";
			_runner.ExecuteQuery(sql);

			Assert.That(_runner.Batch, Is.Not.Null);
			Assert.That(_runner.Batch.Queries.Count, Is.EqualTo(1));
			Assert.That(_runner.Batch.Queries[0].Result, Is.Not.Null);
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("BusinessEntityID"));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("BirthDate"));
		}

		[Test]
		public void Run_a_batched_query()
		{
			string sql =
                @"
				-- batch 1, 2 queries
				SELECT BusinessEntityID, BirthDate FROM HumanResources.Employee
				SELECT ct.ContactTypeID, ct.Name   FROM Person.ContactType ct
				GO

				-- batch 2, 1 query
				SELECT p.BusinessEntityID AS id, p.Title, p.FirstName, p.LastName, ea.EmailAddress, pp.PhoneNumber, e.BirthDate
				FROM   Person.Person p
				         INNER JOIN HumanResources.Employee e ON p.BusinessEntityID = e.BusinessEntityID
				         INNER JOIN Person.EmailAddress ea ON p.BusinessEntityID = ea.BusinessEntityID
				         INNER JOIN Person.PersonPhone pp ON p.BusinessEntityID = pp.BusinessEntityID
				WHERE  e.BirthDate >= '1 jan 1975' ";
			_runner.ExecuteQuery(sql);

			Assert.That(_runner.Batch, Is.Not.Null);
			Assert.That(_runner.Batch.Queries.Count, Is.EqualTo(2));
			Assert.That(_runner.Batch.Queries[0].Result, Is.Not.Null);

			Assert.That(_runner.Batch.Queries[0].Result.Tables.Count, Is.EqualTo(2));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("BusinessEntityID"));
			Assert.That(_runner.Batch.Queries[0].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("BirthDate"));

			Assert.That(_runner.Batch.Queries[1].Result.Tables.Count, Is.EqualTo(1));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[0].ColumnName, Is.EqualTo("id"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[1].ColumnName, Is.EqualTo("Title"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[2].ColumnName, Is.EqualTo("FirstName"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[3].ColumnName, Is.EqualTo("LastName"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[4].ColumnName, Is.EqualTo("EmailAddress"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[5].ColumnName, Is.EqualTo("PhoneNumber"));
			Assert.That(_runner.Batch.Queries[1].Result.Tables[0].Columns[6].ColumnName, Is.EqualTo("BirthDate"));
		}

		[SetUp]
		public void TestSetUp()
		{
			_runner = QueryRunner.Create(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn, true, 30);
		}
	}
}