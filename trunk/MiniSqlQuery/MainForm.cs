#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery
{
	/// <summary>The main form.</summary>
	public partial class MainForm : Form, IHostWindow
	{
		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _settings.</summary>
		private readonly IApplicationSettings _settings;

		/// <summary>The _arguements.</summary>
		private string[] _arguements;

		/// <summary>The _db inspector.</summary>
		private IDatabaseInspector _dbInspector;

		/// <summary>The _initialized.</summary>
		private bool _initialized;

		/// <summary>Initializes a new instance of the <see cref="MainForm"/> class.</summary>
		public MainForm()
		{
			InitializeComponent();
			SetPointerState(Cursors.AppStarting);
		}

		/// <summary>Initializes a new instance of the <see cref="MainForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="settings">The settings.</param>
		public MainForm(IApplicationServices services, IApplicationSettings settings)
			: this()
		{
			_services = services;
			_settings = settings;

			AllowDrop = true;
			DragEnter += WindowDragEnter;
			DragDrop += WindowDragDrop;
		}

		/// <summary>Gets or sets ActiveChildForm.</summary>
		public Form ActiveChildForm { get; internal set; }

		/// <summary>Gets DatabaseInspector.</summary>
		public IDatabaseInspector DatabaseInspector
		{
			get
			{
				if (_dbInspector == null || ((Form)_dbInspector).IsDisposed)
				{
					return null;
				}

				return _dbInspector;
			}
		}

		/// <summary>Gets Instance.</summary>
		public Form Instance
		{
			get { return this; }
		}

		/// <summary>Gets ToolStrip.</summary>
		[DebuggerNonUserCode]
		public ToolStrip ToolStrip
		{
			get { return toolStripConnection; }
		}


		/// <summary>The add plugin command.</summary>
		/// <typeparam name="TCommand"></typeparam>
		public void AddPluginCommand<TCommand>() where TCommand : ICommand, new()
		{
			pluginsMenu.DropDownItems.Add(CommandControlBuilder.CreateToolStripMenuItem<TCommand>());
		}

		/// <summary>The add tool strip command.</summary>
		/// <param name="index">The index.</param>
		/// <typeparam name="TCommand"></typeparam>
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

		/// <summary>The add tool strip seperator.</summary>
		/// <param name="index">The index.</param>
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

		/// <summary>The display docked form.</summary>
		/// <param name="frm">The frm.</param>
		public void DisplayDockedForm(DockContent frm)
		{
			if (frm != null)
			{
				frm.Show(dockPanel, DockState.Document);
			}
		}

		/// <summary>The display message box.</summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="icon">The icon.</param>
		/// <param name="defaultButton">The default button.</param>
		/// <param name="options">The options.</param>
		/// <param name="helpFilePath">The help file path.</param>
		/// <param name="keyword">The keyword.</param>
		/// <returns></returns>
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

		/// <summary>The display simple message box.</summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public DialogResult DisplaySimpleMessageBox(Form source, string text, string caption)
		{
			return MessageBox.Show(source, text, caption);
		}

		/// <summary>The get menu item.</summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
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

		/// <summary>The set arguments.</summary>
		/// <param name="args">The args.</param>
		public void SetArguments(string[] args)
		{
			_arguements = args;
		}

		/// <summary>The set pointer state.</summary>
		/// <param name="cursor">The cursor.</param>
		public void SetPointerState(Cursor cursor)
		{
			Cursor = cursor;
			Application.DoEvents();
		}

		/// <summary>The set status.</summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
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

		/// <summary>The show database inspector.</summary>
		/// <param name="databaseInspector">The database inspector.</param>
		/// <param name="dockState">The dock state.</param>
		/// <exception cref="InvalidOperationException"></exception>
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

		/// <summary>The show tool window.</summary>
		/// <param name="form">The form.</param>
		/// <param name="dockState">The dock state.</param>
		public void ShowToolWindow(DockContent form, DockState dockState)
		{
			form.Show(dockPanel, dockState);
		}

		/// <summary>The app settings connection definitions changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void AppSettingsConnectionDefinitionsChanged(object sender, EventArgs e)
		{
			bool load = true;

			if (_initialized)
			{
				DialogResult result = MessageBox.Show(this, 
				                                      Resources.The_connections_have_changed__would_you_like_to_refresh_the_database_connection, 
				                                      Resources.Reload_Connection, 
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

		/// <summary>The load up connections.</summary>
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

		/// <summary>The main form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The main form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			_settings.ConnectionDefinitionsChanged += AppSettingsConnectionDefinitionsChanged;

			Utility.CreateConnectionStringsIfRequired();
			_settings.SetConnectionDefinitions(Utility.LoadDbConnectionDefinitions());
		}

		/// <summary>The main form_ mdi child activate.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void MainForm_MdiChildActivate(object sender, EventArgs e)
		{
			ActiveChildForm = ActiveMdiChild;
		}

		/// <summary>The main form_ shown.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The set window title.</summary>
		/// <param name="connectionName">The connection name.</param>
		private void SetWindowTitle(string connectionName)
		{
			Text = string.Format("Mini SQL Query [{0}]", connectionName);
		}

		/// <summary>The window drag drop.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void WindowDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
				IFileEditorResolver resolver = _services.Resolve<IFileEditorResolver>();
				foreach (string filename in filePaths)
				{
					// todo: check for file exist file in open windows;
					IEditor editor = resolver.ResolveEditorInstance(filename);
					editor.FileName = filename;
					editor.LoadFile();
					DisplayDockedForm(editor as DockContent);
				}
			}
		}

		/// <summary>The window drag enter.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The exit tool strip menu item_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CommandManager.GetCommandInstance<ExitApplicationCommand>().Execute();
		}

		/// <summary>The hide show tool strip menu item_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The sys icon_ double click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The tool strip combo box connection_ selected index changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripComboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_initialized)
			{
				DbConnectionDefinition dbConnectionDefinition = (DbConnectionDefinition)toolStripComboBoxConnection.SelectedItem;
				_settings.ConnectionDefinition = dbConnectionDefinition;
				SetWindowTitle(dbConnectionDefinition.Name);
			}
		}
	}
}