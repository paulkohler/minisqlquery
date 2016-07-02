#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	/// <summary>
	/// 	The execute task command.
	/// </summary>
	public class ExecuteTaskCommand
		: CommandBase
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "ExecuteTaskCommand" /> class.
		/// </summary>
		public ExecuteTaskCommand()
			: base("&Execute")
		{
			ShortcutKeys = Keys.F5;
			ShortcutKeysText = "F5";
			SmallImage = ImageResource.lightning;
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
				if (editor != null)
				{
					return !editor.IsBusy;
				}

				return false;
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
				editor.ExecuteTask();
			}
		}
	}
}