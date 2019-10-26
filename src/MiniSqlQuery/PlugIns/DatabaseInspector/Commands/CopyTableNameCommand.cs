#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
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