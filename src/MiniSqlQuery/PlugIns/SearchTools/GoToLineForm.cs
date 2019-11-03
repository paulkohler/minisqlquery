#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.SearchTools
{
    /// <summary>The go to line form.</summary>
    public partial class GoToLineForm : Form
    {
        /// <summary>The _services.</summary>
        private readonly IApplicationServices _services;

        /// <summary>Initializes a new instance of the <see cref="GoToLineForm"/> class.</summary>
        /// <param name="services">The services.</param>
        public GoToLineForm(IApplicationServices services)
        {
            _services = services;
            InitializeComponent();
        }

        /// <summary>Gets or sets LineValue.</summary>
        public string LineValue
        {
            get { return txtLine.Text; }
            set { txtLine.Text = value; }
        }

        /// <summary>The go to line form_ load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void GoToLineForm_Load(object sender, EventArgs e)
        {
            INavigatableDocument navDoc = _services.HostWindow.ActiveChildForm as INavigatableDocument;
            if (navDoc != null)
            {
                LineValue = (navDoc.CursorLine + 1).ToString();
                Text = string.Format("{0} (1-{1})", Text, navDoc.TotalLines);
            }
        }

        /// <summary>The btn ok_ click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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