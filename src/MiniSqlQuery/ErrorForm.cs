#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery
{
	/// <summary>The error form.</summary>
	public partial class ErrorForm : Form
	{
		/// <summary>Initializes a new instance of the <see cref="ErrorForm"/> class.</summary>
		public ErrorForm()
		{
			InitializeComponent();
		}

		/// <summary>The set exception.</summary>
		/// <param name="exp">The exp.</param>
		public void SetException(Exception exp)
		{
			exceptionControl1.SetException(exp);
		}

		/// <summary>The btn ok_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}