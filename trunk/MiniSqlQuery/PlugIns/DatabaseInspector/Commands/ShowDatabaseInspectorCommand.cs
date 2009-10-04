#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class ShowDatabaseInspectorCommand : CommandBase
	{
		public ShowDatabaseInspectorCommand()
			: base("Show Database Inspector")
		{
		}

		public override void Execute()
		{
			DockContent databaseInspector = Services.Resolve<IDatabaseInspector>() as DockContent;
			if (databaseInspector != null)
			{
				HostWindow.ShowDatabaseInspector(databaseInspector as IDatabaseInspector, DockState.DockLeft);
				databaseInspector.Activate();
			}
		}
	}
}