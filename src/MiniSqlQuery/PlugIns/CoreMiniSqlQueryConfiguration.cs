#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns
{
	/// <summary>The core mini sql query configuration.</summary>
	public class CoreMiniSqlQueryConfiguration : NotifyPropertyChangedBase, IConfigurationObject
	{
		/// <summary>The application settings.</summary>
		private readonly IApplicationSettings _settings;

		/// <summary>The settings wrapper.</summary>
		private readonly CoreMiniSqlQuerySettingsWrapper _settingsWrapper;

		/// <summary>The dirty record.</summary>
		private static bool _isDirty;

		/// <summary>Initializes a new instance of the <see cref="CoreMiniSqlQueryConfiguration"/> class.</summary>
		/// <param name="settings">The settings.</param>
		public CoreMiniSqlQueryConfiguration(IApplicationSettings settings)
		{
			_settings = settings;
			_settingsWrapper = new CoreMiniSqlQuerySettingsWrapper(_settings);
			_settingsWrapper.PropertyChanged += ProxyPropertyChanged;
			_isDirty = false;
		}

		/// <summary>Gets a value indicating whether IsDirty.</summary>
		public bool IsDirty
		{
			get { return _isDirty; }
			private set
			{
				if (_isDirty != value)
				{
					_isDirty = value;
					OnPropertyChanged("IsDirty");
				}
			}
		}

		/// <summary>Gets Name.</summary>
		public string Name
		{
			get { return "Mini SQL Query Settings"; }
		}

		/// <summary>Gets Settings.</summary>
		public object Settings
		{
			get { return _settingsWrapper; }
		}

		/// <summary>Save the settings back.</summary>
		public void Save()
		{
			_settings.EnableQueryBatching = _settingsWrapper.EnableQueryBatching;
			_settings.CommandTimeout = _settingsWrapper.CommandTimeout;
			_settings.PlugInFileFilter = _settingsWrapper.PlugInFileFilter;
			_settings.MostRecentFiles = _settingsWrapper.MostRecentFiles;
			_settings.LoadExternalPlugins = _settingsWrapper.LoadExternalPlugins;
			_settings.DefaultConnectionDefinitionFilename = _settingsWrapper.DefaultConnectionDefinitionFilename;
			_settings.DateTimeFormat = _settingsWrapper.DateTimeFormat;
			_settings.NullText = _settingsWrapper.NullText;
			_settings.IncludeReadOnlyColumnsInExport = _settingsWrapper.IncludeReadOnlyColumnsInExport;
			IsDirty = false;
		}

		/// <summary>The proxy property changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The events.</param>
		private void ProxyPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IsDirty = true;
		}

		/// <summary>The core mini sql query settings wrapper.</summary>
		public class CoreMiniSqlQuerySettingsWrapper : NotifyPropertyChangedBase
		{
			/// <summary>The _settings.</summary>
			private readonly IApplicationSettings _settings;

			/// <summary>The _date time format.</summary>
			private string _dateTimeFormat;

			/// <summary>The _default connection definition filename.</summary>
			private string _defaultConnectionDefinitionFilename;

			/// <summary>The _enable query batching.</summary>
			private bool _enableQueryBatching;

			/// <summary>The _load plugins.</summary>
			private bool _loadPlugins;

			/// <summary>The _null text.</summary>
			private string _nullText;

			/// <summary>The _plug in file filter.</summary>
			private string _plugInFileFilter;

			StringCollection _mostRecentFiles;

			/// <summary>The include ReadOnly Columns in Export value.</summary>
			private bool _includeReadOnlyColumnsInExport;

			/// <summary>The command timout.</summary>
			private int _commandTimeout;

			/// <summary>Initializes a new instance of the <see cref="CoreMiniSqlQuerySettingsWrapper"/> class.</summary>
			/// <param name="settings">The settings.</param>
			public CoreMiniSqlQuerySettingsWrapper(IApplicationSettings settings)
			{
				_settings = settings;

				EnableQueryBatching = _settings.EnableQueryBatching;
				CommandTimeout = _settings.CommandTimeout;
				PlugInFileFilter = _settings.PlugInFileFilter;
				MostRecentFiles = _settings.MostRecentFiles;
				LoadExternalPlugins = _settings.LoadExternalPlugins;
				DefaultConnectionDefinitionFilename = _settings.DefaultConnectionDefinitionFilename;
				DateTimeFormat = _settings.DateTimeFormat;
				NullText = _settings.NullText;
				IncludeReadOnlyColumnsInExport = _settings.IncludeReadOnlyColumnsInExport;
			}

			/// <summary>Gets or sets DateTimeFormat.</summary>
			[Category("Query Results")]
			[Description("")]
			public string DateTimeFormat
			{
				get { return _dateTimeFormat; }
				set
				{
					if (_dateTimeFormat != value)
					{
						_dateTimeFormat = value;
						OnPropertyChanged("DateTimeFormat");
					}
				}
			}

			/// <summary>Gets or sets DefaultConnectionDefinitionFilename.</summary>
			[Category("Query")]
			[Description("If this value is set to a specific connections XML file it will be loaded in preferences to the default path " +
			             "(%APPDATA%\\MiniSqlQuery\\Connections.xml). Note that changing this value will require a restart of the application.")]
			public string DefaultConnectionDefinitionFilename
			{
				get { return _defaultConnectionDefinitionFilename; }
				set
				{
					if (_defaultConnectionDefinitionFilename != value)
					{
						_defaultConnectionDefinitionFilename = value;
						OnPropertyChanged("DefaultConnectionDefinitionFilename");
					}
				}
			}

			/// <summary>Gets or sets a value indicating whether EnableQueryBatching.</summary>
			[Category("Query")]
			[Description("Set to true to enable the batching feature, if false the 'GO' statements will be passed straight through.")]
			public bool EnableQueryBatching
			{
				get { return _enableQueryBatching; }
				set
				{
					if (_enableQueryBatching != value)
					{
						_enableQueryBatching = value;
						OnPropertyChanged("EnableQueryBatching");
					}
				}
			}

			/// <summary>Gets or sets a value indicating the command timeout.</summary>
			[Category("Query")]
			[Description("Gets or sets the wait time before terminating the attempt to execute a command and generating an error. A value of 0 indicates no limit.")]
			public int CommandTimeout
			{
				get { return _commandTimeout; }
				set
				{
					if (_commandTimeout != value)
					{
						_commandTimeout = value;
						OnPropertyChanged("CommandTimeout");
					}
				}
			}

			/// <summary>Gets or sets a value indicating whether LoadExternalPlugins.</summary>
			[Category("Plugins")]
			[Description("If true, external plugin files will be loaded (requires restart).")]
			public bool LoadExternalPlugins
			{
				get { return _loadPlugins; }
				set
				{
					if (_loadPlugins != value)
					{
						_loadPlugins = value;
						OnPropertyChanged("LoadExternalPlugins");
					}
				}
			}

			/// <summary>Gets or sets NullText.</summary>
			[Category("Query Results")]
			[Description("")]
			public string NullText
			{
				get { return _nullText; }
				set
				{
					if (_nullText != value)
					{
						_nullText = value;
						OnPropertyChanged("NullText");
					}
				}
			}

			/// <summary>Gets or sets PlugInFileFilter.</summary>
			[Category("Plugins")]
			[Description("The file filter used for finding plugins (*.PlugIn.dll)")]
			public string PlugInFileFilter
			{
				get { return _plugInFileFilter; }
				set
				{
					if (_plugInFileFilter != value)
					{
						_plugInFileFilter = value;
						OnPropertyChanged("PlugInFileFilter");
					}
				}
			}

			/// <summary>Gets or sets MostRecentFiles.</summary>
			[Category("Plugins")]
			[Description("The file filter used for finding plugins (*.PlugIn.dll)")]
			public StringCollection MostRecentFiles
			{
				get { return _mostRecentFiles; }
				set
				{
					if (_mostRecentFiles != value)
					{
						_mostRecentFiles = value;
						OnPropertyChanged("MostRecentFiles");
					}
				}
			}

			/// <summary>Gets or sets PlugInFileFilter.</summary>
			[Category("Export Scripts")]
			[Description("If true, the readonly columns (e.g. identity) will be exported in the script.")]
			public bool IncludeReadOnlyColumnsInExport
			{
				get { return _includeReadOnlyColumnsInExport; }
				set
				{
					if (_includeReadOnlyColumnsInExport != value)
					{
						_includeReadOnlyColumnsInExport = value;
						OnPropertyChanged("PlugInFileFilter");
					}
				}
			}
		}
	}
}