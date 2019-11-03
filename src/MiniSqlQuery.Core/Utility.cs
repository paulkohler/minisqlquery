#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using MiniSqlQuery.Core.Properties;

namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	Some basic helper functions.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 	Writes a default file if none present.
        /// </summary>
        public static void CreateConnectionStringsIfRequired()
        {
            string filename = GetConnectionStringFilename();
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, Resources.DefaultConnectionDefinitionFile);
            }
        }

        /// <summary>
        /// 	Resolves the full filename of the connection string file, by default in the application data folder
        /// 	for "MiniSqlQuery", e.g. "C:\Users\(username)\AppData\Roaming\MiniSqlQuery\connections.xml".
        /// 	Allows for the override vis the "MiniSqlQuery.Core.dll.config" file "DefaultConnectionDefinitionFilename" 
        /// 	setting.
        /// </summary>
        /// <returns>A filename.</returns>
        public static string GetConnectionStringFilename()
        {
            string filename = ApplicationServices.Instance.Settings.DefaultConnectionDefinitionFilename;

            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                return filename;
            }

            string folder = GetAppFolderPath();
            filename = Path.Combine(folder, "connections.xml");

            return filename;
        }

        /// <summary>
        /// 	Returns an array of SQL provider types supported by the current platform.
        /// </summary>
        /// <returns>An array of SQL provider types.</returns>
        public static string[] GetSqlProviderNames()
        {
            DataTable providers = DbProviderFactories.GetFactoryClasses();
            var providerNames = new List<string>();

            foreach (DataRow row in providers.Rows)
            {
                providerNames.Add(row["InvariantName"].ToString());
            }

            return providerNames.ToArray();
        }

        /// <summary>
        /// 	Loads the connection string data from the file.
        /// </summary>
        /// <returns>The text file contents as a single string.</returns>
        /// <seealso cref = "GetConnectionStringFilename" />
        public static string LoadConnections()
        {
            string filename = GetConnectionStringFilename();
            string data = File.ReadAllText(filename);
            return data;
        }

        /// <summary>
        /// 	Loads the db connection definitions from an XML file.
        /// </summary>
        /// <returns>A <see cref = "DbConnectionDefinitionList" /> instance or null if the file does not exist.</returns>
        public static DbConnectionDefinitionList LoadDbConnectionDefinitions()
        {
            string filename = GetConnectionStringFilename();
            DbConnectionDefinitionList definitionList = null;

            if (File.Exists(filename))
            {
                definitionList = DbConnectionDefinitionList.FromXml(File.ReadAllText(filename));
            }

            return definitionList;
        }

        /// <summary>
        /// 	Attempts to convert a database object name to it's "bracketed for", e.g. "Name" -> "[Name]".
        /// </summary>
        /// <param name = "name">The name of the object.</param>
        /// <returns>The SQL friendly conversion.</returns>
        public static string MakeSqlFriendly(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }

            if (!name.StartsWith("[") && (name.Contains(" ") || name.Contains("$")))
            {
                // TODO - reserved wods?
                return string.Concat("[", name, "]");
            }

            return name;
        }

        /// <summary>
        /// 	The render safe schema object name.
        /// </summary>
        /// <param name = "schema">The schema.</param>
        /// <param name = "objectName">The object name.</param>
        /// <returns>The render safe schema object name.</returns>
        public static string RenderSafeSchemaObjectName(string schema, string objectName)
        {
            if (string.IsNullOrEmpty(schema))
            {
                return string.Format("[{0}]", objectName);
            }

            return string.Format("[{0}].[{1}]", schema, objectName);
        }

        /// <summary>
        /// 	Saves the <paramref name = "definitionList" /> to the connection string file.
        /// </summary>
        /// <param name = "definitionList">The contents of the file.</param>
        /// <seealso cref = "GetConnectionStringFilename" />
        public static void SaveConnections(DbConnectionDefinitionList definitionList)
        {
            string filename = GetConnectionStringFilename();
            string newXml = definitionList.ToXml();
            File.WriteAllText(filename, newXml);
        }

        /// <summary>
        /// 	Shows the URL in the defaut browser.
        /// </summary>
        /// <param name = "url">The URL to display.</param>
        public static void ShowUrl(string url)
        {
            Process.Start(url);
        }

        /// <summary>
        /// 	Serializes the <paramref name = "obj" />.
        /// </summary>
        /// <typeparam name = "T">The type.</typeparam>
        /// <param name = "obj">The object to serialize.</param>
        /// <returns>A UTF8 XML string representing <paramref name = "obj" />.</returns>
        public static string ToXml<T>(T obj)
        {
            using (StringWriter sw = new StringWriterWithEncoding(Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 	Resolves the "(application data path)\MiniSqlQuery" for this user.
        /// </summary>
        /// <returns>A folder path.</returns>
        private static string GetAppFolderPath()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Resources.ShortAppTitle);
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }
    }
}