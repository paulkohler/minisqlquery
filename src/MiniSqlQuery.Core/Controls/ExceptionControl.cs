#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
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