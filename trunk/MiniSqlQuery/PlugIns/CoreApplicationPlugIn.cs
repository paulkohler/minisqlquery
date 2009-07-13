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
			ToolStripMenuItem fileMenu = Services.HostWindow.GetMenuItem("File");
			ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");
			ToolStripMenuItem pluginsMenu = Services.HostWindow.GetMenuItem("plugins");
			ToolStripMenuItem queryMenu = Services.HostWindow.GetMenuItem("query");
			ToolStripMenuItem helpMenu = Services.HostWindow.GetMenuItem("help");

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
			queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<CloseDatabaseConnectionCommand>());
            queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TurnEnableQueryBatchingOnCommand>());
            queryMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TurnEnableQueryBatchingOffCommand>());

			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<PasteAroundSelectionCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<SetLeftPasteAroundSelectionCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<SetRightPasteAroundSelectionCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<OpenConnectionFileCommand>());
			editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateCommandCodeCommand>());

			// get the new item and make in invisible - the shortcut key is still available etc ;-)
			ToolStripItem item = editMenu.DropDownItems["SetLeftPasteAroundSelectionCommandToolStripMenuItem"];
			item.Visible = false;
			item = editMenu.DropDownItems["SetRightPasteAroundSelectionCommandToolStripMenuItem"];
			item.Visible = false;


            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowHelpCommand>());
			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowWebPageCommand>());
			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowDiscussionsWebPageCommand>());
			helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<EmailAuthorCommand>());
            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItemSeperator());
            helpMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<ShowAboutCommand>());

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