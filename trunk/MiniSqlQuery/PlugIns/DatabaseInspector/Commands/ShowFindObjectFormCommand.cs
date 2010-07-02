#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The show find object form command.</summary>
	public class ShowFindObjectFormCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowFindObjectFormCommand"/> class.</summary>
		public ShowFindObjectFormCommand()
			: base("Find Object...")
		{
			ShortcutKeys = Keys.Alt | Keys.F;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			using (var frm = Services.Resolve<FindObjectForm>())
			{
				frm.ShowDialog(HostWindow.Instance);

				if (frm.DialogResult == DialogResult.OK)
				{
					IDbModelNamedObject obj = HostWindow.DatabaseInspector.DbSchema.FindTableOrView(frm.SelectedTableName);
					HostWindow.DatabaseInspector.NavigateTo(obj);
				}
			}
		}
	}
}