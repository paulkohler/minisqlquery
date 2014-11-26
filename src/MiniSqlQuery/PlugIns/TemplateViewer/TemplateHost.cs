#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Globalization;
using System.Text;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	/// <summary>The template host.</summary>
	public class TemplateHost : IDisposable
	{
		/// <summary>The _database inspector.</summary>
		private readonly IDatabaseInspector _databaseInspector;

		/// <summary>Initializes a new instance of the <see cref="TemplateHost"/> class.</summary>
		/// <param name="applicationServices">The application services.</param>
		/// <param name="databaseInspector">The database inspector.</param>
		public TemplateHost(IApplicationServices applicationServices, IDatabaseInspector databaseInspector)
		{
			Services = applicationServices;
			_databaseInspector = databaseInspector;
		}

		/// <summary>Gets or sets Data.</summary>
		public TemplateData Data { get; set; }

		/// <summary>Gets MachineName.</summary>
		public string MachineName
		{
			get { return Environment.MachineName; }
		}

		/// <summary>Gets Model.</summary>
		public DbModelInstance Model
		{
			get
			{
				if (_databaseInspector.DbSchema == null)
				{
					_databaseInspector.LoadDatabaseDetails();
				}

				return _databaseInspector.DbSchema;
			}
		}

		/// <summary>Gets Services.</summary>
		public IApplicationServices Services { get; private set; }

		/// <summary>Gets UserName.</summary>
		public string UserName
		{
			get { return Environment.UserName; }
		}

		// to do - helper functions for changing names too code friendly etc

		/// <summary>The date.</summary>
		/// <param name="format">The format.</param>
		/// <returns>The date.</returns>
		public string Date(string format)
		{
			return DateTime.Now.ToString(format);
		}

		/// <summary>The to camel case.</summary>
		/// <param name="text">The text.</param>
		/// <returns>The to camel case.</returns>
		public string ToCamelCase(string text)
		{
			if (text == null)
			{
				return string.Empty;
			}

			StringBuilder sb = new StringBuilder();
			text = ToPascalCase(text);

			for (int i = 0; i < text.Length; i++)
			{
				if (Char.IsUpper(text, i))
				{
					sb.Append(Char.ToLower(text[i]));
				}
				else
				{
					// allows for names that start with an acronym, e.g. "ABCCode" -> "abcCode"
					if (i > 1)
					{
						i--; // reverse one
						sb.Remove(i, 1); // drop last lower cased char
					}

					sb.Append(text.Substring(i));
					break;
				}
			}

			return sb.ToString();
		}

		/// <summary>The to pascal case.</summary>
		/// <param name="text">The text.</param>
		/// <returns>The to pascal case.</returns>
		public string ToPascalCase(string text)
		{
			if (text == null)
			{
				return string.Empty;
			}

			if (text.Contains(" ") || text.Contains("_"))
			{
				text = text.Replace("_", " ");
				text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
				text = text.Replace(" ", string.Empty);
			}
			else if (text.Length > 1)
			{
				text = char.ToUpper(text[0]) + text.Substring(1);
			}

			return text;
		}

		/// <summary>The dispose.</summary>
		public void Dispose()
		{
			if (Data != null)
			{
				Data.Dispose();
			}
		}
	}
}