#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	/// <summary>The db connections form.</summary>
	public partial class DbConnectionsForm : Form
	{
		/// <summary>The _host window.</summary>
		private readonly IHostWindow _hostWindow;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _settings.</summary>
		private readonly IApplicationSettings _settings;

		/// <summary>The _definition list.</summary>
		private DbConnectionDefinitionList _definitionList;

        private bool _loaded;
        
        private bool _dirty;

	    /// <summary>Initializes a new instance of the <see cref="DbConnectionsForm"/> class.</summary>
		public DbConnectionsForm()
		{
			InitializeComponent();
			toolStripButtonAdd.Image = ImageResource.database_add;
			toolStripButtonCopyAsNew.Image = ImageResource.database_add;
			toolStripButtonEditConnStr.Image = ImageResource.database_edit;
			toolStripButtonDelete.Image = ImageResource.database_delete;
			Icon = ImageResource.disconnect_icon;
		}

		/// <summary>Initializes a new instance of the <see cref="DbConnectionsForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="hostWindow">The host window.</param>
		/// <param name="settings">The settings.</param>
		public DbConnectionsForm(IApplicationServices services, IHostWindow hostWindow, IApplicationSettings settings)
			: this()
		{
			_services = services;
			_hostWindow = hostWindow;
			_settings = settings;
		}

		/// <summary>The load connection definitions.</summary>
		/// <returns></returns>
		private static DbConnectionDefinitionList LoadConnectionDefinitions()
		{
			return DbConnectionDefinitionList.FromXml(Utility.LoadConnections());
		}


		/// <summary>The add to list.</summary>
		/// <param name="definition">The definition.</param>
		private void AddToList(DbConnectionDefinition definition)
		{
			if (!lstConnections.Items.Contains(definition))
			{
				lstConnections.Items.Add(definition);
                SetDirty();
            }
		}

	    /// <summary>The db connections form_ form closing.</summary>
	    /// <param name="sender">The sender.</param>
	    /// <param name="e">The e.</param>
	    private void DbConnectionsForm_FormClosing(object sender, FormClosingEventArgs e)
	    {
	        if (_dirty)
	        {
	            DialogResult saveFile = _hostWindow.DisplayMessageBox(
	                this,
	                Resources.The_connection_details_have_changed__do_you_want_to_save,
	                Resources.Save_Changes,
	                MessageBoxButtons.YesNoCancel,
	                MessageBoxIcon.Question,
	                MessageBoxDefaultButton.Button1,
	                0,
	                null,
	                null);

	            switch (saveFile)
	            {
	                case DialogResult.Yes:
	                    SaveConnectionDefinitions(_definitionList);
	                    break;
	                case DialogResult.Cancel:
	                    e.Cancel = true;
	                    break;
	            }
	        }
	    }

	    /// <summary>The db connections form_ shown.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DbConnectionsForm_Shown(object sender, EventArgs e)
		{
			_definitionList = LoadConnectionDefinitions();
			UpdateListView();
	        _loaded = true;
		}

		/// <summary>The manage definition.</summary>
		/// <param name="definition">The definition.</param>
		private void ManageDefinition(DbConnectionDefinition definition)
		{
			ConnectionStringBuilderForm frm;
			string oldName = null;

			if (definition == null)
			{
				frm = new ConnectionStringBuilderForm(_hostWindow, _services); // new blank form
			}
			else
			{
				oldName = definition.Name;
				frm = new ConnectionStringBuilderForm(_hostWindow, definition, _services);
			}

			frm.ShowDialog(this);

			if (frm.DialogResult == DialogResult.OK)
			{
			    SetDirty();

				if (lstConnections.Items.Contains(frm.ConnectionDefinition) && definition != null)
				{
					if (definition.Name != oldName)
					{
						// want the list text to update due to name change
						UpdateListView();
						lstConnections.SelectedItem = definition;
					}
					else
					{
						UpdateDetailsPanel(lstConnections.SelectedItem as DbConnectionDefinition);
					}
				}
				else
				{
					_definitionList.AddDefinition(frm.ConnectionDefinition);
					AddToList(frm.ConnectionDefinition);
					lstConnections.SelectedItem = frm.ConnectionDefinition;
				}
			}
		}

		/// <summary>The remove from list.</summary>
		/// <param name="definition">The definition.</param>
		private void RemoveFromList(DbConnectionDefinition definition)
		{
			if (lstConnections.Items.Contains(definition))
			{
				lstConnections.Items.Remove(definition);
                SetDirty();
			}
		}


		/// <summary>The save connection definitions.</summary>
		/// <param name="data">The data.</param>
		private void SaveConnectionDefinitions(DbConnectionDefinitionList data)
		{
			_settings.SetConnectionDefinitions(data);
			Utility.SaveConnections(data);
		    _dirty = false;
		}

		/// <summary>The update details panel.</summary>
		/// <param name="definition">The definition.</param>
		private void UpdateDetailsPanel(DbConnectionDefinition definition)
		{
			if (definition != null)
			{
				txtName.Text = definition.Name;
				txtProvider.Text = definition.ProviderName;
				txtConn.Text = definition.ConnectionString;
				txtComment.Text = definition.Comment;
			}
			else
			{
				txtName.Clear();
				txtProvider.Clear();
				txtConn.Clear();
				txtComment.Clear();
			}
		}

		/// <summary>The update list view.</summary>
		private void UpdateListView()
		{
			if (_definitionList.Definitions != null && _definitionList.Definitions.Length > 0)
			{
				lstConnections.Items.Clear();
				lstConnections.Items.AddRange(_definitionList.Definitions);
				lstConnections.SelectedItem = _definitionList.Definitions[0];
			}
		}

		/// <summary>The lst connections_ double click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lstConnections_DoubleClick(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

		/// <summary>The lst connections_ selected value changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void lstConnections_SelectedValueChanged(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			UpdateDetailsPanel(definition);
		}

		/// <summary>The tool strip button add_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			ManageDefinition(null);
		}

		/// <summary>The tool strip button cancel_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>The tool strip button copy as new_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonCopyAsNew_Click(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				DbConnectionDefinition newDefinition = DbConnectionDefinition.FromXml(definition.ToXml());
				newDefinition.Name = "Copy of " + newDefinition.Name;
				ManageDefinition(newDefinition);
			}
		}

		/// <summary>The tool strip button delete_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				int newIndex = Math.Max(lstConnections.SelectedIndex - 1, 0);
				_definitionList.RemoveDefinition(definition);
				RemoveFromList(definition);
				if (lstConnections.Items.Count > 0)
				{
					lstConnections.SelectedIndex = newIndex;
				}
			}
		}

		/// <summary>The tool strip button edit conn str_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonEditConnStr_Click(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

		/// <summary>The tool strip button ok_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			SaveConnectionDefinitions(_definitionList);
			Close();
		}

		/// <summary>The tool strip button test_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonTest_Click(object sender, EventArgs e)
		{
			// do a standalone raw connection test
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				Exception exp = QueryRunner.TestDbConnection(definition.ProviderName, definition.ConnectionString);
				if (exp == null)
				{
					string msg = string.Format("Connected to '{0}' successfully.", definition.Name);
					_hostWindow.DisplaySimpleMessageBox(this, msg, "Connection Successful");
				}
				else
				{
					string msg = string.Format("Failed connecting to '{0}'.{1}{2}", definition.Name, Environment.NewLine, exp.Message);
					_hostWindow.DisplaySimpleMessageBox(this, msg, "Connection Failed");
				}
			}
		}

        private void DbConnectionsForm_Load(object sender, EventArgs e)
        {

        }

        private void SetDirty()
        {
            if (!_loaded)
            {
                return;
            }

            if (!_dirty)
            {
                _dirty = true;
                Text += "*";
            }
        }
	}
}