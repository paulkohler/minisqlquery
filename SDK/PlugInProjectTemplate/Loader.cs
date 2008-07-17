using System;
using $safeprojectname$.Commands;
using MiniSqlQuery.Core;

namespace $safeprojectname$
{
	public class Loader : PluginLoaderBase
	{
		public Loader() : base(
			"My Plugin Name", 
			"My Plugin Description.")
		{
		}

		public override void InitializePlugIn()
		{
			Services.HostWindow.AddPluginCommand<SampleCommand>();
			Services.HostWindow.AddToolStripCommand<SampleCommand>(null);
		}
	}
}
