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
	/// <summary>The find replace form.</summary>
	public partial class FindReplaceForm : Form, IFindReplaceWindow
	{
		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>Initializes a new instance of the <see cref="FindReplaceForm"/> class.</summary>
		/// <param name="services">The services.</param>
		public FindReplaceForm(IApplicationServices services)
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			_services = services;
		}

		/// <summary>Gets or sets FindString.</summary>
		public string FindString
		{
			get { return txtFindString.Text; }
			set { txtFindString.Text = value; }
		}

		/// <summary>Gets or sets ReplaceString.</summary>
		public string ReplaceString
		{
			get { return txtReplaceText.Text; }
			set { txtReplaceText.Text = value; }
		}

		/// <summary>The create find request.</summary>
		/// <param name="provider">The provider.</param>
		/// <param name="findString">The find string.</param>
		/// <param name="replaceValue">The replace value.</param>
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

		/// <summary>The dim form.</summary>
		private void DimForm()
		{
			Opacity = 0.8;
		}

		/// <summary>The find replace form_ activated.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The find replace form_ deactivate.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void FindReplaceForm_Deactivate(object sender, EventArgs e)
		{
			DimForm();
		}

		/// <summary>The find replace form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void FindReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		/// <summary>The find replace form_ key up.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void FindReplaceForm_KeyUp(object sender, KeyEventArgs e)
		{
			// simulate close
			if (e.KeyCode == Keys.Escape)
			{
				e.Handled = true;
				Hide();
			}
		}


		/// <summary>The handle find next.</summary>
		/// <param name="provider">The provider.</param>
		/// <param name="findString">The find string.</param>
		/// <param name="replaceValue">The replace value.</param>
		private void HandleFindNext(IFindReplaceProvider provider, string findString, string replaceValue)
		{
			CreateFindRequest(provider, findString, replaceValue);
			CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
		}

		/// <summary>The handle replace next.</summary>
		/// <param name="provider">The provider.</param>
		/// <param name="findString">The find string.</param>
		/// <param name="replaceValue">The replace value.</param>
		private void HandleReplaceNext(IFindReplaceProvider provider, string findString, string replaceValue)
		{
			CommandManager.GetCommandInstance<ReplaceStringCommand>().Execute();
		}

		/// <summary>The un dim form.</summary>
		private void UnDimForm()
		{
			Opacity = 1.0;
		}

		/// <summary>The btn cancel_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Hide();
		}

		/// <summary>The btn find next_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The btn replace_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The txt find string_ enter.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void txtFindString_Enter(object sender, EventArgs e)
		{
			UnDimForm();
		}

		/// <summary>The txt find string_ leave.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void txtFindString_Leave(object sender, EventArgs e)
		{
			DimForm();
		}
	}
}