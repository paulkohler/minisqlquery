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
            : base("Mini SQL Query Core", "Plugin to setup the core features of Mini SQL Query.", 1)
        {
        }

        public override void InitializePlugIn()
        {
			Services.RegisterEditor<BasicEditor>("default-editor");
			Services.RegisterEditor<QueryForm>("sql-editor", "sql");
			Services.RegisterEditor<BasicEditor>("cs-editor", "cs");
			Services.RegisterEditor<BasicEditor>("vb-editor", "vb");
			Services.RegisterEditor<BasicEditor>("xml-editor", "xml");
			//Services.RegisterEditor<BasicEditor>("htm-editor", "htm", "html");
			Services.RegisterEditor<BasicEditor>("htm-editor", "htm");
			Services.RegisterEditor<BasicEditor>("html-editor", "html");
			Services.RegisterEditor<BasicEditor>("txt-editor", "txt");

			ToolStripMenuItem fileMenu = Services.HostWindow.GetMenuItem("File");
			ToolStripMenuItem editMenu = Services.HostWindow.GetMenuItem("edit");
			ToolStripMenuItem queryMenu = Services.HostWindow.GetMenuItem("query");
			ToolStripMenuItem helpMenu = Services.HostWindow.GetMenuItem("help");

            fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<NewQueryFormCommand>());
			fileMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<NewFileCommand>());
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
			//editMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<GenerateCommandCodeCommand>());

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

			Services.HostWindow.AddPluginCommand<InsertGuidCommand>();

            // watch tool strip enabled properties
            // by simply iterating each one every second or so we avoid the need to track via events
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += TimerTick;
            _timer.Enabled = true;
        }

		/// <summary>
		/// Called frequently to run through all the commands on the toolstrip and check their enabled state. 
		/// Marked as "DebuggerNonUserCode" because it can get in the way of a debug sesssion.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		[System.Diagnostics.DebuggerNonUserCode]
        private void TimerTick(object sender, EventArgs e)
        {
			try
			{
				_timer.Enabled = false;
				foreach (ToolStripItem item in Services.HostWindow.ToolStrip.Items)
				{
					ICommand cmd = item.Tag as ICommand;
					if (cmd != null)
					{
						item.Enabled = cmd.Enabled;
					}
				}
			}
			finally
			{
				_timer.Enabled = true;
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