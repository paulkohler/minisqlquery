using System;
using System.IO;
using System.Xml.Serialization;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Provides a defition of database connection by provider and name.
	/// </summary>
	[Serializable]
	public class DbConnectionDefinition
	{
		/// <summary>
		/// A default connection, an MSSQL db on localhost connecting to "master".
		/// </summary>
		[XmlIgnore] public static readonly DbConnectionDefinition Default;

		/// <summary>
		/// Initializes the <see cref="DbConnectionDefinition"/> class.
		/// </summary>
		static DbConnectionDefinition()
		{
			Default = new DbConnectionDefinition
			          {
			          	Name = "Default - MSSQL Master@localhost",
			          	ProviderName = "System.Data.SqlClient",
			          	ConnectionString = @"Server=.; Database=Master; Integrated Security=SSPI"
			          };
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[XmlElement(IsNullable = false)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the provider.
		/// </summary>
		/// <value>The name of the provider.</value>
		[XmlElement(IsNullable = false)]
		public string ProviderName { get; set; }

		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		[XmlElement(IsNullable = false)]
		public string ConnectionString { get; set; }

		/// <summary>
		/// Gets or sets a comment in relation to this connection, e.g. "development..."
		/// </summary>
		/// <value>A comment.</value>
		[XmlElement(IsNullable = true)]
		public string Comment { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			return Name ?? GetType().FullName;
		}

		/// <summary>
		/// Serialize the object to XML.
		/// </summary>
		/// <returns>An XML string.</returns>
		public string ToXml()
		{
			return Utility.ToXml(this);
		}

		public static DbConnectionDefinition FromXml(string xml)
		{
			using (StringReader sr = new StringReader(xml))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(DbConnectionDefinition));
				return (DbConnectionDefinition) serializer.Deserialize(sr);
			}
		}
	}
}