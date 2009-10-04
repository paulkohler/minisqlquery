#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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
				IPrintableContent printable = HostWindow.ActiveChildForm as IPrintableContent;
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
			IPrintableContent printable = HostWindow.ActiveChildForm as IPrintableContent;
			if (printable != null)
			{
				PrintDocument doc = printable.PrintDocument;

				if (doc != null)
				{
					using (PrintDialog ppd = new PrintDialog())
					{
						ppd.Document = doc;
						ppd.AllowSomePages = true;
						if (ppd.ShowDialog(HostWindow.Instance) == DialogResult.OK)
						{
							doc.Print();
						}
					}
				}
			}
		}
	}
}