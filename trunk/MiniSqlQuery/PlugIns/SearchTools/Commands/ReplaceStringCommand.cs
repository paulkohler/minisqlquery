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
			SmallImage = ImageResource.find;
			ShortcutKeys = Keys.Alt | Keys.R;
		}

		public override void Execute()
		{
			IFindReplaceProvider editorFindProvider = Services.HostWindow.ActiveChildForm as IFindReplaceProvider;

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