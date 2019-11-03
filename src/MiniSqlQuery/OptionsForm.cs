#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery
{
    /// <summary>The options form.</summary>
    public partial class OptionsForm : Form
    {
        /// <summary>The _configuration objects.</summary>
        private readonly List<IConfigurationObject> _configurationObjects = new List<IConfigurationObject>();

        /// <summary>The _host.</summary>
        private readonly IHostWindow _host;

        /// <summary>The _property grid.</summary>
        private readonly PropertyGrid _propertyGrid;

        /// <summary>The _services.</summary>
        private readonly IApplicationServices _services;

        /// <summary>Initializes a new instance of the <see cref="OptionsForm"/> class.</summary>
        /// <param name="applicationServices">The application services.</param>
        /// <param name="hostWindow">The host window.</param>
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

        /// <summary>Gets ConfigurationObject.</summary>
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

        /// <summary>The ask to save changes.</summary>
        /// <returns></returns>
        private DialogResult AskToSaveChanges()
        {
            return _host.DisplayMessageBox(null, "Configuration changes made, would you like to save them?", "Save Changes?", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                           MessageBoxOptions.ServiceNotification, null, null);
        }

        /// <summary>The config object property changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ConfigObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
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

        /// <summary>The options form_ form closing.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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

        /// <summary>The options form_ load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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

        /// <summary>The btn o k_ click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ConfigurationObject != null)
            {
                ConfigurationObject.Save();
            }

            Close();
        }

        /// <summary>The list box 1_ selected value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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
    }
}