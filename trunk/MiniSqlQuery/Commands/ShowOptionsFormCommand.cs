#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	public class ShowOptionsFormCommand
		: CommandBase
	{
		public ShowOptionsFormCommand()
			: base("Options")
		{
			//ShortcutKeys = ?;
			SmallImage = ImageResource.cog;
		}

		public override void Execute()
		{
			using (OptionsForm optionsForm = Services.Resolve<OptionsForm>())
			{
				optionsForm.ShowDialog(HostWindow.Instance);
			}
		}
	}
}