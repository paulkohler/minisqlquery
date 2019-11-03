#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.ConnectionStringsManager.Commands;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
    /// <summary>The connection strings manager loader.</summary>
    public class ConnectionStringsManagerLoader : PluginLoaderBase
    {
        /// <summary>Initializes a new instance of the <see cref="ConnectionStringsManagerLoader"/> class.</summary>
        public ConnectionStringsManagerLoader() : base(
            "Connection String Manager",
            "A Mini SQL Query Plugin for managing the list of connection strings.",
            10)
        {
        }

        /// <summary>Iinitialize the plug in.</summary>
        public override void InitializePlugIn()
        {
            Services.RegisterComponent<DbConnectionsForm>("DbConnectionsForm");
            ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");
            editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<EditConnectionsFormCommand>());
            Services.HostWindow.AddToolStripCommand<EditConnectionsFormCommand>(null);
        }
    }
}