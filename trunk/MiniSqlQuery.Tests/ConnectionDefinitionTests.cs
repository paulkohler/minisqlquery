using MiniSqlQuery.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests
{
	[TestFixture]
	public class ConnectionDefinitionTests
	{
		string sampleConnectionDefinition = "Connection Name^Provider.Namespace^Server=.; Database=master; Integrated Security=SSPI";
		ConnectionDefinition connDef;

		[SetUp]
		public void TestSetUp()
		{
			connDef = ConnectionDefinition.Parse(sampleConnectionDefinition);
		}

		[Test]
		public void Check_that_connection_strings_parse()
		{
			Assert.That(connDef.Name, Is.EqualTo("Connection Name"));
			Assert.That(connDef.ProviderName, Is.EqualTo("Provider.Namespace"));
			Assert.That(connDef.ConnectionString, Is.EqualTo("Server=.; Database=master; Integrated Security=SSPI"));
		}

		[Test]
		public void Parsing_a_hashed_connection_string_yields_null()
		{
			ConnectionDefinition connDef = ConnectionDefinition.Parse("#" + sampleConnectionDefinition);
			Assert.That(connDef, Is.Null);
		}

		[Test]
		public void Parsing_empty_and_null_strings_yield_null()
		{
			string nullString = null;
			Assert.That(ConnectionDefinition.Parse(nullString), Is.Null);
			Assert.That(ConnectionDefinition.Parse(""), Is.Null);
		}

		[Test]
		public void Calling_ToString_yields_the_connection_name()
		{
			Assert.That(connDef.ToString(), Is.EqualTo("Connection Name"));
		}

		[Test]
		public void Can_parse_an_array_of_connection_definitions()
		{
			string[] lines = new string[]
			{
				"Connection1^sql^Server=.; Database=master; Integrated Security=SSPI",
				"Connection2^oracle^Server=.; Database=fred; user=bob; password=p"
			};

			ConnectionDefinition[] connDefs = ConnectionDefinition.Parse(lines);

			Assert.That(connDefs.Length, Is.EqualTo(2));
			Assert.That(connDefs[0].Name, Is.EqualTo("Connection1"));
			Assert.That(connDefs[1].Name, Is.EqualTo("Connection2"));
		}


		[Test]
		public void Calling_ToParsableFormat_creates_a_parsable_string()
		{
			connDef.Name = "the name";
			connDef.ProviderName = "the provider";
			connDef.ConnectionString = "the connection string";

			string connDefStr = connDef.ToParsableFormat();
			Assert.That(connDefStr, Is.EqualTo("the name^the provider^the connection string"));

			ConnectionDefinition newConnDef = ConnectionDefinition.Parse(connDefStr);
			Assert.That(connDef.Name, Is.EqualTo("the name"));
			Assert.That(connDef.ProviderName, Is.EqualTo("the provider"));
			Assert.That(connDef.ConnectionString, Is.EqualTo("the connection string"));
		}
	}
}
