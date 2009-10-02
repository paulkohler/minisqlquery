using System;
using System.Globalization;
using System.Text;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateHost : IDisposable
	{
		private readonly IDatabaseInspector _databaseInspector;

		public TemplateHost(IApplicationServices applicationServices, IDatabaseInspector databaseInspector)
		{
			Services = applicationServices;
			_databaseInspector = databaseInspector;
		}

		public IApplicationServices Services { get; private set; }

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

		public TemplateData Data { get; set; }

		public string MachineName
		{
			get { return Environment.MachineName; }
		}

		public string UserName
		{
			get { return Environment.UserName; }
		}

		// to do - helper functions for changing names too code friendly etc

		#region IDisposable Members

		public void Dispose()
		{
			if (Data != null)
			{
				Data.Dispose();
			}
		}

		#endregion

		public string Date(string format)
		{
			return DateTime.Now.ToString(format);
		}

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
					if (i > 1) // cater for vars that start with an acronym, e.g. "ABCCode" -> "abcCode"
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
	}
}