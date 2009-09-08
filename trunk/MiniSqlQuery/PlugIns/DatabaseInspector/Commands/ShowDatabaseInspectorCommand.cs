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
				Services.HostWindow.ShowDatabaseInspector(databaseInspector as IDatabaseInspector, DockState.DockLeft);
				databaseInspector.Activate();
			}
		}
	}
}