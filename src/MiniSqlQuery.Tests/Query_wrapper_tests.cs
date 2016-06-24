#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using MiniSqlQuery.Core;
using NUnit.Framework;


namespace MiniSqlQuery.Tests
{
	[TestFixture]
	public class Query_wrapper_tests
	{
		[Test]
		public void A_query_with_GOs_produces_multiple_Query_objects()
		{
			var batch = QueryBatch.Parse(@"-- test query...

select 1


GO 
select foo
from bar

	go

	insert into table 1 (name, desc)
	values('a name like gogo', 'i want to GO now.')

	GO

select 3
GO
GO
GO
GO
");
			Assert.That(batch.Queries.Count, Is.EqualTo(4), "Empty queries should be ignored");
			Assert.That(batch.Queries[0].Sql, Is.EqualTo(@"-- test query...

select 1"));
			Assert.That(batch.Queries[1].Sql, Is.EqualTo("select foo\r\nfrom bar"));
			Assert.That(batch.Queries[2].Sql, Is.EqualTo(@"insert into table 1 (name, desc)
	values('a name like gogo', 'i want to GO now.')"));
			Assert.That(batch.Queries[3].Sql, Is.EqualTo("select 3"));
		}

		[Test]
		public void No_single_query_produces_empty_batch()
		{
			var batch = QueryBatch.Parse("");
			Assert.That(batch.Queries.Count, Is.EqualTo(0));
		}

		[Test(Description ="Need Sql Query Parser"), Explicit("Needs a fair bit of work to pass...")]
		public void Batch_indicators_on_a_line_alone_cause_issues()
		{
			var batch = QueryBatch.Parse(@"-- issue...
insert into table 1 (name, desc)
values('foo', 'if the
go
is on a line by itself we have a problem...')");
			Assert.That(batch.Queries.Count, Is.EqualTo(1), "This fails for now...");

			// Basically there is no easy workaround short of writing an actual SQL 
			// query parser... allow 'turn off batches' will do...

			// A good comprimise would token parse taking string (and escaping) into account
			// but even that would add up...
		}

		[Test]
		public void A_single_query_produces_1_batch_with_1_Query()
		{
			var batch = QueryBatch.Parse("select 1");
			Assert.That(batch.Queries.Count, Is.EqualTo(1));
		}
	}
}