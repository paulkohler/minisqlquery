#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// An interface for simple plugins for use in extending Mini SQL Query.
	/// Plugins are loaded from DLL's in the working directory matching the pattern "*.PlugIn.dll"
	/// </summary>
	/// <remarks>
	/// <para>
	/// Plugins are created during the load process.
	/// After all plugins are loaded they are sorted by the <see cref="RequestedLoadOrder"/> property.
	/// Next the <see cref="LoadPlugIn"/> method is called on each in turn supplying a reference to
	/// <see cref="IApplicationServices"/>. 
	/// Next the main application form is displayed and after all control creation is complete (i.e. after the Form
	/// Shown event) a call to <see cref="InitializePlugIn"/> is made for each loaded plugin.
	/// This is where <see cref="ICommand"/> instances should be created and assigned to buttons, menus etc.
	/// These services provide access to the rest of the editor.
	/// As the main form is closing down, each plugins <see cref="UnloadPlugIn"/> method is called.
	/// </para>
	/// <para>
	/// The <see cref="PluginLoaderBase"/> class can be used to handle the basics of a plugin class to speed development.
	/// </para>
	/// </remarks>
	public interface IPlugIn
	{
		/// <summary>
		/// Loads the plugin and stores a reference to the service container.
		/// Called at application startup time.
		/// </summary>
		/// <param name="services">The service container, allows access to other serivces in the application.</param>
		void LoadPlugIn(IApplicationServices services);

		/// <summary>
		/// Initializes the plug in, called after the main form is displayed.
		/// </summary>
		void InitializePlugIn();

		/// <summary>
		/// Called when the plugin is unloading (typically application shutdown).
		/// </summary>
		void UnloadPlugIn();

		/// <summary>
		/// Plugin load order. For external plugins start with values over 1000. 
		/// This is a simple way of handling dependencies of other services etc.
		/// </summary>
		int RequestedLoadOrder { get; }

		/// <summary>
		/// The descriptive name of the plugin.
		/// </summary>
		string PluginName { get; }

		/// <summary>
		/// A brief description of the plugin.
		/// </summary>
		string PluginDescription { get; }
	}
}
