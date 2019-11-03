#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion


namespace MiniSqlQuery.Core
{
    /// <summary>
    /// 	An interface for simple plugins for use in extending Mini SQL Query.
    /// 	Plugins are loaded from DLL's in the working directory matching the pattern "*.PlugIn.dll"
    /// </summary>
    /// <remarks>
    /// 	<para>Plugins are created during the load process.
    /// 		After all plugins are loaded they are sorted by the <see cref = "RequestedLoadOrder" /> property.
    /// 		Next the <see cref = "LoadPlugIn" /> method is called on each in turn supplying a reference to<see cref = "IApplicationServices" />. 
    /// 		Next the main application form is displayed and after all control creation is complete (i.e. after the Form
    /// 		Shown event) a call to <see cref = "InitializePlugIn" /> is made for each loaded plugin.
    /// 		This is where <see cref = "ICommand" /> instances should be created and assigned to buttons, menus etc.
    /// 		These services provide access to the rest of the editor.
    /// 		As the main form is closing down, each plugins <see cref = "UnloadPlugIn" /> method is called.</para>
    /// 	<para>The <see cref = "PluginLoaderBase" /> class can be used to handle the basics of a plugin class to speed development.</para>
    /// </remarks>
    public interface IPlugIn
    {
        /// <summary>
        /// 	Gets a brief description of the plugin.
        /// </summary>
        /// <value>The plugin description.</value>
        string PluginDescription { get; }

        /// <summary>
        /// 	Gets the descriptive name of the plugin.
        /// </summary>
        /// <value>The plugin name.</value>
        string PluginName { get; }

        /// <summary>
        /// 	Gets the plugin load order. For external plugins start with values over 1000. 
        /// 	This is a simple way of handling dependencies of other services etc.
        /// </summary>
        /// <value>The requested load order.</value>
        int RequestedLoadOrder { get; }

        /// <summary>
        /// 	Initializes the plug in, called after the main form is displayed.
        /// </summary>
        void InitializePlugIn();

        /// <summary>
        /// 	Loads the plugin and stores a reference to the service container.
        /// 	Called at application startup time.
        /// </summary>
        /// <param name = "services">The service container, allows access to other serivces in the application.</param>
        void LoadPlugIn(IApplicationServices services);

        /// <summary>
        /// 	Called when the plugin is unloading (typically application shutdown).
        /// </summary>
        void UnloadPlugIn();
    }
}