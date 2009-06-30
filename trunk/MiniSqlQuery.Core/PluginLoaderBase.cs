using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A simple base class to use for implementing the <see cref="IPlugIn"/> interface.
	/// </summary>
	public abstract class PluginLoaderBase : IPlugIn
	{
		/// <summary>
		/// Creates a new instance of a plugin loader class.
		/// </summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <remarks>
		/// The <see cref="RequestedLoadOrder"/> defaults to 1000.
		/// </remarks>
		protected PluginLoaderBase(string name, string description)
			: this(name, description, 1000)
		{
		}

		/// <summary>
		/// Creates a new instance of a plugin loader class.
		/// </summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <param name="requestedLoadOrder">The requested load order. See <see cref="IPlugIn.RequestedLoadOrder"/>.</param>
		protected PluginLoaderBase(string name, string description, int requestedLoadOrder)
		{
			PluginName = name;
			PluginDescription = description;
			RequestedLoadOrder = requestedLoadOrder;
		}

		/// <summary>
		/// A reference to the applications service manager.
		/// </summary>
		public IApplicationServices Services { get; private set; }

		#region IPlugIn Members

		/// <summary>
		/// Called when the plugins are loading during application startup. 
		/// Stores the reference to <paramref name="services"/>.
		/// </summary>
		/// <param name="services">The application services intance.</param>
		public void LoadPlugIn(IApplicationServices services)
		{
			Services = services;
		}

		/// <summary>
		/// Must be implemented by the inheriting class. 
		/// Called after the application has started.
		/// </summary>
		public abstract void InitializePlugIn();

		/// <summary>
		/// Called as the application is shutting down.
		/// </summary>
		/// <remarks>
		/// In most cases there is probably no need to do anything here, all controls etc created
		/// will be disposed of implicitly. It would only be unmanaged references created explicitly
		/// by the plugin that would need removal or cleanup.
		/// </remarks>
		public virtual void UnloadPlugIn()
		{
		}

		/// <summary>
		/// A lame ordering system. Needs to be replaced with Windsor containter etc.
		/// </summary>
		public int RequestedLoadOrder { get; private set; }

		/// <summary>
		/// The descriptive name of the plugin.
		/// </summary>
		public string PluginName { get; private set; }

		/// <summary>
		/// A brief description of the plugin.
		/// </summary>
		public string PluginDescription { get; private set; }

		#endregion
	}
}