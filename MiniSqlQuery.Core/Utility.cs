using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
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
		/// Loads the connection definition file as an array of strings (can contain blanks and comments).
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// <para>
		/// Below is the default example of a connection definition file 
		/// (See <see cref="Properties.Resources.DefaultConnectionDefinitionFile"/>):
		/// </para>
		/// <code>
		/// # Connection Definition file
		/// # This file is intentionally plain text, thats nice and simple  :)
		/// # Blank and Hashed (#) lines are ignored by the application. See more details at end of file.
		/// 
		/// Default - MSSQL Master@localhost   ^ System.Data.SqlClient ^ Server=.; Database=master; Integrated Security=SSPI
		/// Sample MSSQL Northwind SQL Express ^ System.Data.SqlClient ^ Server=.\sqlexpress; Database=Northwind; Integrated Security=SSPI
		/// Sample Access DB Connection        ^ System.Data.OleDb     ^ Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\SomeDirectory\access.mdb
		/// 
		/// ######################################################################################################
		/// #
		/// # The format of a "connection definition" is "&lt;Friendly Name&gt;^&lt;Provider Name&gt;^&lt;Connection String&gt;"
		/// # You can have whitespace around the "^" to aid readability etc.
		/// # Typical providers:
		/// #   System.Data.SqlClient
		/// #   System.Data.Oracle
		/// #   System.Data.OleDb
		/// #   System.Data.Odbc
		/// #
		/// </code>
		/// </remarks>
		public static string[] LoadConnectionDefinitionStrings()
		{
			string[] lines = new string[0] { };
			string filename = GetConnectionStringFilename();

			if (File.Exists(filename))
			{
				lines = File.ReadAllLines(filename);
			}
			else
			{
				Debug.WriteLine("no file: " + filename);
			}

			return lines;
		}

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
		/// Saves the <paramref name="data"/> to the connection string file.
		/// </summary>
		/// <param name="data">The contents of the file.</param>
		/// <seealso cref="GetConnectionStringFilename"/>
		public static void SaveConnections(string data)
		{
			string filename = GetConnectionStringFilename();
			File.WriteAllText(filename, data);
		}

		public static void SaveConnectionXml(string xml)
		{
			string filename = GetConnectionStringFilename();
			File.WriteAllText(filename, xml);
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
			string filename = Properties.Settings.Default.DefaultConnectionDefinitionFilename;

			if (string.IsNullOrEmpty(filename))
			{
				string folder = GetAppFolderPath();
				filename = Path.Combine(folder, "connections.xml");
			}

			return filename;
		}

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
						string.Format("The file '{0}' was upgraded to XML and saved as '{1}'.", oldFilename, filename),
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
