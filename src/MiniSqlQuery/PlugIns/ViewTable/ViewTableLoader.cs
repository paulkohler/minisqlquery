#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.ViewTable.Commands;

namespace MiniSqlQuery.PlugIns.ViewTable
{
    /// <summary>The view table loader.</summary>
    public class ViewTableLoader : PluginLoaderBase
    {
        /// <summary>Initializes a new instance of the <see cref="ViewTableLoader"/> class.</summary>
        public ViewTableLoader()
            : base("View Table Data", "A Mini SQL Query Plugin for viewing table data.", 50)
        {
        }

        /// <summary>Iinitialize the plug in.</summary>
        public override void InitializePlugIn()
        {
            Services.RegisterComponent<IViewTable, ViewTableForm>("ViewTableForm");

            // the DB inspector may not be present
            if (Services.HostWindow.DatabaseInspector != null)
            {
                Services.HostWindow.DatabaseInspector.TableMenu.Items.Insert(
                    0, CommandControlBuilder.CreateToolStripMenuItem<ViewTableFromInspectorCommand>());
            }

            Services.HostWindow.AddPluginCommand<ViewTableFormCommand>();
        }
    }
}