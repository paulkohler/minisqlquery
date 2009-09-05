using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateHost
	{
		public IApplicationServices Services { get; set; }
		public DbModelInstance Model { get; set; }

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
	}
}