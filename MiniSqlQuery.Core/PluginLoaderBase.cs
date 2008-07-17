using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A simple base class to use for implementing the <see cref="IPlugIn"/> interface.
	/// </summary>
    public abstract class PluginLoaderBase : IPlugIn
    {
        private string _pluginDescription;
        private string _pluginName;
        private int _requestedLoadOrder;
        private IApplicationServices _services;

		/// <summary>
		/// Creates a new instance of a plugin loader class.
		/// </summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <remarks>
		/// The <see cref="RequestedLoadOrder"/> defaults to 1000.
		/// </remarks>
        public PluginLoaderBase(string name, string description)
            : this(name, description, 1000)
        {
        }

		/// <summary>
		/// Creates a new instance of a plugin loader class.
		/// </summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <param name="requestedLoadOrder">The requested load order. See <see cref="IPlugIn.RequestedLoadOrder"/>.</param>
        public PluginLoaderBase(string name, string description, int requestedLoadOrder)
        {
            _pluginName = name;
            _pluginDescription = description;
            _requestedLoadOrder = requestedLoadOrder;
        }

        /// <summary>
        /// A reference to the applications service manager.
        /// </summary>
        public IApplicationServices Services
        {
            get { return _services; }
        }

        #region IPlugIn Members

        /// <summary>
        /// Called when the plugins are loading during application startup. 
        /// Stores the reference to <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The application services intance.</param>
        public void LoadPlugIn(IApplicationServices services)
        {
            _services = services;
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
        public int RequestedLoadOrder
        {
            get { return _requestedLoadOrder; }
        }

		/// <summary>
		/// The descriptive name of the plugin.
		/// </summary>
		public string PluginName
        {
            get { return _pluginName; }
        }

		/// <summary>
		/// A brief description of the plugin.
		/// </summary>
		public string PluginDescription
        {
            get { return _pluginDescription; }
        }

        #endregion
    }
}