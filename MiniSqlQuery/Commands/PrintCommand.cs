using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Core;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MiniSqlQuery.Commands
{
	public class PrintCommand
	   : CommandBase
	{
		public PrintCommand()
			: base("Print...")
		{
		}

		public override void Execute()
		{
			IPrintableContent printable = Services.HostWindow.ActiveChildForm as IPrintableContent;
			if (printable != null)
			{
				PrintDocument doc = printable.PrintDocument;

				if (doc != null)
				{
					using (PrintDialog ppd = new PrintDialog())
					{
						ppd.Document = doc;
						ppd.AllowSomePages = true;
						if (ppd.ShowDialog(Services.HostWindow.Instance) == DialogResult.OK)
						{
							doc.Print();
						}
					}
				}
			}
		}

		public override bool Enabled
		{
			get
			{
				IPrintableContent printable = Services.HostWindow.ActiveChildForm as IPrintableContent;
				if (printable != null)
				{
					PrintDocument doc = printable.PrintDocument;

					if (doc != null)
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
