#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion


namespace MiniSqlQuery.Core.Commands
{
    /// <summary>
    /// 	The close active window command.
    /// </summary>
    public class CloseActiveWindowCommand
        : CommandBase
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "CloseActiveWindowCommand" /> class.
        /// </summary>
        public CloseActiveWindowCommand()
            : base("&Close")
        {
        }

        /// <summary>
        /// 	Gets a value indicating whether Enabled.
        /// </summary>
        /// <value>The enabled state.</value>
        public override bool Enabled
        {
            get { return HostWindow.ActiveChildForm != null; }
        }

        /// <summary>
        /// 	Execute the command.
        /// </summary>
        public override void Execute()
        {
            var frm = HostWindow.ActiveChildForm;
            if (frm != null)
            {
                frm.Close();
            }
        }
    }
}