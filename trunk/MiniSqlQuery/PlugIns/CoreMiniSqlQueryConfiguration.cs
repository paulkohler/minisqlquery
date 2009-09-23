using System;
using System.ComponentModel;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns
{
	public class CoreMiniSqlQueryConfiguration : NotifyPropertyChangedBase, IConfigurationObject
	{
		private static bool _isDirty;
		private readonly IApplicationSettings _settings;
		private readonly CoreMiniSqlQuerySettingsWrapper _settingsWrapper;

		public CoreMiniSqlQueryConfiguration(IApplicationSettings settings)
		{
			_settings = settings;
			_settingsWrapper = new CoreMiniSqlQuerySettingsWrapper(_settings);
			_settingsWrapper.PropertyChanged += ProxyPropertyChanged;
			_isDirty = false;
		}

		#region IConfigurationObject Members

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

		public void Save()
		{
			_settings.EnableQueryBatching = _settingsWrapper.EnableQueryBatching;
			IsDirty = false;
		}

		public object Settings
		{
			get { return _settingsWrapper; }
		}

		public string Name
		{
			get { return "Mini SQL Query Settings"; }
		}

		#endregion

		private void ProxyPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IsDirty = true;
		}

		#region Nested type: CoreMiniSqlQuerySettingsWrapper

		public class CoreMiniSqlQuerySettingsWrapper : NotifyPropertyChangedBase
		{
			private readonly IApplicationSettings _settings;

			private bool _enableQueryBatching;

			public CoreMiniSqlQuerySettingsWrapper(IApplicationSettings settings)
			{
				_settings = settings;

				EnableQueryBatching = _settings.EnableQueryBatching;
			}

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
		}

		#endregion
	}
}