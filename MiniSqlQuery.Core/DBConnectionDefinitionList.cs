using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MiniSqlQuery.Core
{
	///<summary>
	///</summary>
	[Serializable]
	public class DbConnectionDefinitionList
	{
		// store internally as a list
		private List<DbConnectionDefinition> _definitions;

		public DbConnectionDefinitionList()
		{
			_definitions = new List<DbConnectionDefinition>();
		}

		public DbConnectionDefinition[] Definitions
		{
			get { return _definitions.ToArray(); }
			set
			{
				_definitions.Clear();
				_definitions.AddRange(value);
			}
		}

		public string DefaultName { get; set; }

		public static DbConnectionDefinitionList FromXml(string xml)
		{
			using (StringReader sr = new StringReader(xml))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(DbConnectionDefinitionList));
				return (DbConnectionDefinitionList) serializer.Deserialize(sr);
			}
		}

		/// <summary>
		/// Serialize the object to XML.
		/// </summary>
		/// <returns>An XML string.</returns>
		public string ToXml()
		{
			return Utility.ToXml(this);
		}

		public static DbConnectionDefinitionList Upgrade(ConnectionDefinition[] oldDefinitions, string defaultName)
		{
			DbConnectionDefinitionList definitionList = new DbConnectionDefinitionList();
			List<DbConnectionDefinition> newDefList = new List<DbConnectionDefinition>();

			definitionList.DefaultName = defaultName;
			foreach (ConnectionDefinition oldConDef in oldDefinitions)
			{
				DbConnectionDefinition dbConnectionDefinition = new DbConnectionDefinition
				                                                {
				                                                	ConnectionString = oldConDef.ConnectionString,
				                                                	Name = oldConDef.Name,
				                                                	ProviderName = oldConDef.ProviderName,
				                                                };
				newDefList.Add(dbConnectionDefinition);
			}
			definitionList.Definitions = newDefList.ToArray();

			if (definitionList.DefaultName == null && definitionList.Definitions.Length > 0)
			{
				definitionList.DefaultName = definitionList.Definitions[0].Name;
			}

			return definitionList;
		}

		public void AddDefinition(DbConnectionDefinition connectionDefinition)
		{
			_definitions.Add(connectionDefinition);
		}

		public bool RemoveDefinition(DbConnectionDefinition connectionDefinition)
		{
			return _definitions.Remove(connectionDefinition);
		}
	}
}