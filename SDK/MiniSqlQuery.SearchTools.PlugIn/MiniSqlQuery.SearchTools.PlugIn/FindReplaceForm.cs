using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.SearchTools.PlugIn.Commands;

namespace MiniSqlQuery.SearchTools.PlugIn
{
	public partial class FindReplaceForm : Form
	{
		public FindReplaceForm()
		{
			InitializeComponent();
		}

		public string FindString
		{
			get
			{
				return txtFindString.Text;
			}
			set
			{
				txtFindString.Text = value;
			}
		}

		private void btnFindNext_Click(object sender, EventArgs e)
		{
			IQueryEditor editor = ApplicationServices.Instance.HostWindow.ActiveChildForm as IQueryEditor;

			if (editor == null)
			{
				System.Media.SystemSounds.Beep.Play();
			}
			else
			{
				HandleFindNext(editor);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void HandleFindNext(IQueryEditor editor)
		{
			int key = editor.GetHashCode();
			FindTextRequest request;

			if (SearchToolsCommon.FindReplaceTextRequests.ContainsKey(key))
			{
				request = SearchToolsCommon.FindReplaceTextRequests[key];
				if (request.SearchValue != FindString)
				{
					// reset find text and set the starting position to the current cursor location
					request.SearchValue = FindString;
					request.Position = editor.CursorOffset;
				}
			}
			else
			{
				request = new FindTextRequest(editor, FindString);
			}

			SearchToolsCommon.FindReplaceTextRequests[key] = request;
			CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
		}
	}
}
