#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The print command.
	/// </summary>
	public class PrintCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "PrintCommand" /> class.
		/// </summary>
		public PrintCommand()
			: base("Print...")
		{
			SmallImage = ImageResource.printer;
		}

		/// <summary>
		/// 	Gets a value indicating whether Enabled.
		/// </summary>
		/// <value>The enabled state.</value>
		public override bool Enabled
		{
			get
			{
				var printable = HostWindow.ActiveChildForm as IPrintableContent;
				if (printable != null)
				{
					var doc = printable.PrintDocument;

					if (doc != null)
					{
						return true;
					}
				}

				return false;
			}
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			var printable = HostWindow.ActiveChildForm as IPrintableContent;
			if (printable != null)
			{
				var doc = printable.PrintDocument;

				if (doc != null)
				{
					using (var ppd = new PrintDialog())
					{
						ppd.Document = doc;
						ppd.AllowSomePages = true;
						// https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k(System.Windows.Forms.PrintDialog.UseEXDialog);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv3.5);k(DevLang-csharp)&rd=true#Anchor_1
						ppd.UseEXDialog = true;
						
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
