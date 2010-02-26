#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Castle.Core;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// Core functions of the main MDI application host Form.
	/// </summary>
	public interface IHostWindow
	{
		/// <summary>
		/// The instance of the host form.
		/// </summary>
		Form Instance { get; }

		/// <summary>
		/// A reference to the database inspector window if open.
		/// </summary>
		/// <value>A <see cref="IDatabaseInspector"/> object or null.</value>
		IDatabaseInspector DatabaseInspector { get; }

		/// <summary>
		/// A reference to the active child form.
		/// </summary>
		/// <value>The active form or null.</value>
		Form ActiveChildForm { get; }

		/// <summary>
		/// Sets the status text of the host.
		/// </summary>
		/// <param name="source">The source form, for tracking MDI children.</param>
		/// <param name="text">The text to set.</param>
		void SetStatus(Form source, string text);

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.</summary>
		/// <param name="source">The source form of the message.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="keyword">The Help keyword to display when the user clicks the Help button. </param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		/// <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		DialogResult DisplayMessageBox(Form source, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword);

		/// <summary>Displays an "OK" message box with the specified text and caption.</summary>
		/// <param name="source">The source form of the message.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		DialogResult DisplaySimpleMessageBox(Form source, string text, string caption);

		///// <summary>
		///// Plays the system beep.
		///// </summary>
		//void Beep();

		/// <summary>
		/// Sets the application cursor to <paramref name="cursor"/>.
		/// </summary>
		/// <param name="cursor">The new cursor mode.</param>
		void SetPointerState(Cursor cursor);

		/// <summary>
		/// A testable way to pass command line arguements to the application.
		/// </summary>
		/// <param name="args">An array of command line arguements.</param>
		void SetArguments(string[] args);

		/// <summary>
		/// Displays the <paramref name="frm"/> in the host window.
		/// </summary>
		/// <param name="frm">The child form to dock.</param>
		void DisplayDockedForm(DockContent frm);

		/// <summary>
		/// Displays (and replaces if required) the database inspactor window.
		/// </summary>
		/// <param name="databaseInspector">The window to display.</param>
		/// <param name="dockState">The state for the window.</param>
		void ShowDatabaseInspector(IDatabaseInspector databaseInspector, DockState dockState);

		/// <summary>
		/// Displays a "tool" window, like the database inspector etc.
		/// </summary>
		/// <param name="form"></param>
		/// <param name="dockState"></param>
		void ShowToolWindow(DockContent form, DockState dockState);

		/// <summary>
		/// Provides access to the host windows tool strip control.
		/// </summary>
		/// <value>The window tool strip.</value>
		ToolStrip ToolStrip	{ get; }

		/// <summary>
		/// Gets the relevent menu item by name.
		/// </summary>
		/// <param name="name">The name of the menu to get, e.g. "Plugins" or "File" (no amphersand required).</param>
		ToolStripMenuItem GetMenuItem(string name);

		/// <summary>
		/// Adds an <see cref="ICommand"/> to the plugins menu.
		/// </summary>
		/// <typeparam name="TCommand">The command implementation to direct the name, image etc of the new menu item.</typeparam>
		void AddPluginCommand<TCommand>() where TCommand : ICommand, new();

		/// <summary>
		/// Adds a command based button to the tool strip by <paramref name="index"/>.
		/// </summary>
		/// <typeparam name="TCommand">The command implementation to direct the name, image etc of the new tool strip item.</typeparam>
		/// <param name="index">The position for the tool strip button, if null the item is appended to the end.</param>
		void AddToolStripCommand<TCommand>(int? index) where TCommand : ICommand, new();

		/// <summary>
		/// Adds a seperator to the tool strip by <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The position for the seperator, if null the item is appended to the end.</param>
		void AddToolStripSeperator(int? index);
	}
}
