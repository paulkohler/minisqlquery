#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

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