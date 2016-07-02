#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Controls
{
	/// <summary>A basic control for displaying an unhandled exception.</summary>
	public partial class ExceptionControl : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionControl"/> class.
		/// </summary>
		public ExceptionControl()
		{
			InitializeComponent();
		}

		/// <summary>Sets the exception to display.</summary>
		/// <param name="exp">The exception object.</param>
		public void SetException(Exception exp)
		{
			if (exp != null)
			{
				lblError.Text = exp.GetType().FullName;
				txtMessage.Text = exp.Message;
				txtDetails.Text = exp.ToString();
			}
		}

		/// <summary>The exception control_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ExceptionControl_Load(object sender, EventArgs e)
		{
		}

		/// <summary>The lnk copy_ link clicked.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clipboard.SetText(txtDetails.Text);
		}
	}
}