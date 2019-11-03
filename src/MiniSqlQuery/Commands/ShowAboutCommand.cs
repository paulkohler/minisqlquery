#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using Ninject;

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