using System;
using System.Data.Common;
using MiniSqlQuery.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
			_runner = new QueryRunner(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn, true);
		}

		#endregion

		private string _conn = @"Server=.\sqlexpress; Database=AdventureWorks; Integrated Security=SSPI";
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
	
}