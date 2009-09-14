using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateHost:IDisposable
	{
		public TemplateHost(IApplicationServices services)
		{
			Services = services;
		}

		public IApplicationServices Services { get; private set; }
		public DbModelInstance Model {
			get
			{
				if (Services.HostWindow.DatabaseInspector.DbSchema == null)
				{
					Services.HostWindow.DatabaseInspector.LoadDatabaseDetails();
				}
				return Services.HostWindow.DatabaseInspector.DbSchema;
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

		public string Date(string format)
		{
			return DateTime.Now.ToString(format);
		}

		// to do - helper functions for changing names too code friendly etc
		public void Dispose()
		{
			if (Data != null)
			{
				Data.Dispose();
			}
		}
	}
}