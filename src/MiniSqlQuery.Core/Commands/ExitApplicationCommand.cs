#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion


namespace MiniSqlQuery.Core.Commands
{
    /// <summary>
    /// 	The exit application command.
    /// </summary>
    public class ExitApplicationCommand
        : CommandBase
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "ExitApplicationCommand" /> class.
        /// </summary>
        public ExitApplicationCommand()
            : base("E&xit")
        {
        }

        /// <summary>
        /// 	Execute the command.
        /// </summary>
        public override void Execute()
        {
            HostWindow.Instance.Close();
        }
    }
}