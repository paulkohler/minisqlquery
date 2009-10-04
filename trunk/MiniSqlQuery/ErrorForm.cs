#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MiniSqlQuery
{
	public partial class ErrorForm : Form
	{
		public ErrorForm()
		{
			InitializeComponent();
		}

		public void SetException(Exception exp)
		{
			exceptionControl1.SetException(exp);
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
