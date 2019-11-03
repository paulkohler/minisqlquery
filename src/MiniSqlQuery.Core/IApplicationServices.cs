#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using Ninject;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	The core services of the application.
	/// </summary>
	public interface IApplicationServices
	{
		/// <summary>
		/// 	Occurs when a system message is posted.
		/// </summary>
		event EventHandler<SystemMessageEventArgs> SystemMessagePosted;

		/// <summary>
		/// 	Gets the Dependency Injection container.
		/// 	This container holds all major application components and plugins.
		/// 	See the "Configuration.xml" file in the main application for settings.
		/// </summary>
		/// <value>The container.</value>
		IKernel Container { get; }

		/// <summary>
		/// 	Gets the application host window.
		/// </summary>
		/// <value>The application host window - a <see cref = "System.Windows.Forms.Form" />.</value>
		IHostWindow HostWindow { get; }

		/// <summary>
		/// 	Gets a dictionary of the current plugins for this application.
		/// </summary>
		/// <value>A reference to the plugin dictionary.</value>
		Dictionary<Type, IPlugIn> Plugins { get; }

		/// <summary>
		/// 	Gets the application settings instance.
		/// </summary>
		/// <value>A reference to the settings handler.</value>
		IApplicationSettings Settings { get; }

		/// <summary>
		/// 	The get configuration object types.
		/// </summary>
		/// <returns>An array of configuration objects.</returns>
		Type[] GetConfigurationObjectTypes();

		/// <summary>
		/// 	Initializes the plugins that have been loaded during application startup.
		/// </summary>
		void InitializePlugIns();

		/// <summary>
		/// 	Loads the <paramref name = "plugIn" /> (calling its <see cref = "IPlugIn.LoadPlugIn" /> method) and
		/// 	adds it to the <see cref = "Plugins" /> dictionary for access by other services.
		/// </summary>
		/// <param name = "plugIn">The plugin to load.</param>
		/// <exception cref = "ArgumentNullException">If <paramref name = "plugIn" /> is null.</exception>
		void LoadPlugIn(IPlugIn plugIn);

		/// <summary>
		/// 	Posts a system message for listeners.
		/// </summary>
		/// <param name = "message">A system message type.</param>
		/// <param name = "data">The asssociated data.</param>
		void PostMessage(SystemMessage message, object data);

		/// <summary>
		/// 	Registers the component service type <typeparamref name = "TService" /> with and implemetation of type <typeparamref name = "TImp" />.
		/// </summary>
		/// <typeparam name = "TService">The contract type.</typeparam>
		/// <typeparam name = "TImp">The implementing type.</typeparam>
		/// <param name = "key">The key or name of the service.</param>
		void RegisterComponent<TService, TImp>(string key);

		/// <summary>
		/// 	Registers the component implemetation of type <typeparamref name = "TImp" />.
		/// </summary>
		/// <typeparam name = "TImp">The implementing type.</typeparam>
		/// <param name = "key">The key or name of the service.</param>
		void RegisterComponent<TImp>(string key);

		/// <summary>
		/// 	The register configuration object.
		/// </summary>
		/// <typeparam name = "TConfig">A configuration class.</typeparam>
		void RegisterConfigurationObject<TConfig>() where TConfig : IConfigurationObject;

		/// <summary>
		/// 	Registers the editor of type <typeparamref name = "TEditor" /> using the <see cref = "FileEditorDescriptor.EditorKeyName" />.
		/// </summary>
		/// <typeparam name = "TEditor">The editor type.</typeparam>
		/// <param name = "fileEditorDescriptor">The file editor descriptor.</param>
		void RegisterEditor<TEditor>(FileEditorDescriptor fileEditorDescriptor) where TEditor : IEditor;

		/// <summary>
		/// 	Registers the component service type <typeparamref name = "TService" /> with and implemetation of type <typeparamref name = "TImp" /> as a singleton.
		/// </summary>
		/// <typeparam name = "TService">The contract type.</typeparam>
		/// <typeparam name = "TImp">The implementing type.</typeparam>
		/// <param name = "key">The key or name of the service.</param>
		void RegisterSingletonComponent<TService, TImp>(string key);

		/// <summary>
		/// 	Resolves an instance of <typeparamref name = "T" /> from the container.
		/// </summary>
		/// <typeparam name = "T">The type to find in the container.</typeparam>
		/// <param name = "key">The key (can be null if not applicable).</param>
		/// <returns>An instance of the type depending on the containters configuration.</returns>
		T Resolve<T>(string key);

		/// <summary>
		/// 	The resolve.
		/// </summary>
		/// <typeparam name = "T">The type to find in the container.</typeparam>
		/// <returns>An instance of the type depending on the containters configuration.</returns>
		T Resolve<T>();

        /// <summary>
        /// Remove the component by name.
        /// </summary>
        /// <returns>True on success.</returns>
	    void RemoveComponent<TImp>();
	}
}