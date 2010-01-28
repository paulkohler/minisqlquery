using System;
using System.Windows.Forms;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class CopyTableNameCommand : CommandBase
	{
		public CopyTableNameCommand()
			: base("Copy table name")
		{
		}

		public override void Execute()
		{
			Clipboard.SetText(HostWindow.DatabaseInspector.RightClickedTableName);
		}
	}
}