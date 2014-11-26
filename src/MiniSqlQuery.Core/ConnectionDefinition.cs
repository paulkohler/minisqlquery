#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Provides a defition of database connections by provider and name.
	/// </summary>
	[Obsolete]
	public class ConnectionDefinition
	{
		/// <summary>
		/// The character used to "split" the definition text into its components.
		/// </summary>
		public const char SplitChar = '^';

		/// <summary>
		/// The prefix character for comments in the definition text.
		/// </summary>
		public const string CommentPrefix = "#";

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the provider.
		/// </summary>
		/// <value>The name of the provider.</value>
		public string ProviderName { get; set; }

		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		public string ConnectionString { get; set; }

		/// <summary>
		/// A default connection, an MSSQL db on localhost connecting to "master".
		/// </summary>
		public static readonly ConnectionDefinition Default;

		/// <summary>
		/// Initializes the <see cref="ConnectionDefinition"/> class.
		/// </summary>
		static ConnectionDefinition()
		{
			Default = new ConnectionDefinition()
			{
				Name = "Default - MSSQL Master@localhost",
				ProviderName = "System.Data.SqlClient",
				ConnectionString = @"Server=.; Database=Master; Integrated Security=SSPI"
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionDefinition"/> class.
		/// </summary>
		public ConnectionDefinition()
		{
		}

		/// <summary>
		/// Parses the specified <paramref name="definition"/> string.
		/// </summary>
		/// <param name="definition">The definition string, e.g. "Default - MSSQL Master@localhost ^ System.Data.SqlClient ^ Server=.; Database=master; Integrated Security=SSPI".</param>
		/// <returns>A new <see cref="ConnectionDefinition"/> object or null.</returns>
		public static ConnectionDefinition Parse(string definition)
		{
			ConnectionDefinition connDef = null;

			if (string.IsNullOrEmpty(definition) == false)
			{
				if (definition.StartsWith(CommentPrefix) == false)
				{
					string[] parts = definition.Split(new char[] { SplitChar }, StringSplitOptions.RemoveEmptyEntries);
					if (parts != null)
					{
						if (parts.Length == 3)
						{
							connDef = new ConnectionDefinition()
							{
								Name = parts[0].Trim(),
								ProviderName = parts[1].Trim(),
								ConnectionString = parts[2].Trim()
							};
						}
					}
				}
			}

			return connDef;
		}

		/// <summary>
		/// Parses the specified definitions.
		/// </summary>
		/// <param name="definitions">The definitions.</param>
		/// <returns>An array of <see cref="ConnectionDefinition"/> objects.</returns>
		public static ConnectionDefinition[] Parse(string[] definitions)
		{
			List<ConnectionDefinition> conDefs = new List<ConnectionDefinition>();

			if (definitions != null)
			{
				foreach (string definition in definitions)
				{
					ConnectionDefinition conDef = ConnectionDefinition.Parse(definition);
					if (conDef != null)
					{
						conDefs.Add(conDef);
					}
				}
			}

			return conDefs.ToArray();
		}

		/// <summary>
		/// Converts the data to a parsable format.
		/// </summary>
		/// <returns></returns>
		public string ToParsableFormat()
		{
			return string.Concat(Name, SplitChar, ProviderName, SplitChar, ConnectionString);
		}

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
	}
}
