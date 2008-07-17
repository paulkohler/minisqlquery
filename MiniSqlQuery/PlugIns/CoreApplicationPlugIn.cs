using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using System.Windows.Forms;

namespace MiniSqlQuery.PlugIns
{
	public class CoreApplicationPlugIn : IPlugIn
	{
		private IApplicationServices _services;
		private Timer _timer;

		public void LoadPlugIn(IApplicationServices services)
		{
			_services = services;
		}

		public string PluginName
		{
			get
			{
				return "Mini SQL Query Core";
			}
		}

		public string PluginDescription
		{
			get
			{
				return "Plugin to to setup the core features of Mini SQL Query.";
			}
		}

		public int RequestedLoadOrder
		{
			get
			{
				return 1;
			}
		}

		public void InitializePlugIn()
		{
			ToolStripMenuItem fileMenu = (ToolStripMenuItem)_services.HostWindow.Instance.MainMenuStrip.Items["fileMenu"];
			ToolStripMenuItem pluginsMenu = (ToolStripMenuItem)_services.HostWindow.Instance.MainMenuStrip.Items["pluginsMenu"];
			ToolStripMenuItem queryMenu = (ToolStripMenuItem)_services.HostWindow.Instance.MainMenuStrip.Items["queryToolStripMenuItem"];
			ToolStripMenuItem helpMenu = (ToolStripMenuItem)_services.HostWindow.Instance.MainMenuStrip.Items["helpMenu"];

			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<NewQueryFormCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<OpenFileCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<SaveFileAsCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseActiveWindowCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<PrintCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ExitApplicationCommand>());

			queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<RefreshDatabaseConnectionCommand>());

			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowHelpCommand>());
			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowAboutCommand>());

			CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(_services.HostWindow.Instance.MainMenuStrip);

			// toolstrip
			_services.HostWindow.AddToolStripCommand<NewQueryFormCommand>(0);
			_services.HostWindow.AddToolStripCommand<OpenFileCommand>(1);
			_services.HostWindow.AddToolStripCommand<SaveFileCommand>(2);
			_services.HostWindow.AddToolStripSeperator(3);
			_services.HostWindow.AddToolStripCommand<ExecuteQueryCommand>(4);
			_services.HostWindow.AddToolStripSeperator(5);
			_services.HostWindow.AddToolStripSeperator(null);
			_services.HostWindow.AddToolStripCommand<RefreshDatabaseConnectionCommand>(null);

			// watch tool strip enabled properties
			_timer = new Timer();
			_timer.Interval = 1000;
			_timer.Tick += new EventHandler(_timer_Tick);
			_timer.Enabled = true;
		}

		void _timer_Tick(object sender, EventArgs e)
		{
			foreach (ToolStripItem item in _services.HostWindow.ToolStrip.Items)
			{
				ICommand cmd = item.Tag as ICommand;
				if (cmd != null)
				{
					item.Enabled = cmd.Enabled;
				}
			}
		}

		public void UnloadPlugIn()
		{
			if (_timer != null)
			{
				_timer.Dispose();
			}
		}
	}
}
