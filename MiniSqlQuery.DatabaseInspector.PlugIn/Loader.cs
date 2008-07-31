using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.DatabaseInspector.PlugIn.Commands;

namespace MiniSqlQuery.DatabaseInspector.PlugIn
{
	public class Loader : PluginLoaderBase
	{
		public Loader ()
			: base(
				"Database Inspector",
				"A Mini SQL Query Plugin for displaying the database schema as a tree view",
				20)
		{
		}

		public override void InitializePlugIn() 
		{
			DatabaseInspectorForm dbi = new DatabaseInspectorForm(Services);
			Services.HostWindow.ShowDatabaseInspector(dbi, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
			dbi.Activate();

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
