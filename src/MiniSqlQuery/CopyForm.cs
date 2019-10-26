#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery
{
	/// <summary>The copy form.</summary>
	public partial class CopyForm : Form
	{
		/// <summary>Initializes a new instance of the <see cref="CopyForm"/> class.</summary>
		public CopyForm()
		{
			InitializeComponent();
			rbTAB.Checked = true;
		}

		/// <summary>Gets Delimiter.</summary>
		public string Delimiter
		{
			get
			{
				if (rbCSV.Checked) return ",";
				if (rbTAB.Checked) return "\t";
				return txtOther.Text;
			}
		}

		/// <summary>Gets a value indicating whether IncludeHeaders.</summary>
		public bool IncludeHeaders
		{
			get { return xbIncludeHeaders.Checked; }
		}

		/// <summary>The cancel button_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>The ok button_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}