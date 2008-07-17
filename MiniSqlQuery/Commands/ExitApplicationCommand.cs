using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	public class ExitApplicationCommand
       : CommandBase
    {
		public ExitApplicationCommand()
            : base("E&xit")
        {
		}

        public override void Execute()
        {
			Services.HostWindow.Instance.Close();
        }
    }
}
