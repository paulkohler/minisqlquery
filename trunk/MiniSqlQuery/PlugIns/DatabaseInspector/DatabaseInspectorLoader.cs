#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.DatabaseInspector.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	public class DatabaseInspectorLoader : PluginLoaderBase
	{
		public DatabaseInspectorLoader()
			: base(
				"Database Inspector",
				"A Mini SQL Query Plugin for displaying the database schema in a tree view",
				20)
		{
		}

		public override void InitializePlugIn() 
		{
			Services.RegisterSingletonComponent<IDatabaseInspector, DatabaseInspectorForm>("DatabaseInspector");
			Services.RegisterComponent<FindObjectForm>("FindObjectForm");

			IHostWindow hostWindow = Services.HostWindow;
			hostWindow.AddPluginCommand<ShowDatabaseInspectorCommand>();
			CommandManager.GetCommandInstance<ShowDatabaseInspectorCommand>().Execute();

			ToolStripMenuItem editMenu = hostWindow.GetMenuItem("edit");
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowFindObjectFormCommand>());

			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectCountStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateInsertStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateUpdateStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateDeleteStatementCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<CopyTableNameCommand>());
			hostWindow.DatabaseInspector.TableMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<TruncateTableCommand>());

			hostWindow.DatabaseInspector.ColumnMenu.Items.Add(CommandControlBuilder.CreateToolStripMenuItem<LocateFkReferenceColumnCommand>());

			// todo: bug - the opening event is not firing....
			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(hostWindow.DatabaseInspector.ColumnMenu);
		}

	}
}