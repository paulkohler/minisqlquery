#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager.Commands
{
	/// <summary>The edit connections form command.</summary>
	public class EditConnectionsFormCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="EditConnectionsFormCommand"/> class.</summary>
		public EditConnectionsFormCommand()
			: base("&Edit Connection Strings")
		{
			SmallImage = ImageResource.database_edit;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			DbConnectionsForm frm = Services.Resolve<DbConnectionsForm>();
			frm.ShowDialog(HostWindow.Instance);
		}
	}
}