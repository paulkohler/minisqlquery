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
			_services = services;
		}

		public string FindString
		{
			get { return txtFindString.Text; }
			set { txtFindString.Text = value; }
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
				HandleFindNext(provider, FindString);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void HandleFindNext(IFindReplaceProvider provider, string findString)
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
					request.Position = provider.CursorOffset;
				}
			}
			else
			{
				request = new FindTextRequest(provider, findString);
			}

			SearchToolsCommon.FindReplaceTextRequests[key] = request;
			CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
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
			if (txtFindString.Focused)
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