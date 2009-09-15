using System;
using System.Collections;
using System.Collections.Generic;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateHost : IDisposable
	{
		private readonly IDatabaseInspector _databaseInspector;

		public TemplateHost(IApplicationServices applicationServices, IDatabaseInspector databaseInspector)
		{
			ApplicationServices = applicationServices;
			_databaseInspector = databaseInspector;
		}

		public IApplicationServices ApplicationServices { get; set; }

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
	}
}