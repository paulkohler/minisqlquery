#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	/// <summary>The show about command.</summary>
	public class ShowAboutCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowAboutCommand"/> class.</summary>
		public ShowAboutCommand()
			: base("&About...")
		{
			SmallImage = ImageResource.ApplicationIcon;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			Form frm = Services.Resolve<AboutForm>();
			frm.Show(HostWindow.Instance);
		}
	}
}