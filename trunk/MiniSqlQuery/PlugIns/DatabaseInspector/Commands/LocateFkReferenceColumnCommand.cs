#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The locate fk reference column command.</summary>
	public class LocateFkReferenceColumnCommand : GenerateStatementCommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="LocateFkReferenceColumnCommand"/> class.</summary>
		public LocateFkReferenceColumnCommand()
			: base("Jump to FK column reference...")
		{
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		public override bool Enabled
		{
			get
			{
				DbModelColumn column = HostWindow.DatabaseInspector.RightClickedModelObject as DbModelColumn;
				return column != null && column.ForeignKeyReference != null;
			}
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			DbModelColumn column = HostWindow.DatabaseInspector.RightClickedModelObject as DbModelColumn;
			if (column != null && column.ForeignKeyReference != null)
			{
				HostWindow.DatabaseInspector.NavigateTo(column.ForeignKeyReference.ReferenceColumn);
			}
		}
	}
}