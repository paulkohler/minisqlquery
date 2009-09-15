using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools.Commands
{
	public class FindNextStringCommand : CommandBase
	{
		public FindNextStringCommand() : base("Find Next String")
		{
			SmallImage = ImageResource.find;
			ShortcutKeys = Keys.F3;
		}

		/// <summary>
		/// Two execution methods - through the "Find Text" window or by hitting F3.
		/// If simply hitting F3, we are executing the current search from current cursor position for this window.
		/// Different windows can also have different search text.
		/// </summary>
		public override void Execute()
		{
			IFindReplaceProvider editorFindProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (editorFindProvider != null)
			{
				FindTextRequest findTextRequest = null;
				int key = editorFindProvider.GetHashCode();

				// is there a request in the table for this window?
				if (SearchToolsCommon.FindReplaceTextRequests.ContainsKey(key))
				{
					findTextRequest = SearchToolsCommon.FindReplaceTextRequests[key];
				}
				else
				{
					if (SearchToolsCommon.FindReplaceTextRequests.Count > 0)
					{
						// if there is an entry in the list of searches create an instance
						findTextRequest = new FindTextRequest(editorFindProvider);
						findTextRequest.Position = editorFindProvider.CursorOffset;
					}
					else
					{
						// none in table, default to curently selected text if its the editor
						IQueryEditor editor = ActiveFormAsEditor;
						if (editor != null && editor.SelectedText.Length > 0)
						{
							findTextRequest = new FindTextRequest(editorFindProvider, editor.SelectedText);
							findTextRequest.Position = editor.CursorOffset;
						}
					}
				}

				if (findTextRequest != null)
				{
					// wrap around to start if at last pos
					if (findTextRequest.Position != 0)
					{
						findTextRequest.Position = editorFindProvider.CursorOffset;
					}

					findTextRequest = editorFindProvider.TextFindService.FindNext(findTextRequest);
					SearchToolsCommon.FindReplaceTextRequests[key] = findTextRequest;
				}
			}
		}
	}
}