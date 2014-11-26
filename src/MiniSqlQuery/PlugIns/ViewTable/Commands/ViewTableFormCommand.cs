#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.ViewTable.Commands
{
	/// <summary>The view table form command.</summary>
	public class ViewTableFormCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ViewTableFormCommand"/> class.</summary>
		public ViewTableFormCommand()
			: base("&View table...")
		{
			ShortcutKeys = Keys.Control | Keys.T;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IQueryEditor queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
			if (queryForm != null)
			{
				string tableName = queryForm.SelectedText;
				IViewTable frm = Services.Resolve<IViewTable>();
				frm.TableName = tableName;
				HostWindow.DisplayDockedForm(frm as DockContent);
			}
		}
	}
}