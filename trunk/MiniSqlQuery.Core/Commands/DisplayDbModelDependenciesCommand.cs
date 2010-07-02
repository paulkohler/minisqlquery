#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Text;
using MiniSqlQuery.Core.DbModel;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>The display db model dependencies command.</summary>
	public class DisplayDbModelDependenciesCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="DisplayDbModelDependenciesCommand"/> class.</summary>
		public DisplayDbModelDependenciesCommand()
			: base("Order Tables by FK Dependencies")
		{
			SmallImage = ImageResource.table_link;
		}

		/// <summary>Execute the command.</summary>
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