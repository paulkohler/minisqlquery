using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery
{
	public partial class MainForm : Form, IHostWindow
	{
		private string[] _arguements;
		private IDatabaseInspector _dbInspector;
		private bool _initialized;
		private readonly IApplicationServices _services;
		private readonly IApplicationSettings _settings;

		public MainForm()
		{
			InitializeComponent();
			SetPointerState(Cursors.AppStarting);
		}

		public MainForm(IApplicationServices services, IApplicationSettings settings)
			: this()
		{
			_services = services;
			_settings = settings;

			AllowDrop = true;
			DragEnter += WindowDragEnter;
			DragDrop += WindowDragDrop;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_settings.ConnectionDefinitionsChanged += AppSettingsConnectionDefinitionsChanged;

			Utility.CreateConnectionStringsIfRequired();
			_settings.SetConnectionDefinitions(Utility.LoadDbConnectionDefinitions());
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			_services.InitializePlugIns();
			DockContent dbInspectorForm = _dbInspector as DockContent;

			if (dbInspectorForm != null)
			{
				// the activate for "DockContent" is different to that of "Form".
				dbInspectorForm.Activate();
			}

			_initialized = true;
			SetPointerState(Cursors.Default);
			SetStatus(null, string.Empty);

			// now check for command line args that are "command type names"
			if (_arguements != null && _arguements.Length > 0)
			{
				foreach (string arg in _arguements)
				{
					if (arg.StartsWith("/cmd:"))
					{
						string cmdName = arg.Substring(5);
						ICommand cmd = CommandManager.GetCommandInstanceByPartialName(cmdName);
						if (cmd != null)
						{
							cmd.Execute();
						}
					}
				}
			}
			else
			{
				CommandManager.GetCommandInstance<NewQueryFormCommand>().Execute();
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.Cancel)
			{
				return;
			}

			if (_settings.ConnectionDefinition != null)
			{
				Settings.Default.NammedConnection = _settings.ConnectionDefinition.Name;
				Settings.Default.Save();
			}

			List<IPlugIn> plugins = new List<IPlugIn>(_services.Plugins.Values);
			plugins.Reverse();
			foreach (IPlugIn plugin in plugins)
			{
				plugin.UnloadPlugIn();
			}

			_services.Container.Dispose();
		}

		private void MainForm_MdiChildActivate(object sender, EventArgs e)
		{
			ActiveChildForm = ActiveMdiChild;
		}

		#region IHostWindow Members

		public Form ActiveChildForm { get; internal set; }

		public IDatabaseInspector DatabaseInspector
		{
			get
			{
				if (_dbInspector == null || ((Form) _dbInspector).IsDisposed)
				{
					return null;
				}
				return _dbInspector;
			}
		}

		[System.Diagnostics.DebuggerNonUserCode]
		public ToolStrip ToolStrip
		{
			get { return toolStripConnection; }
		}

		public Form Instance
		{
			get { return this; }
		}

		public ToolStripMenuItem GetMenuItem(string name)
		{
			ToolStripMenuItem menuItem = null;

			foreach (ToolStripMenuItem item in MainMenuStrip.Items)
			{
				if (item.Text.Replace("&", string.Empty).ToLower() == name.ToLower())
				{
					menuItem = item;
					break;
				}
			}

			return menuItem;
		}


		public void SetStatus(Form source, string text)
		{
			if (source == null || ActiveMdiChild == source)
			{
				if (text != null)
				{
					text = text.Replace("\r", string.Empty).Replace("\n", "  ");
				}
				toolStripStatusLabel.Text = text;
			}
		}

		public void SetArguments(string[] args)
		{
			_arguements = args;
		}

		public void DisplayDockedForm(DockContent frm)
		{
			if (frm != null)
			{
				frm.Show(dockPanel, DockState.Document);
			}
		}

		public void ShowDatabaseInspector(IDatabaseInspector databaseInspector, DockState dockState)
		{
			if (_dbInspector != null && _dbInspector != databaseInspector)
			{
				_dbInspector.Close();
			}

			_dbInspector = databaseInspector;
			DockContent frm = _dbInspector as DockContent;
			if (frm == null)
			{
				throw new InvalidOperationException(
					"The 'databaseInspector' must be a 'WeifenLuo.WinFormsUI.Docking.DockContent' based form.");
			}
			frm.Show(dockPanel, dockState);
		}

		public void ShowToolWindow(DockContent form, DockState dockState)
		{
			form.Show(dockPanel, dockState);
		}


		public void AddPluginCommand<TCommand>() where TCommand : ICommand, new()
		{
			pluginsMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TCommand>());
		}

		public void AddToolStripCommand<TCommand>(int? index) where TCommand : ICommand, new()
		{
			ToolStripButton item = CommandControlBuilder.CreateToolStripButton<TCommand>();
			if (index == null)
			{
				toolStripConnection.Items.Add(item);
			}
			else
			{
				toolStripConnection.Items.Insert(index.Value, item);
			}
		}

		public void AddToolStripSeperator(int? index)
		{
			ToolStripSeparator item = CommandControlBuilder.CreateToolStripMenuItemSeparator();
			if (index == null)
			{
				toolStripConnection.Items.Add(item);
			}
			else
			{
				toolStripConnection.Items.Insert(index.Value, item);
			}
		}

		public void SetPointerState(Cursor cursor)
		{
			Cursor = cursor;
			Application.DoEvents();
		}

		public DialogResult DisplaySimpleMessageBox(Form source, string text, string caption)
		{
			return MessageBox.Show(source, text, caption);
		}

		public DialogResult DisplayMessageBox(
			Form source, string text, string caption, MessageBoxButtons buttons,
			MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
			MessageBoxOptions options, string helpFilePath, string keyword)
		{
			if (helpFilePath == null && keyword == null)
			{
				return MessageBox.Show(source, text, caption, buttons, icon, defaultButton, options);
			}
			return MessageBox.Show(source, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
		}

		#endregion

		private void AppSettingsConnectionDefinitionsChanged(object sender, EventArgs e)
		{
			bool load = true;

			if (_initialized)
			{

				DialogResult result = MessageBox.Show(this,
				                                      "The connections have changed, would you like to refresh the database connection?",
				                                      "Reload Connection?",
				                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				                                      MessageBoxDefaultButton.Button1);
				if (result != DialogResult.Yes)
				{
					load = false;
				}
			}

			if (load)
			{
				LoadUpConnections();
			}
		}

		private void LoadUpConnections()
		{
			DbConnectionDefinitionList definitionList = _settings.GetConnectionDefinitions();

			// TODO - prevent the "reset"
			toolStripComboBoxConnection.Items.Clear();
			foreach (var connDef in definitionList.Definitions)
			{
				toolStripComboBoxConnection.Items.Add(connDef);
				if (connDef.Name == Settings.Default.NammedConnection)
				{
					toolStripComboBoxConnection.SelectedItem = connDef;
					_settings.ConnectionDefinition = connDef;
					SetWindowTitle(connDef.Name);
				}
			}
		}

		private void SetWindowTitle(string connectionName)
		{
			Text = string.Format("Mini SQL Query [{0}]", connectionName);
		}

		private void toolStripComboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_initialized)
			{
				DbConnectionDefinition dbConnectionDefinition = (DbConnectionDefinition) toolStripComboBoxConnection.SelectedItem;
				_settings.ConnectionDefinition = dbConnectionDefinition;
				SetWindowTitle(dbConnectionDefinition.Name);
			}
		}

		private void sysIcon_DoubleClick(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
			{
				Hide();
				WindowState = FormWindowState.Minimized;
			}
			else
			{
				Show();
				WindowState = FormWindowState.Normal;
			}
		}

		private void hideShowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
			{
				Hide();
				WindowState = FormWindowState.Minimized;
			}
			else
			{
				Show();
				WindowState = FormWindowState.Normal;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CommandManager.GetCommandInstance<ExitApplicationCommand>().Execute();
		}

		private void WindowDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void WindowDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
				IFileEditorResolver resolver = _services.Resolve<IFileEditorResolver>();
				foreach (string filename in filePaths)
				{
					//todo: check for file exist file in open windows;
					IEditor editor = resolver.ResolveEditorInstance(filename);
					editor.FileName = filename;
					editor.LoadFile();
					DisplayDockedForm(editor as DockContent);
				}
			}
		}
	}
}