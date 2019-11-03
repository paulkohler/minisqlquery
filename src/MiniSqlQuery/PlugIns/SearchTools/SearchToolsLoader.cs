#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.PlugIns.SearchTools.Commands;

namespace MiniSqlQuery.PlugIns.SearchTools
{
    /// <summary>The search tools loader.</summary>
    public class SearchToolsLoader : PluginLoaderBase
    {
        /// <summary>Initializes a new instance of the <see cref="SearchToolsLoader"/> class.</summary>
        public SearchToolsLoader()
            : base(
                "Mini SQL Query Search Tools",
                "Text searching tools - generic find text tool window.",
                50)
        {
        }

        /// <summary>Iinitialize the plug in.</summary>
        public override void InitializePlugIn()
        {
            Services.RegisterComponent<GoToLineForm>("GoToLineForm");

            ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");

            // add the find to the plugins menu
            editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowFindTextFormCommand>());
            editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<FindNextStringCommand>());
            editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ReplaceStringCommand>());
            editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowGoToLineFormCommand>());

            // get the new item and make in invisible - the shortcut key is still available etc ;-)
            ToolStripItem item = editMenu.DropDownItems["FindNextStringCommandToolStripMenuItem"];
            item.Visible = false;
            item = editMenu.DropDownItems["ReplaceStringCommandToolStripMenuItem"];
            item.Visible = false;

            // append the button the the toolbar items
            Services.HostWindow.AddToolStripSeperator(null);
            Services.HostWindow.AddToolStripCommand<ShowFindTextFormCommand>(null);
        }
    }
}