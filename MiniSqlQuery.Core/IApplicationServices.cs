using System;
using System.Collections.Generic;
using System.Text;
using MiniSqlQuery.Core;
using Castle.Windsor;

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
		IWindsorContainer Container { get; }

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
	}
}
