#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	Manages a list of database connections.
    /// </summary>
    [Serializable]
    public class DbConnectionDefinitionList
    {
        // store internally as a list
        /// <summary>
        /// 	An class refernece to the database definitions.
        /// </summary>
        private readonly List<DbConnectionDefinition> _definitions;

        /// <summary>
        /// 	Initializes a new instance of the <see cref = "DbConnectionDefinitionList" /> class.
        /// </summary>
        public DbConnectionDefinitionList()
        {
            _definitions = new List<DbConnectionDefinition>();
        }

        /// <summary>
        /// 	Gets or sets the default name of a connection definition from the list of <see cref = "Definitions" />.
        /// </summary>
        /// <value>The default name.</value>
        public string DefaultName { get; set; }

        /// <summary>
        /// 	Gets or sets the connection definitions.
        /// </summary>
        /// <value>The definitions.</value>
        public DbConnectionDefinition[] Definitions
        {
            get
            {
                return _definitions.ToArray();
            }
            set
            {
                _definitions.Clear();
                _definitions.AddRange(value);
            }
        }

        /// <summary>
        /// 	Creates a <see cref = "DbConnectionDefinitionList" /> from a string of <paramref name = "xml" />.
        /// </summary>
        /// <param name = "xml">The XML to hydrate from.</param>
        /// <returns>An instance of <see cref = "DbConnectionDefinitionList" />.</returns>
        public static DbConnectionDefinitionList FromXml(string xml)
        {
            using (var sr = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(DbConnectionDefinitionList));
                return (DbConnectionDefinitionList)serializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// 	Adds the definition from the list.
        /// </summary>
        /// <param name = "connectionDefinition">The connection definition.</param>
        public void AddDefinition(DbConnectionDefinition connectionDefinition)
        {
            _definitions.Add(connectionDefinition);
        }

        /// <summary>
        /// 	Removes the definition from the list.
        /// </summary>
        /// <param name = "connectionDefinition">The connection definition.</param>
        /// <returns>True if the item was removed.</returns>
        public bool RemoveDefinition(DbConnectionDefinition connectionDefinition)
        {
            return _definitions.Remove(connectionDefinition);
        }

        /// <summary>
        /// 	Serialize the list to XML.
        /// </summary>
        /// <returns>An XML string.</returns>
        public string ToXml()
        {
            return Utility.ToXml(this);
        }
    }
}