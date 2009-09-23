using System;
using System.Collections.Generic;
using Castle.MicroKernel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// The core services of the application.
	/// </summary>
	public interface IApplicationServices
	{
		/// <summary>
		/// The Dependency Injection container.
		/// This container holds all major application components and plugins.
		/// See the "Configuration.xml" file in the main application for settings.
		/// </summary>
		IKernel Container { get; }

		/// <summary>
		/// The application host window.
		/// </summary>
		/// <value>The application host window - a <see cref="System.Windows.Forms.Form"/>.</value>
		IHostWindow HostWindow { get; }

		/// <summary>
		/// The application settings instance.
		/// </summary>
		/// <value>A reference to the settings handler.</value>
		IApplicationSettings Settings { get; }

		/// <summary>
		/// A dictionary of the current plugins for this application.
		/// </summary>
		/// <value>A reference to the plugin dictionary.</value>
		Dictionary<Type, IPlugIn> Plugins { get; }

		/// <summary>
		/// Registers the component service type <typeparamref name="TService"/> with and implemetation of type <typeparamref name="TImp"/>.
		/// </summary>
		/// <typeparam name="TService">The contract type.</typeparam>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		void RegisterComponent<TService, TImp>(string key);

		/// <summary>
		/// Registers the component implemetation of type <typeparamref name="TImp"/>.
		/// </summary>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		void RegisterComponent<TImp>(string key);

		/// <summary>
		/// Registers the component service type <typeparamref name="TService"/> with and implemetation of type <typeparamref name="TImp"/> as a singleton.
		/// </summary>
		/// <typeparam name="TService">The contract type.</typeparam>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		void RegisterSingletonComponent<TService, TImp>(string key);

		/// <summary>
		/// Resolves an instance of <typeparamref name="T"/> from the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key (can be null if not applicable).</param>
		/// <returns></returns>
		T Resolve<T>(string key);

		T Resolve<T>();

		/// <summary>
		/// Loads the <paramref name="plugIn"/> (calling its <see cref="IPlugIn.LoadPlugIn"/> method) and
		/// adds it to the <see cref="Plugins"/> dictionary for access by other services.
		/// </summary>
		/// <param name="plugIn">The plugin to load.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="plugIn"/> is null.</exception>
		void LoadPlugIn(IPlugIn plugIn);

		/// <summary>
		/// Initializes the plugins that have been loaded during application startup.
		/// </summary>
		void InitializePlugIns();

		/// <summary>
		/// Occurs when a system message is posted.
		/// </summary>
		event EventHandler<SystemMessageEventArgs> SystemMessagePosted;

		/// <summary>
		/// Posts a system message for listeners.
		/// </summary>
		/// <param name="message">A system message type.</param>
		/// <param name="data">The asssociated data.</param>
		void PostMessage(SystemMessage message, object data);

		/// <summary>
		/// Registers the editor of type <typeparamref name="TEditor"/> using the <see cref="FileEditorDescriptor.EditorKeyName"/>.
		/// </summary>
		/// <typeparam name="TEditor">The editor type.</typeparam>
		/// <param name="fileEditorDescriptor"></param>
		void RegisterEditor<TEditor>(FileEditorDescriptor fileEditorDescriptor) where TEditor : IEditor;


		void RegisterConfigurationObject<TConfig>() where TConfig : IConfigurationObject;

		Type[] GetConfigurationObjectTypes();
	}
}