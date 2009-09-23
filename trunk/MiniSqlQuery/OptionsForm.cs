using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
	public partial class OptionsForm : Form
	{
		private readonly List<IConfigurationObject> _configurationObjects = new List<IConfigurationObject>();
		private readonly IHostWindow _host;
		private readonly PropertyGrid _propertyGrid;
		private readonly IApplicationServices _services;

		public OptionsForm(IApplicationServices applicationServices, IHostWindow hostWindow)
		{
			InitializeComponent();

			// add a grid to the panel
			_propertyGrid = new PropertyGrid();
			_propertyGrid.Dock = DockStyle.Fill;
			groupBox1.Controls.Add(_propertyGrid);

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
			var cofigTypes = _services.GetConfigurationObjectTypes();

			// build a list of config instances
			foreach (Type cofigType in cofigTypes)
			{
				_configurationObjects.Add(_services.Resolve<IConfigurationObject>(cofigType.FullName));
			}

			// add the config editors to the list and watch them for changes
			foreach (var configObject in _configurationObjects)
			{
				configObject.PropertyChanged += ConfigObjectPropertyChanged;
				lstSettingsProviders.Items.Add(configObject.Name);
			}

			// select first
			if (lstSettingsProviders.Items.Count > 0)
			{
				lstSettingsProviders.SelectedIndex = 0;
			}
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
					DialogResult result = AskToSaveChanges();
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
					_propertyGrid.SelectedObject = ConfigurationObject.Settings;
				}
			}
		}

		private void ConfigObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsDirty")
			{
				string title = "Options";
				if (((IConfigurationObject) sender).IsDirty)
				{
					title += "*";
				}
				Text = title;
			}
		}

		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (ConfigurationObject != null)
			{
				if (ConfigurationObject.IsDirty)
				{
					DialogResult result = AskToSaveChanges();
					if (result == DialogResult.Yes)
					{
						ConfigurationObject.Save();
					}
					else if (result == DialogResult.Cancel)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private DialogResult AskToSaveChanges()
		{
			return _host.DisplayMessageBox(null, "Configuration changes made, would you like to save them?", "Save Changes?", MessageBoxButtons.YesNo,
			                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
			                               MessageBoxOptions.ServiceNotification, null, null);
		}

	}
}