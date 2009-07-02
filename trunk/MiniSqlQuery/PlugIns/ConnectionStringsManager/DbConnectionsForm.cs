using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	public partial class DbConnectionsForm : Form
	{
		private DbConnectionDefinitionList _definitionList;

		public DbConnectionsForm()
		{
			InitializeComponent();
			toolStripButtonAdd.Image = ImageResource.database_add;
			toolStripButtonCopyAsNew.Image = ImageResource.database_add;
			toolStripButtonEditConnStr.Image = ImageResource.database_edit;
			toolStripButtonDelete.Image = ImageResource.database_delete;
		}

		public DbConnectionsForm(IApplicationServices services)
			: this()
		{
			Services = services;
		}

		public IApplicationServices Services { get; private set; }

		private void DbConnectionsForm_Shown(object sender, EventArgs e)
		{
			_definitionList = LoadConnectionDefinitions();
			UpdateListView();
		}

		private void UpdateListView()
		{
			if (_definitionList.Definitions != null && _definitionList.Definitions.Length > 0)
			{
				lstConnections.Items.Clear();
				lstConnections.Items.AddRange(_definitionList.Definitions);
				lstConnections.SelectedItem = _definitionList.Definitions[0];
			}
		}

		private void RemoveFromList(DbConnectionDefinition definition)
		{
			if (lstConnections.Items.Contains(definition))
			{
				lstConnections.Items.Remove(definition);
			}
		}

		private void AddToList(DbConnectionDefinition definition)
		{
			if (!lstConnections.Items.Contains(definition))
			{
				lstConnections.Items.Add(definition);
			}
		}

		private void DbConnectionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// todo - confirm changes lost
		}


		private static DbConnectionDefinitionList LoadConnectionDefinitions()
		{
			return DbConnectionDefinitionList.FromXml(Utility.LoadConnections());
		}

		private static void SaveConnectionDefinitions(DbConnectionDefinitionList data)
		{
			ApplicationServices.Instance.Settings.SetConnectionDefinitions(data);
			Utility.SaveConnections(data);
		}

		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			SaveConnectionDefinitions(_definitionList);
			Close();
		}

		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			ManageDefinition(null);
		}

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

		private void toolStripButtonEditConnStr_Click(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

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

		private void lstConnections_SelectedValueChanged(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			UpdateDetailsPanel(definition);
		}

		private void lstConnections_DoubleClick(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

		private void ManageDefinition(DbConnectionDefinition definition)
		{
			ConnectionStringBuilderForm frm;
			string oldName = null;

			if (definition == null)
			{
				frm = new ConnectionStringBuilderForm(Services); // new blank form
			}
			else
			{
				oldName = definition.Name;
				frm = new ConnectionStringBuilderForm(definition, Services);
			}

			frm.ShowDialog(this);

			if (frm.DialogResult == DialogResult.OK)
			{
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
					Services.HostWindow.DisplaySimpleMessageBox(this, msg, "Connection Successful");
				}
				else
				{
					string msg = string.Format("Failed connecting to '{0}'.{1}{2}", definition.Name, Environment.NewLine, exp.Message);
					Services.HostWindow.DisplaySimpleMessageBox(this, msg, "Connection Failed");
				}
			}
		}
	}
}