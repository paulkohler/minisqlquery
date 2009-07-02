using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using MiniSqlQuery.Core.Properties;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Some basic helper functions.
	/// </summary>
	public static class Utility
	{
        /// <summary>
        /// Shows the URL in the defaut browser.
        /// </summary>
        /// <param name="url">The URL.</param>
        public static void ShowUrl(string url)
        {
            Process.Start(url);
        }

	    /// <summary>
		/// Returns an array of SQL provider types supported by the current platform.
		/// </summary>
		/// <returns>An array of SQL provider types.</returns>
		public static string[] GetSqlProviderNames()
		{
			DataTable providers = DbProviderFactories.GetFactoryClasses();
			List<string> providerNames = new List<string>();
			
			foreach (DataRow row in providers.Rows)
			{
				providerNames.Add(row["InvariantName"].ToString());
			}

			return providerNames.ToArray();
		}

		/// <summary>
		/// Loads the db connection definitions from an XML file.
		/// </summary>
		/// <returns>A <see cref="DbConnectionDefinitionList"/> instance or null if the file does not exist.</returns>
		public static DbConnectionDefinitionList LoadDbConnectionDefinitions()
		{
			string filename = GetConnectionStringFilename();
			DbConnectionDefinitionList definitionList=null;

			if (File.Exists(filename))
			{
				definitionList = DbConnectionDefinitionList.FromXml(File.ReadAllText(filename));
			}

			return definitionList;
		}

		/// <summary>
		/// Loads the connection string data from the file.
		/// </summary>
		/// <returns>The text file contents as a single string.</returns>
		/// <seealso cref="GetConnectionStringFilename"/>
		public static string LoadConnections()
		{
			string filename = GetConnectionStringFilename();
			string data = File.ReadAllText(filename);
			return data;
		}

		/// <summary>
		/// Saves the <paramref name="definitionList"/> to the connection string file.
		/// </summary>
		/// <param name="definitionList">The contents of the file.</param>
		/// <seealso cref="GetConnectionStringFilename"/>
		public static void SaveConnections(DbConnectionDefinitionList definitionList)
		{
			string filename = GetConnectionStringFilename();
			File.WriteAllText(filename, definitionList.ToXml());
		}

		/// <summary>
		/// Resolves the full filename of the connection string file, by default in the application data folder
		/// for "MiniSqlQuery", e.g. "C:\Users\(username)\AppData\Roaming\MiniSqlQuery\connections.txt".
		/// Allows for the override vis the "MiniSqlQuery.Core.dll.config" file "DefaultConnectionDefinitionFilename" 
		/// setting.
		/// </summary>
		/// <returns>A filename.</returns>
		public static string GetConnectionStringFilename()
		{
			string filename = Settings.Default.DefaultConnectionDefinitionFilename;

			if (string.IsNullOrEmpty(filename))
			{
				string folder = GetAppFolderPath();
				filename = Path.Combine(folder, "connections.xml");
			}

			return filename;
		}

		/// <summary>
		/// Gets the old (text) connection string filename.
		/// </summary>
		/// <returns></returns>
		public static string GetOldConnectionStringFilename()
		{
			string folder = GetAppFolderPath();
			string filename = Path.Combine(folder, "connections.txt");
			return filename;
		}

		/// <summary>
		/// Resolves the "(application data path)\MiniSqlQuery" for this user.
		/// </summary>
		/// <returns>A folder path.</returns>
		private static string GetAppFolderPath()
		{
			string folder = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Properties.Resources.ShortAppTitle);
			Debug.WriteLine(folder);
			if (Directory.Exists(folder) == false)
			{
				Debug.WriteLine("creating " + folder);
				Directory.CreateDirectory(folder);
			}
			return folder;
		}

		/// <summary>
		/// Writes a default file if none present.
		/// </summary>
		public static void CreateConnectionStringsIfRequired()
		{
			DbConnectionDefinitionList newDefinitionList = null;
			string oldFilename = GetOldConnectionStringFilename();
			if (File.Exists(oldFilename))
			{
				// offer migrate
				string[] oldLines = File.ReadAllLines(oldFilename);
				ConnectionDefinition[] oldConnectionDefinitions = ConnectionDefinition.Parse(oldLines);
				newDefinitionList = DbConnectionDefinitionList.Upgrade(oldConnectionDefinitions, null);

				string migratedFilename = oldFilename + ".migrated-to-xml-file";
				File.Move(oldFilename, migratedFilename);
			}

			string filename = GetConnectionStringFilename();
			if (!File.Exists(filename))
			{
				if (newDefinitionList != null)
				{
					File.WriteAllText(filename, newDefinitionList.ToXml());
					ApplicationServices.Instance.HostWindow.DisplaySimpleMessageBox(
						null,
						string.Format("The file\r\n'{0}'\r\nwas upgraded to XML and saved as\r\n'{1}'", oldFilename, filename),
						"Migrated TEXT connections file to XML");
				}
				else
				{
					File.WriteAllText(filename, Resources.DefaultConnectionDefinitionFile);
				}
			}
		}

		/// <summary>
		/// Serializes the <paramref name="obj"/>.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>A UTF8 XML string representing <paramref name="obj"/>.</returns>
		public static string ToXml<T>(T obj)
		{
			using (StringWriter sw = new StringWriterWithEncoding(Encoding.UTF8))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(sw, obj);
				return sw.ToString();
			}
		}
	}
}
