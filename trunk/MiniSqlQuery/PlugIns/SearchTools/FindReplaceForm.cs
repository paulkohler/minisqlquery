#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.SearchTools.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools
{
	public partial class FindReplaceForm : Form, IFindReplaceWindow
	{
		private readonly IApplicationServices _services;

		public FindReplaceForm(IApplicationServices services)
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			_services = services;
		}

		#region IFindReplaceWindow Members

		public string FindString
		{
			get { return txtFindString.Text; }
			set { txtFindString.Text = value; }
		}

		public string ReplaceString
		{
			get { return txtReplaceText.Text; }
			set { txtReplaceText.Text = value; }
		}

		#endregion

		private void FindReplaceForm_KeyUp(object sender, KeyEventArgs e)
		{
			// simulate close
			if (e.KeyCode == Keys.Escape)
			{
				e.Handled = true;
				Hide();
			}
		}

		private void FindReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void btnFindNext_Click(object sender, EventArgs e)
		{
			IFindReplaceProvider provider = _services.HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (provider == null)
			{
				SystemSounds.Beep.Play();
			}
			else
			{
				HandleFindNext(provider, FindString, ReplaceString);
			}
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			IFindReplaceProvider provider = _services.HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (provider == null)
			{
				SystemSounds.Beep.Play();
			}
			else
			{
				HandleReplaceNext(provider, FindString, ReplaceString);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Hide();
		}


		private void HandleFindNext(IFindReplaceProvider provider, string findString, string replaceValue)
		{
			CreateFindRequest(provider, findString, replaceValue);
			CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
		}

		private void HandleReplaceNext(IFindReplaceProvider provider, string findString, string replaceValue)
		{
			//CreateFindRequest(provider, findString);
			CommandManager.GetCommandInstance<ReplaceStringCommand>().Execute();
		}

		private void CreateFindRequest(IFindReplaceProvider provider, string findString, string replaceValue)
		{
			int key = provider.GetHashCode();
			FindTextRequest request;

			if (SearchToolsCommon.FindReplaceTextRequests.ContainsKey(key))
			{
				request = SearchToolsCommon.FindReplaceTextRequests[key];
				if (request.SearchValue != findString)
				{
					// reset find text and set the starting position to the current cursor location
					request.SearchValue = findString;
					request.ReplaceValue = replaceValue;
					request.Position = provider.CursorOffset;
				}
			}
			else
			{
				request = new FindTextRequest(provider, findString);
				request.ReplaceValue = replaceValue;
			}

			SearchToolsCommon.FindReplaceTextRequests[key] = request;
		}

		#region Dim Handling

		private void txtFindString_Enter(object sender, EventArgs e)
		{
			UnDimForm();
		}

		private void txtFindString_Leave(object sender, EventArgs e)
		{
			DimForm();
		}

		private void FindReplaceForm_Deactivate(object sender, EventArgs e)
		{
			DimForm();
		}

		private void FindReplaceForm_Activated(object sender, EventArgs e)
		{
			if (txtFindString.Focused | txtReplaceText.Focused)
			{
				UnDimForm();
			}
			else
			{
				DimForm();
			}
		}

		private void UnDimForm()
		{
			Opacity = 1.0;
		}

		private void DimForm()
		{
			Opacity = 0.8;
		}

		#endregion
	}
}