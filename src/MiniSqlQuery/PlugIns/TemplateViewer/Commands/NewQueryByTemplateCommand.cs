#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.TemplateViewer.Commands
{
	/// <summary>The new query by template command.</summary>
	public class NewQueryByTemplateCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="NewQueryByTemplateCommand"/> class.</summary>
		public NewQueryByTemplateCommand()
			: base("New &Query from Template")
		{
			SmallImage = ImageResource.script_code;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			ICommand newQueryFormCommand = CommandManager.GetCommandInstance("NewQueryFormCommand");
			newQueryFormCommand.Execute();
		}
	}
}