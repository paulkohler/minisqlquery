#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
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