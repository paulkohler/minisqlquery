using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;

namespace MiniSqlQuery.Commands
{
	public class ShowAboutCommand
        : CommandBase
    {
		public ShowAboutCommand()
            : base("&About...")
        {
			SmallImage = ImageResource.ApplicationIcon;
		}

        public override void Execute()
        {
			Form frm = (Form)Services.Container.Resolve(typeof(AboutForm));
			frm.Show(HostWindow.Instance);
        }
    }
}
