#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using MiniSqlQuery.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace MiniSqlQuery.Tests
{
	[TestFixture]
	public class DbConnectionDefinitionListTests
	{
		[Test]
		public void Can_add_DbConnectionDefinition_object()
		{
			DbConnectionDefinitionList definitionList = new DbConnectionDefinitionList();
			definitionList.DefaultName = "def";
			definitionList.Definitions = new[]
			                             {
			                             	new DbConnectionDefinition {ConnectionString = "cs1", Name = "nm1", ProviderName = "p1"},
			                             	new DbConnectionDefinition {ConnectionString = "cs2", Name = "nm2", ProviderName = "p2"}
			                             };
			Assert.That(definitionList.Definitions.Length, Is.EqualTo(2));
			definitionList.AddDefinition(new DbConnectionDefinition {ConnectionString = "cs3", Name = "nm3", ProviderName = "p3"});
			Assert.That(definitionList.Definitions.Length, Is.EqualTo(3));
		}

		[Test]
		public void Can_Deserialize_a_DbConnectionDefinitionList_from_XML_string()
		{
			string xml =
				@"<DbConnectionDefinitionList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <DefaultName>default</DefaultName>
  <Definitions>
    <DbConnectionDefinition>
      <Name>nm1</Name>
      <ConnectionString>cs1</ConnectionString>
    </DbConnectionDefinition>
    <DbConnectionDefinition>
      <Name>nm2</Name>
      <ConnectionString>cs2</ConnectionString>
    </DbConnectionDefinition>
  </Definitions>
</DbConnectionDefinitionList>";

			DbConnectionDefinitionList definitionList = DbConnectionDefinitionList.FromXml(xml);
			Assert.That(definitionList.DefaultName, Is.EqualTo("default"));
			Assert.That(definitionList.Definitions[0].Name, Is.EqualTo("nm1"));
			Assert.That(definitionList.Definitions[0].ConnectionString, Is.EqualTo("cs1"));
			Assert.That(definitionList.Definitions[1].Name, Is.EqualTo("nm2"));
		}

		[Test]
		public void Can_remove_a_DbConnectionDefinition_object()
		{
			DbConnectionDefinitionList definitionList = new DbConnectionDefinitionList();
			definitionList.DefaultName = "def";
			var conn1 = new DbConnectionDefinition {ConnectionString = "cs1", Name = "nm1", ProviderName = "p1"};
			var conn2 = new DbConnectionDefinition {ConnectionString = "cs2", Name = "nm2", ProviderName = "p2"};
			var conn3unused = new DbConnectionDefinition {ConnectionString = "cs3", Name = "nm3", ProviderName = "p3"};
			definitionList.Definitions = new[] {conn1, conn2};
			Assert.That(definitionList.Definitions.Length, Is.EqualTo(2));
			bool remove1=definitionList.RemoveDefinition(conn2);
			Assert.That(remove1, Is.EqualTo(true));
			Assert.That(definitionList.Definitions.Length, Is.EqualTo(1));
			Assert.That(definitionList.Definitions[0].Name, Is.EqualTo("nm1"));
			bool remove2 = definitionList.RemoveDefinition(conn3unused);
			Assert.That(remove2, Is.EqualTo(false));
			Assert.That(definitionList.Definitions.Length, Is.EqualTo(1));
		}

		[Test]
		public void Can_serialize_a_DbConnectionDefinitionList()
		{
			DbConnectionDefinitionList definitionList = new DbConnectionDefinitionList();
			definitionList.DefaultName = "def";
			definitionList.Definitions = new[]
			                             {
			                             	new DbConnectionDefinition {ConnectionString = "cs1", Name = "nm1", ProviderName = "p1"},
			                             	new DbConnectionDefinition {ConnectionString = "cs2", Name = "nm2", ProviderName = "p2"}
			                             };
			string xml = definitionList.ToXml();
			//Console.WriteLine(xml);
			Assert.That(xml, Text.Contains("<DefaultName>def</DefaultName>"));
			Assert.That(xml, Text.Contains("<Name>nm1</Name>"));
			Assert.That(xml, Text.Contains("<ConnectionString>cs1</ConnectionString>"));
			Assert.That(xml, Text.Contains("<ProviderName>p1</ProviderName>"));
			Assert.That(xml, Text.Contains("<Name>nm2</Name>"));
			Assert.That(xml, Text.Contains("<ConnectionString>cs2</ConnectionString>"));
			Assert.That(xml, Text.Contains("<ProviderName>p2</ProviderName>"));
		}
	}
}