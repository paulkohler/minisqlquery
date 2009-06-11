using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MiniSqlQuery.Core
{
	///<summary>
	///</summary>
	[Serializable]
	public class DbConnectionDefinitionList
	{
		public DbConnectionDefinition[] Definitions { get; set; }

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

			if (definitionList.DefaultName == null && definitionList.Definitions.Length>0)
			{
				definitionList.DefaultName = definitionList.Definitions[0].Name;
			}

			return definitionList;
		}
	}
}