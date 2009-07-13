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
			ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");

			// add the find to the plugins menu
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowFindTextFormCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<FindNextStringCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowGoToLineFormCommand>());

			// get the new item and make in invisible - the shortcut key is still available etc ;-)
			ToolStripItem item = editMenu.DropDownItems["FindNextStringCommandToolStripMenuItem"];
			item.Visible = false;

			// append the button the the toolbar items
			Services.HostWindow.AddToolStripSeperator(null);
			Services.HostWindow.AddToolStripCommand<ShowFindTextFormCommand>(null);
		}
	}
}
