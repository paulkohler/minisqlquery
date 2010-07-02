#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>A simple base class to use for implementing the <see cref="IPlugIn"/> interface.</summary>
	public abstract class PluginLoaderBase : IPlugIn
	{
		/// <summary>Initializes a new instance of the <see cref="PluginLoaderBase"/> class. Creates a new instance of a plugin loader class.</summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <remarks>The <see cref="RequestedLoadOrder"/> defaults to 1000.</remarks>
		protected PluginLoaderBase(string name, string description)
			: this(name, description, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="PluginLoaderBase"/> class. Creates a new instance of a plugin loader class.</summary>
		/// <param name="name">The descriptive name of the plugin</param>
		/// <param name="description">A brief description of the plugin.</param>
		/// <param name="requestedLoadOrder">The requested load order. See <see cref="IPlugIn.RequestedLoadOrder"/>.</param>
		protected PluginLoaderBase(string name, string description, int requestedLoadOrder)
		{
			PluginName = name;
			PluginDescription = description;
			RequestedLoadOrder = requestedLoadOrder;
		}

		/// <summary>A brief description of the plugin.</summary>
		/// <value>The plugin description.</value>
		public string PluginDescription { get; private set; }

		/// <summary>The descriptive name of the plugin.</summary>
		/// <value>The plugin name.</value>
		public string PluginName { get; private set; }

		/// <summary>A lame ordering system. Needs to be replaced with Windsor containter etc.</summary>
		/// <value>The requested load order.</value>
		public int RequestedLoadOrder { get; private set; }

		/// <summary>A reference to the applications service manager.</summary>
		/// <value>The services.</value>
		public IApplicationServices Services { get; private set; }

		/// <summary>Must be implemented by the inheriting class. 
		/// Called after the application has started.</summary>
		public abstract void InitializePlugIn();

		/// <summary>Called when the plugins are loading during application startup. 
		/// Stores the reference to <paramref name="services"/>.</summary>
		/// <param name="services">The application services intance.</param>
		public void LoadPlugIn(IApplicationServices services)
		{
			Services = services;
		}

		/// <summary>Called as the application is shutting down.</summary>
		/// <remarks>In most cases there is probably no need to do anything here, all controls etc created
		/// will be disposed of implicitly. It would only be unmanaged references created explicitly
		/// by the plugin that would need removal or cleanup.</remarks>
		public virtual void UnloadPlugIn()
		{
		}
	}
}