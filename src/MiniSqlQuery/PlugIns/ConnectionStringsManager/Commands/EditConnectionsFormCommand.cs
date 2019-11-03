#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager.Commands
{
    /// <summary>The edit connections form command.</summary>
    public class EditConnectionsFormCommand : CommandBase
    {
        /// <summary>Initializes a new instance of the <see cref="EditConnectionsFormCommand"/> class.</summary>
        public EditConnectionsFormCommand()
            : base("&Edit Connection Strings")
        {
            SmallImage = ImageResource.database_edit;
        }

        /// <summary>Execute the command.</summary>
        public override void Execute()
        {
            DbConnectionsForm frm = Services.Resolve<DbConnectionsForm>();
            frm.ShowDialog(HostWindow.Instance);
        }
    }
}