using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace $safeprojectname$.Commands
{
	public class SampleCommand : CommandBase
    {
		public SampleCommand() : base("&Descriptive Name")
		{
			SmallImage = ImageResource.plugin;
        }

        public override void Execute()
        {
			Form1 frm = new Form1();
			frm.ShowDialog(Services.HostWindow.Instance);
        }
    }
}
