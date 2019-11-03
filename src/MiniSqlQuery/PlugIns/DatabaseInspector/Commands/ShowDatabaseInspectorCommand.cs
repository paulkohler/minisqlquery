#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The show database inspector command.</summary>
	public class ShowDatabaseInspectorCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowDatabaseInspectorCommand"/> class.</summary>
		public ShowDatabaseInspectorCommand()
			: base("Show Database Inspector")
		{
		}

		/// <summary>Execute the command.</summary>
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