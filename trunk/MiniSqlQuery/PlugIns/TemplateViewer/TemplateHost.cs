using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
			string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
			return result.Replace(" ", string.Empty);
		}

		public string ToCamelCase(string text)
		{
			if (text == null)
			{
				return string.Empty;
			}
			string result = ToPascalCase(text);
			if (result.Length > 1)
			{
				result = Char.ToLower(result[0]) + result.Substring(1);
			}
			else
			{
				result = result.ToLowerInvariant();
			}
			return result;
		}
	}
}