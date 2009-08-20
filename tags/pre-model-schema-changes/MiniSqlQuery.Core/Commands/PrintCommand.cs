using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class PrintCommand
		: CommandBase
	{
		public PrintCommand()
			: base("Print...")
		{
			SmallImage = ImageResource.printer;
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
	}
}