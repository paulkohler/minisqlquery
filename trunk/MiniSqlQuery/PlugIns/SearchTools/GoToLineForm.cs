#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.SearchTools
{
	public partial class GoToLineForm : Form
	{
		private readonly IApplicationServices _services;

		public GoToLineForm(IApplicationServices services)
		{
			_services = services;
			InitializeComponent();
		}

		public string LineValue
		{
			get { return txtLine.Text; }
			set { txtLine.Text = value; }
		}

		private void GoToLineForm_Load(object sender, EventArgs e)
		{
			INavigatableDocument navDoc = _services.HostWindow.ActiveChildForm as INavigatableDocument;
			if (navDoc != null)
			{
				LineValue = (navDoc.CursorLine + 1).ToString();
				Text = string.Format("{0} (1-{1})", Text, navDoc.TotalLines);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			INavigatableDocument navDoc = _services.HostWindow.ActiveChildForm as INavigatableDocument;
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
				SystemSounds.Beep.Play();
			}
		}
	}
}