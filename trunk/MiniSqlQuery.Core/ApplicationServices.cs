using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Castle.Core;
using Castle.MicroKernel;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// The core services of the application (singleton).
	/// </summary>
	public class ApplicationServices : IApplicationServices
	{
		private static readonly IKernel _container;

		private Dictionary<Type, IPlugIn> _plugins = new Dictionary<Type, IPlugIn>();

		static ApplicationServices()
		{
			_container = new DefaultKernel();

			// add self
			_container.AddComponent("ApplicationServices", typeof (IApplicationServices), typeof (ApplicationServices), LifestyleType.Singleton);
		}

		/// <summary>
		/// A reference to the singleton instance of the services for this application.
		/// </summary>
		/// <value>The singleton instance of <see cref="IApplicationServices"/>.</value>
		public static IApplicationServices Instance
		{
			get { return _container.GetService<IApplicationServices>(); }
		}

		#region IApplicationServices Members

		/// <summary>
		/// The Dependency Injection container.
		/// </summary>
		public IKernel Container
		{
			get { return _container; }
		}

		/// <summary>
		/// Registers the component service type <typeparamref name="TService"/> with and implemetation of type <typeparamref name="TImp"/>.
		/// </summary>
		/// <typeparam name="TService">The contract type.</typeparam>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		public void RegisterComponent<TService, TImp>(string key)
		{
			_container.AddComponent(key, typeof (TService), typeof (TImp), LifestyleType.Transient);
		}

		/// <summary>
		/// Registers the component implemetation of type <typeparamref name="TImp"/>.
		/// </summary>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		public void RegisterComponent<TImp>(string key)
		{
			_container.AddComponent(key, typeof (TImp), LifestyleType.Transient);
		}

		/// <summary>
		/// Registers the component service type <typeparamref name="TService"/> with and implemetation of type <typeparamref name="TImp"/> as a singleton.
		/// </summary>
		/// <typeparam name="TService">The contract type.</typeparam>
		/// <typeparam name="TImp">The implementing type.</typeparam>
		/// <param name="key">The key or name of the service.</param>
		public void RegisterSingletonComponent<TService, TImp>(string key)
		{
			_container.AddComponent(key, typeof (TService), typeof (TImp), LifestyleType.Singleton);
		}

		/// <summary>
		/// Resolves an instance of <typeparamref name="T"/> from the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key (can be null if not applicable).</param>
		/// <returns></returns>
		public T Resolve<T>(string key)
		{
			if (key == null)
			{
				return _container.Resolve<T>();
			}
			return _container.Resolve<T>(key);
		}

		public T Resolve<T>()
		{
			return _container.Resolve<T>();
		}


		/// <summary>
		/// The application host window.
		/// </summary>
		/// <value>The host window - a <see cref="Form"/>.</value>
		public IHostWindow HostWindow
		{
			get { return _container.GetService<IHostWindow>(); }
		}

		/// <summary>
		/// The application settings instance.
		/// </summary>
		/// <value>A reference to the settings handler.</value>
		public IApplicationSettings Settings
		{
			get { return _container.GetService<IApplicationSettings>(); }
		}

		/// <summary>
		/// A dictionary of the current plugins for this application.
		/// </summary>
		/// <value>A reference to the plugin dictionary.</value>
		public Dictionary<Type, IPlugIn> Plugins
		{
			get { return _plugins; }
		}

		/// <summary>
		/// Loads the <paramref name="plugIn"/> (calling its <see cref="IPlugIn.LoadPlugIn"/> method) and
		/// adds it to the <see cref="Plugins"/> dictionary for access by other services.
		/// </summary>
		/// <param name="plugIn">The plugin to load.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="plugIn"/> is null.</exception>
		public void LoadPlugIn(IPlugIn plugIn)
		{
			if (plugIn == null)
			{
				throw new ArgumentNullException("plugIn");
			}

			plugIn.LoadPlugIn(this);
			_plugins.Add(plugIn.GetType(), plugIn);
			_container.AddComponent(plugIn.GetType().FullName, typeof(IPlugIn), plugIn.GetType());
		}

		/// <summary>
		/// Initializes the plugins that have been loaded during application startup.
		/// </summary>
		public void InitializePlugIns()
		{
			foreach (IPlugIn plugIn in _plugins.Values)
			{
				try
				{
					if (HostWindow != null)
					{
						HostWindow.SetStatus(null, "Initializing " + plugIn.PluginName);
					}
					plugIn.InitializePlugIn();
				}
				catch (Exception exp)
				{
					if (HostWindow == null)
					{
						throw;
					}
					HostWindow.DisplayMessageBox(
						null,
						string.Format("Error Initializing {0}:{1}{2}", plugIn.PluginName, Environment.NewLine, exp),
						"Plugin Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button1,
						0,
						null,
						null);
				}
			}
		}

		/// <summary>
		/// Occurs when a system message is posted.
		/// </summary>
		public event EventHandler<SystemMessageEventArgs> SystemMessagePosted;

		/// <summary>
		/// Posts a system message for listeners.
		/// </summary>
		/// <param name="message">A system message type.</param>
		/// <param name="data">The asssociated data.</param>
		public void PostMessage(SystemMessage message, object data)
		{
			OnSystemMessagePosted(new SystemMessageEventArgs(message, data));
		}

		/// <summary>
		/// Registers the editor of type <typeparamref name="TEditor"/> using the <paramref name="editorKeyName"/>.
		/// </summary>
		/// <typeparam name="TEditor">The editor type.</typeparam>
		/// <param name="editorKeyName">Name of the editor key.</param>
		/// <param name="extensions">The extensions, "sql", "cs" etc.</param>
		public void RegisterEditor<TEditor>(string editorKeyName, params string[] extensions) where TEditor : IEditor
		{
			RegisterComponent<IEditor, TEditor>(editorKeyName);

			// push the ext reg into the resolver....
			IFileEditorResolver resolver = Resolve<IFileEditorResolver>();
			if (extensions != null && extensions.Length > 0)
			{
				foreach (string extention in extensions)
				{
					resolver.Register(extention);
				}
			}
		}

		#endregion

		protected void OnSystemMessagePosted(SystemMessageEventArgs eventArgs)
		{
			EventHandler<SystemMessageEventArgs> handler = SystemMessagePosted;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}
	}
}