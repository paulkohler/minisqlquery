#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Commands
{
	/// <summary>The show options form command.</summary>
	public class ShowOptionsFormCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowOptionsFormCommand"/> class.</summary>
		public ShowOptionsFormCommand()
			: base("Options")
		{
			// ShortcutKeys = ?;
			SmallImage = ImageResource.cog;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			using (OptionsForm optionsForm = Services.Resolve<OptionsForm>())
			{
				optionsForm.ShowDialog(HostWindow.Instance);
			}
		}
	}
}