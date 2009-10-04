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
	public class RunTemplateCommand
		: CommandBase
	{
		public RunTemplateCommand()
			: base("Run Template")
		{
			SmallImage = ImageResource.script_code;
		}

		public override void Execute()
		{
			ITemplateEditor templateEditor = HostWindow.ActiveChildForm as ITemplateEditor;

			if (templateEditor != null)
			{
				templateEditor.RunTemplate();
			}
		}

		public override bool Enabled
		{
			get
			{
				return HostWindow.ActiveChildForm is ITemplateEditor;
			}
		}
	}
}