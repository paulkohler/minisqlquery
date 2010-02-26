#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core.Commands
{
	public class CancelTaskCommand
		: CommandBase
	{
		public CancelTaskCommand()
			: base("&Cancel")
		{
			SmallImage = ImageResource.stop;
		}

		public override bool Enabled
		{
			get
			{
				IPerformTask editor = HostWindow.ActiveChildForm as IPerformTask;
				return editor != null && editor.IsBusy;
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
				editor.CancelTask();
			}
		}
	}
}