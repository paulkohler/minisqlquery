#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.ViewTable.Commands;

namespace MiniSqlQuery.PlugIns.ViewTable
{
	/// <summary>The view table loader.</summary>
	public class ViewTableLoader : PluginLoaderBase
	{
		/// <summary>Initializes a new instance of the <see cref="ViewTableLoader"/> class.</summary>
		public ViewTableLoader()
			: base("View Table Data", "A Mini SQL Query Plugin for viewing table data.", 50)
		{
		}

		/// <summary>Iinitialize the plug in.</summary>
		public override void InitializePlugIn()
		{
			Services.RegisterComponent<IViewTable, ViewTableForm>("ViewTableForm");

			// the DB inspector may not be present
			if (Services.HostWindow.DatabaseInspector != null)
			{
				Services.HostWindow.DatabaseInspector.TableMenu.Items.Insert(
					0, CommandControlBuilder.CreateToolStripMenuItem<ViewTableFromInspectorCommand>());
			}

			Services.HostWindow.AddPluginCommand<ViewTableFormCommand>();
		}
	}
}