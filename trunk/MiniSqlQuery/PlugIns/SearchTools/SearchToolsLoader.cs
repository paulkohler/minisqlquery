using System;
using MiniSqlQuery.PlugIns.SearchTools.Commands;
using MiniSqlQuery.Core;
using System.Windows.Forms;

namespace MiniSqlQuery.PlugIns.SearchTools
{
	public class SearchToolsLoader : PluginLoaderBase
	{
		public SearchToolsLoader()
			: base(
				"Mini SQL Query Search Tools",
				"Text searching tools - generic find text tool window.", 
				50)
		{
		}

		public override void InitializePlugIn()
		{
			// add the find to the plugins menu
			Services.HostWindow.AddPluginCommand<ShowFindTextFormCommand>();
			Services.HostWindow.AddPluginCommand<FindNextStringCommand>();
			Services.HostWindow.AddPluginCommand<ShowGoToLineFormCommand>();

			// get the new item and make in invisible - the shortcut key is still available etc ;-)
			ToolStripMenuItem pluginsMenu = Services.HostWindow.GetMenuItem("plugins");
			ToolStripItem item = pluginsMenu.DropDownItems["FindNextStringCommandToolStripMenuItem"];
			item.Visible = false;

			// append the button the the toolbar items
			Services.HostWindow.AddToolStripSeperator(null);
			Services.HostWindow.AddToolStripCommand<ShowFindTextFormCommand>(null);
		}
	}
}
