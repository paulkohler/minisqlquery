using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Properties;
using System.Data.Common;
using MiniSqlQuery.Commands;
using WeifenLuo.WinFormsUI.Docking;
using Castle.Core;

namespace MiniSqlQuery
{
	public partial class MainForm : Form, IHostWindow
    {
		private IApplicationServices _services;
        private bool _initialized;
		private IDatabaseInspector _dbInspector;
		private string[] _arguements;

		public Form ActiveChildForm { get; internal set; }

		public MainForm(IApplicationServices services)
			: this()
		{
			_services = services;
		}

        public MainForm()
        {
            InitializeComponent();
			SetPointerState(Cursors.AppStarting);
		}

		private void toolStripComboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (_initialized)
            {
				DbConnectionDefinition dbConnectionDefinition = (DbConnectionDefinition)toolStripComboBoxConnection.SelectedItem;
				ApplicationServices.Instance.Settings.ConnectionDefinition = dbConnectionDefinition;
				SetWindowTitle(dbConnectionDefinition.Name);
			}
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
			_services.Settings.ConnectionDefinitionsChanged +=AppSettingsConnectionDefinitionsChanged;

			// TODO - check for old conn str file and no new one, offer upgrade
			Utility.CreateConnectionStringsIfRequired();
			_services.Settings.SetConnectionDefinitions(Utility.LoadDbConnectionDefinitions());
        }


		void AppSettingsConnectionDefinitionsChanged(object sender, EventArgs e)
		{
			LoadUpConnections();
		}

		void LoadUpConnections()
		{
			DbConnectionDefinitionList definitionList = _services.Settings.GetConnectionDefinitions();

			toolStripComboBoxConnection.Items.Clear();
			foreach (var connDef in definitionList.Definitions)
			{
				toolStripComboBoxConnection.Items.Add(connDef);
				if (connDef.ToString() == Settings.Default.NammedConnection)
				{
					toolStripComboBoxConnection.SelectedItem = connDef;
					_services.Settings.ConnectionDefinition = connDef;
					SetWindowTitle(connDef.Name);
				}
			}
		}

		void SetWindowTitle(string connectionName)
		{
			Text = string.Format("Mini SQL Query [{0}]", connectionName);
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
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
			if (_services.Settings.ConnectionDefinition != null)
			{
				Settings.Default.NammedConnection = _services.Settings.ConnectionDefinition.ToString();
				Settings.Default.Save();
			}

        	List<IPlugIn> plugins = new List<IPlugIn>(_services.Plugins.Values);
			plugins.Reverse();
			foreach (IPlugIn plugin in plugins)
			{
				plugin.UnloadPlugIn();
			}
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            ActiveChildForm = ActiveMdiChild;
        }

		#region IHostWindow Members

		public Form Instance
		{
			get
			{
				return this;
			}
		}

		public IDatabaseInspector DatabaseInspector
		{
			get
			{
				if (_dbInspector == null || (_dbInspector as Form).IsDisposed)
				{
					return null;
				}
				return _dbInspector;
			}
		}

		public ToolStrip ToolStrip
		{
			get
			{
				return toolStripConnection;
			}
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

		public void SetArguements(string[] args)
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
			if (_dbInspector != null)
			{
				_dbInspector.Close();
			}

			_dbInspector = databaseInspector;
			DockContent frm = _dbInspector as DockContent;
			if (frm == null)
			{
				throw new InvalidOperationException("The 'databaseInspector' must be a 'WeifenLuo.WinFormsUI.Docking.DockContent' based form.");
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
			ToolStripSeparator item = CommandControlBuilder.CreateToolStripMenuItemSeperator();
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
			this.Cursor = cursor;
			Application.DoEvents();
		}

		public DialogResult DisplaySimpleMessageBox(Form source, string text, string caption)
		{
			return MessageBox.Show(source, text, caption);
		}

		public DialogResult DisplayMessageBox(Form source, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			if (helpFilePath == null && keyword == null)
			{
				return MessageBox.Show(source, text, caption, buttons, icon, defaultButton, options);
			}
			else
			{
				return MessageBox.Show(source, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
			}
		}

		#endregion

	}
}
