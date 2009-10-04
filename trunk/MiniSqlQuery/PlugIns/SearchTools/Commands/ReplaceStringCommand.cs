#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	public class ReplaceStringCommand : CommandBase
	{
		public ReplaceStringCommand()
			: base("Replace String")
		{
		}

		public override void Execute()
		{
			IFindReplaceProvider editorFindProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (editorFindProvider != null)
			{
				FindTextRequest req = null;
				int key = editorFindProvider.GetHashCode();

				// is there a request in the table for this window?
				if (SearchToolsCommon.FindReplaceTextRequests.ContainsKey(key))
				{
					req = SearchToolsCommon.FindReplaceTextRequests[key];
				}

				if (req != null)
				{
					// wrap around to start if at last pos
					if (req.Position != 0)
					{
						req.Position = editorFindProvider.CursorOffset;
					}

					if (editorFindProvider.ReplaceString(req.ReplaceValue, req.Position - req.SearchValue.Length, req.SearchValue.Length))
					{
						CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
					}
				}
			}
		}
	}
}