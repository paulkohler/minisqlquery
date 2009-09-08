using System;
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

			Services.HostWindow.AddPluginCommand<ShowDatabaseInspectorCommand>();
			CommandManager.GetCommandInstance<ShowDatabaseInspectorCommand>().Execute();

			Services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
				CommandControlBuilder.CreateToolStripMenuItem<GenerateSelectStatementCommand>());
			Services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
				CommandControlBuilder.CreateToolStripMenuItem<GenerateInsertStatementCommand>());
			Services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
				CommandControlBuilder.CreateToolStripMenuItem<GenerateUpdateStatementCommand>());
			Services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
				CommandControlBuilder.CreateToolStripMenuItem<GenerateDeleteStatementCommand>());
			Services.HostWindow.DatabaseInspector.TableMenu.Items.Add(
				CommandControlBuilder.CreateToolStripMenuItem<TruncateTableCommand>());
		}

	}
}