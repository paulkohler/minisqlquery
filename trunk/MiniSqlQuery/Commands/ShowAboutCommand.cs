#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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
