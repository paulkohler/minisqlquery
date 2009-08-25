using System;
using MiniSqlQuery.Core.Template;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests.Templates
{
	[TestFixture]
	public class TextFormater_tests
	{
		private class MyClass
		{
			public string Name{get;set;}
			public DateTime Time { get; set; }
			public int Age { get; set; }
		}

		ITextFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new HenriFormatter();
		}

		[Test]
		public void Unchanged()
		{
			string text = _formatter.Format("nothing", null);
			Assert.That(text, Is.EqualTo("nothing"));
		}

		[Test]
		public void Accepts_values()
		{
			MyClass o = new MyClass { Name = "Blue", Age = 32 };
			string text = _formatter.Format("Mr {Name} arrived, aged {Age}.", o);
			Assert.That(text, Is.EqualTo("Mr Blue arrived, aged 32."));
		}
	}
}