using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	public partial class OptionsForm : Form
	{
		private readonly IApplicationServices _services;
		private readonly IHostWindow _host;
		private PropertyGrid propertyGrid;
		readonly List<IConfigurationObject> _configurationObjects = new List<IConfigurationObject>();

		public OptionsForm(IApplicationServices applicationServices, IHostWindow hostWindow)
		{
			InitializeComponent();
			_services = applicationServices;
			_host = hostWindow;
		}

		private IConfigurationObject ConfigurationObject
		{
			get
			{
				if (lstSettingsProviders.SelectedIndex > -1)
				{
					return _configurationObjects[lstSettingsProviders.SelectedIndex];
				}
				return null;
			}
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			propertyGrid = new PropertyGrid();
			propertyGrid.Dock = DockStyle.Fill;
			groupBox1.Controls.Add(propertyGrid);

			var cofigTypes = _services.GetConfigurationObjectTypes();

			// build a list of config instances
			foreach (Type cofigType in cofigTypes)
			{
				_configurationObjects.Add(_services.Resolve<IConfigurationObject>(cofigType.FullName));
			}

			// check all plugins (and editors?)
			foreach (var configObject in _configurationObjects)
			{
				configObject.PropertyChanged += ConfigObjectPropertyChanged;
				lstSettingsProviders.Items.Add(configObject.Name);
			}

			lstSettingsProviders.SelectedIndex = 0;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (ConfigurationObject != null)
			{
				ConfigurationObject.Save();
			}
			Close();
		}

		private void listBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			if (ConfigurationObject != null)
			{
				bool change = true;
				if (ConfigurationObject.IsDirty)
				{
					var result = _host.DisplayMessageBox(null, "Changes made, save?", "Save Changes?", MessageBoxButtons.YesNo,
					                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
					                                           MessageBoxOptions.ServiceNotification, null, null);
					if (result == DialogResult.Yes)
					{
						ConfigurationObject.Save();
					}
					else
					{
						change = false;
					}
				}

				if (change)
				{
					propertyGrid.SelectedObject = ConfigurationObject.Settings;
				}
			}
		}

		void ConfigObjectPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsDirty")
			{
				string title = "Options";
				if (((IConfigurationObject)sender).IsDirty)
				{
					title += "*";
				}
				Text = title;
			}
		}
	}
}