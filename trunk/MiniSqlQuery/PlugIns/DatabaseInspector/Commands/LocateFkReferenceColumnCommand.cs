using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	public class LocateFkReferenceColumnCommand : GenerateStatementCommandBase
	{
		public LocateFkReferenceColumnCommand()
			: base("Jump to FK column reference...")
		{
		}

		public override void Execute()
		{
			DbModelColumn column = HostWindow.DatabaseInspector.RightClickedModelObject as DbModelColumn;
			if (column != null && column.ForeignKeyReference != null)
			{
				HostWindow.DatabaseInspector.NavigateTo(column.ForeignKeyReference.ReferenceColumn);
			}
		}

		public override bool Enabled
		{
			get
			{
				DbModelColumn column = HostWindow.DatabaseInspector.RightClickedModelObject as DbModelColumn;
				return column != null && column.ForeignKeyReference != null;
			}
		}
	}
}