#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.DatabaseInspector.Commands
{
	/// <summary>The copy table name command.</summary>
	public class CopyTableNameCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="CopyTableNameCommand"/> class.</summary>
		public CopyTableNameCommand()
			: base("Copy table name")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			Clipboard.SetText(HostWindow.DatabaseInspector.RightClickedTableName);
		}
	}
}