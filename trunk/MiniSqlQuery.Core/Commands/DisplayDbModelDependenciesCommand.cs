using System;
using System.Text;
using MiniSqlQuery.Core.DbModel;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	public class DisplayDbModelDependenciesCommand
		: CommandBase
	{
		public DisplayDbModelDependenciesCommand()
			: base("Order Tables by FK Dependencies")
		{
			SmallImage = ImageResource.table_link;
		}

		public override void Execute()
		{
			var editor = Services.Resolve<IEditor>("txt-editor");
			editor.FileName = null;
			HostWindow.DisplayDockedForm(editor as DockContent);

			if (HostWindow.DatabaseInspector.DbSchema == null)
			{
				HostWindow.DatabaseInspector.LoadDatabaseDetails();
			}
			DbModelDependencyWalker dependencyWalker = new DbModelDependencyWalker(HostWindow.DatabaseInspector.DbSchema);
			var tables = dependencyWalker.SortTablesByForeignKeyReferences();

			StringBuilder sb = new StringBuilder();
			foreach (DbModelTable table in tables)
			{
				sb.AppendLine(table.FullName);
			}

			editor.AllText = sb.ToString();
		}
	}
}