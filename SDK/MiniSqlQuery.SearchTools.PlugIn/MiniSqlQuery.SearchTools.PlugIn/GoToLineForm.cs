using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.SearchTools.PlugIn
{
	public partial class GoToLineForm : Form
	{
		public GoToLineForm()
		{
			InitializeComponent();
		}

		public string LineValue
		{
			get
			{
				return txtLine.Text;
			}
			set
			{
				txtLine.Text = value;
			}
		}

		private void GoToLineForm_Load(object sender, EventArgs e)
		{
			INavigatableDocument navDoc = ApplicationServices.Instance.HostWindow.ActiveChildForm as INavigatableDocument;
			if (navDoc != null)
			{
				LineValue = (navDoc.CursorLine + 1).ToString();
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			INavigatableDocument navDoc = ApplicationServices.Instance.HostWindow.ActiveChildForm as INavigatableDocument;
			if (navDoc != null)
			{
				int line;

				if (int.TryParse(LineValue, out line))
				{
					int column = 0;
					line = Math.Abs(line - 1);

					// todo - copy column?
					if (navDoc.SetCursorByLocation(line, column))
					{
						Close();
					}
				}

				// otherwise
				System.Media.SystemSounds.Beep.Play();
			}
		}
	}
}
