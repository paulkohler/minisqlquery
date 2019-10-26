#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The cancel task command.
	/// </summary>
	public class CancelTaskCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "CancelTaskCommand" /> class.
		/// </summary>
		public CancelTaskCommand()
			: base("&Cancel")
		{
			SmallImage = ImageResource.stop;
		}

		/// <summary>
		/// 	Gets a value indicating whether Enabled.
		/// </summary>
		/// <value>The enabled state.</value>
		public override bool Enabled
		{
			get
			{
				var editor = HostWindow.ActiveChildForm as IPerformTask;
				return editor != null && editor.IsBusy;
			}
		}

		/// <summary>
		/// 	Execute the command.
		/// </summary>
		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			var editor = HostWindow.ActiveChildForm as IPerformTask;
			if (editor != null)
			{
				editor.CancelTask();
			}
		}
	}
}