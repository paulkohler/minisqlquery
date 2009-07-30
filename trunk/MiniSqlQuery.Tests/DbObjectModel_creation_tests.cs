using System;
using System.Data.Common;
using MiniSqlQuery.Core;
using NUnit.Framework;

namespace MiniSqlQuery.Tests
{
	[TestFixture(Description = "Requires AdventureWorks DB")]
	[Category("Functional")]
	public class DbObjectModel_creation_tests
	{
		private string _conn = @"Server=.\sqlexpress; Database=AdventureWorks; Integrated Security=SSPI";
		DatabaseMetaDataService _dataService;

		[SetUp]
		public void TestSetup()
		{
			_dataService = new DatabaseMetaDataService(DbProviderFactories.GetFactory("System.Data.SqlClient"), _conn);
		}

		[Test]
		public void view_tree()
		{
			DbObjectModel model = _dataService.GetDbObjectModel();

			Console.WriteLine(model.Tables.Count);
			foreach (DbTable table in model.Tables)
			{
				Console.WriteLine(table);
				foreach (DbColumn column in table.Columns)
				{
					Console.WriteLine("  " + column);
				}
			}
		}

	}
}