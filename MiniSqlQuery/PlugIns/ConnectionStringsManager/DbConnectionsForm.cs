using System;
using System.Collections.Generic;
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
		}

		private void DbConnectionsForm_Load(object sender, EventArgs e)
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
			Utility.SaveConnections(data.ToXml());
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

		private void toolStripButtonEditConnStr_Click(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

		private void lstConnections_SelectedValueChanged(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				txtName.Text = definition.Name;
				txtProvider.Text = definition.ProviderName;
				txtConn.Text = definition.ConnectionString;
				txtComment.Text = definition.Comment;
			}
		}

		private void lstConnections_DoubleClick(object sender, EventArgs e)
		{
			DbConnectionDefinition definition = lstConnections.SelectedItem as DbConnectionDefinition;
			if (definition != null)
			{
				ManageDefinition(definition);
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			ManageDefinition(null);
		}

		private void ManageDefinition(DbConnectionDefinition definition)
		{
			ConnectionStringBuilderForm frm;

			if (definition == null)
			{
				frm = new ConnectionStringBuilderForm(); // new blank form
			}
			else
			{
				frm = new ConnectionStringBuilderForm(definition);
			}

			frm.ShowDialog(this);
			if (frm.DialogResult == DialogResult.OK)
			{
				if (definition == null)
				{
					// add new to list...
					List<DbConnectionDefinition> list = new List<DbConnectionDefinition>(_definitionList.Definitions);
					list.Add(frm.ConnectionDefinition);
					_definitionList.Definitions = list.ToArray();
				}
				UpdateListView();
			}
		}
	}
}