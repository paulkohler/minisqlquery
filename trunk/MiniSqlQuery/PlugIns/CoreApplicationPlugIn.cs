using System;
using System.Windows.Forms;
using MiniSqlQuery.Commands;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.PlugIns
{
    public class CoreApplicationPlugIn : PluginLoaderBase
    {
        private Timer _timer;

        public CoreApplicationPlugIn()
            : base("Mini SQL Query Core", "Plugin to to setup the core features of Mini SQL Query.", 1)
        {
        }

        public override void InitializePlugIn()
        {
            ToolStripMenuItem fileMenu = (ToolStripMenuItem) Services.HostWindow.Instance.MainMenuStrip.Items["fileMenu"];
            ToolStripMenuItem pluginsMenu = (ToolStripMenuItem) Services.HostWindow.Instance.MainMenuStrip.Items["pluginsMenu"];
            ToolStripMenuItem queryMenu = (ToolStripMenuItem) Services.HostWindow.Instance.MainMenuStrip.Items["queryToolStripMenuItem"];
            ToolStripMenuItem helpMenu = (ToolStripMenuItem) Services.HostWindow.Instance.MainMenuStrip.Items["helpMenu"];

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
            queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TurnEnableQueryBatchingOnCommand>());
            queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TurnEnableQueryBatchingOffCommand>());

            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowHelpCommand>());
            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowAboutCommand>());

            Services.HostWindow.AddPluginCommand<PasteAroundSelectionCommand>();
            Services.HostWindow.AddPluginCommand<SetLeftPasteAroundSelectionCommand>();
            Services.HostWindow.AddPluginCommand<SetRightPasteAroundSelectionCommand>();

            // get the new item and make in invisible - the shortcut key is still available etc ;-)
            //ToolStripMenuItem pluginsMenu = Services.HostWindow.GetMenuItem("plugins");
            ToolStripItem item = pluginsMenu.DropDownItems["SetLeftPasteAroundSelectionCommandToolStripMenuItem"];
            item.Visible = false;
            item = pluginsMenu.DropDownItems["SetRightPasteAroundSelectionCommandToolStripMenuItem"];
            item.Visible = false;

            Services.HostWindow.AddPluginCommand<GenerateCommandCodeCommand>();

            CommandControlBuilder.MonitorMenuItemsOpeningForEnabling(Services.HostWindow.Instance.MainMenuStrip);

            // toolstrip
            Services.HostWindow.AddToolStripCommand<NewQueryFormCommand>(0);
            Services.HostWindow.AddToolStripCommand<OpenFileCommand>(1);
            Services.HostWindow.AddToolStripCommand<SaveFileCommand>(2);
            Services.HostWindow.AddToolStripSeperator(3);
            Services.HostWindow.AddToolStripCommand<ExecuteQueryCommand>(4);
            Services.HostWindow.AddToolStripSeperator(5);
            Services.HostWindow.AddToolStripSeperator(null);
            Services.HostWindow.AddToolStripCommand<RefreshDatabaseConnectionCommand>(null);

            // watch tool strip enabled properties
            // by simply iterating each one every second or so we avoid the need to track via events
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += TimerTick;
            _timer.Enabled = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in Services.HostWindow.ToolStrip.Items)
            {
                ICommand cmd = item.Tag as ICommand;
                if (cmd != null)
                {
                    item.Enabled = cmd.Enabled;
                }
            }
        }

        public override void UnloadPlugIn()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }
    }
}