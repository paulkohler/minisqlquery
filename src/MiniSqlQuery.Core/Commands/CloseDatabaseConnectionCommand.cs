#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Data;

namespace MiniSqlQuery.Core.Commands
{
    /// <summary>The close database connection command.</summary>
    public class CloseDatabaseConnectionCommand
        : CommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="CloseDatabaseConnectionCommand"/> class.</summary>
        public CloseDatabaseConnectionCommand()
            : base("Close Current connection")
        {
        }

        /// <summary>Gets a value indicating whether Enabled.</summary>
        /// <value>The enabled state.</value>
        public override bool Enabled
        {
            get
            {
                if (Settings.Connection == null ||
                    (Settings.Connection.State == ConnectionState.Closed &&
                    Settings.Connection.State == ConnectionState.Broken))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>Execute the command.</summary>
        public override void Execute()
        {
            Settings.CloseConnection();
        }
    }
}