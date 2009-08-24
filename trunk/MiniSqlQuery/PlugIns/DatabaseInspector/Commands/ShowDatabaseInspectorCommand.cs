using System;
using System.Collections.Generic;
using System.Collections;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class ShowDatabaseInspectorCommand : CommandBase
	{
		DatabaseInspectorForm _inspectorForm;

		public ShowDatabaseInspectorCommand()
			: base("Show Database Inspector")
		{
		}

		public override void Execute()
		{
			if (_inspectorForm == null)
			{
				_inspectorForm = new DatabaseInspectorForm(Services);
			}
			Services.HostWindow.ShowDatabaseInspector(_inspectorForm, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
			_inspectorForm.Activate();
		}
	}
}