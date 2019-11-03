#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
    /// <summary>
    /// 	Description of PasteAroundSelectionCommand.
    /// </summary>
    public class PasteAroundSelectionCommand : CommandBase
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "PasteAroundSelectionCommand" /> class.
        /// </summary>
        public PasteAroundSelectionCommand()
          : base("Paste &Around Selection")
        {
            ShortcutKeys = Keys.Alt | Keys.A;
            SmallImage = ImageResource.around_text;
        }

        /// <summary>
        /// Gets or sets the "left text".
        /// </summary>
        /// <value>The "left text".</value>
        public static string LeftText { get; set; }

        /// <summary>
        /// Gets or sets the "right text".
        /// </summary>
        /// <value>The "right text".</value>
        public static string RightText { get; set; }

        /// <summary>
        /// 	Execute the command.
        /// </summary>
        public override void Execute()
        {
            var queryForm = HostWindow.Instance.ActiveMdiChild as IQueryEditor;
            if (queryForm != null)
            {
                string newText = string.Concat(LeftText, queryForm.SelectedText, RightText);
                queryForm.InsertText(newText);
            }
        }
    }
}