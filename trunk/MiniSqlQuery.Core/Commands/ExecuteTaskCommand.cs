#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class ExecuteTaskCommand
		: CommandBase
	{
		public ExecuteTaskCommand()
			: base("&Execute")
		{
			ShortcutKeys = Keys.F5;
			ShortcutKeysText = "F5";
			SmallImage = ImageResource.lightning;
		}

		public override bool Enabled
		{
			get
			{
				IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
				if (editor != null)
				{
					return !editor.IsBusy;
				}
				return false;
			}
		}

		public override void Execute()
		{
			if (!Enabled)
			{
				return;
			}

			IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
			if (editor != null)
			{
				editor.ExecuteTask();
			}
		}
	}
}