#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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