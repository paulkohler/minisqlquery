#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using MiniSqlQuery.Core.DbModel;
using MiniSqlQuery.Core.Template;
using NUnit.Framework;


namespace MiniSqlQuery.Tests.Templates
{
	[TestFixture]
	public class TextFormater_tests
	{
		#region Setup/Teardown

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new NVelocityWrapper();
		}

		#endregion

		private class MyClass
		{
			public string Name { get; set; }
			public DateTime Time { get; set; }
			public int Age { get; set; }
		}

		private ITextFormatter _formatter;

		public class Something
		{
			private string firstName = "hammett";
			private string middleNameInitial = "V";

			public string FirstName
			{
				get { return firstName; }
				set { firstName = value; }
			}

			public string MiddleNameInitial
			{
				get { return middleNameInitial; }
				set { middleNameInitial = value; }
			}

			public String Print(String arg)
			{
				return arg;
			}

			public String Contents(params String[] args)
			{
				return String.Join(",", args);
			}
		}

		[Test]
		public void Accepts_values()
		{
			MyClass o = new MyClass {Name = "Blue", Age = 32};
			Dictionary<string, object> items = new Dictionary<string, object>();
			items.Add("data", o);
			string text = _formatter.Format("Mr $data.Name arrived, aged $data.Age.", items);
			Assert.That(text, Is.EqualTo("Mr Blue arrived, aged 32."));
		}

		[Test]
		public void nvelocity_with_dbmodel2()
		{
			DbModelInstance model = new DbModelInstance();
			model.ConnectionString = "conn str";
			model.ProviderName = "sql.foo";
			DbModelTable table = new DbModelTable {Name = "MyTable"};
			model.Add(table);
			table.Add(new DbModelColumn {Name = "ID"});
			table.Add(new DbModelColumn {Name = "FirstName"});

			Dictionary<string, object> items = new Dictionary<string, object>();
			items.Add("model", model);

			string template =
				@"Template Test ($num):
ConnectionString: ""$model.ConnectionString""
ProviderName: ""$model.ProviderName""

#foreach ($table in $model.Tables)
$table.Name
#foreach ($c in $table.Columns)
  * $c.Name
#end
#end
";
			string s = _formatter.Format(template, items);

			Console.WriteLine(s);
			Assert.That(s.Length, Is.GreaterThan(0));
		}

		[Test]
		public void Unchanged()
		{
			string text = _formatter.Format("nothing", null);
			Assert.That(text, Is.EqualTo("nothing"));
		}
	}
}