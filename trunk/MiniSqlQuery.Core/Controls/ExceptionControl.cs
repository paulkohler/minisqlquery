using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Controls
{
	/// <summary>
	/// A basic control for displaying an unhandled exception.
	/// </summary>
	public partial class ExceptionControl : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionControl"/> class.
		/// </summary>
		public ExceptionControl()
		{
			InitializeComponent();
		}

		private void ExceptionControl_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Sets the exception to display.
		/// </summary>
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

		private void lnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clipboard.SetText(txtDetails.Text);
		}
	}
}
