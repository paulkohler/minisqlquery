using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using ICSharpCode.TextEditor.Document;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	public partial class DbConnectionsForm : Form
	{
		public DbConnectionsForm()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
		}

		private void DbConnectionsForm_Load(object sender, EventArgs e)
		{
			txtConns.Text = LoadConnectionDefinitions();
			LoadProvidersDropDownButton();
			txtConns.Focus();
		}

		private void LoadProvidersDropDownButton()
		{
			string[] providerNames = Utility.GetSqlProviderNames();

			foreach (string providerName in providerNames)
			{
				ToolStripMenuItem newProviderNameMenuItem = new ToolStripMenuItem();
				string name = string.Concat("ProviderName_", providerName.Replace(".", "_"), "_ToolStripMenuItem");

				newProviderNameMenuItem.Name = name;
				newProviderNameMenuItem.Text = providerName;
				newProviderNameMenuItem.Tag = providerName; // store data in tag so Text can change etc
				newProviderNameMenuItem.Click += InsertProviderNameToolStripMenuItemClickHandler;

				toolStripSplitButtonInsertProvider.DropDownItems.Add(newProviderNameMenuItem);
			}
		}

		private void DbConnectionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// todo - confirm changes lost
		}


		private static string LoadConnectionDefinitions()
		{
			return Utility.LoadConnections();
		}

		private static void SaveConnectionDefinitions(string data)
		{
			string[] conns = data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			ConnectionDefinition[] connDefs = ConnectionDefinition.Parse(conns);
			ApplicationServices.Instance.Settings.SetConnectionDefinitions(connDefs);
			Utility.SaveConnections(data);
		}

		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			string data = txtConns.Text;
			SaveConnectionDefinitions(data);
			Close();
		}

		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void InsertProviderNameToolStripMenuItemClickHandler(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

			if (menuItem != null)
			{
				int offset = txtConns.ActiveTextAreaControl.Caret.Offset;
				//if (txtConns.ActiveTextAreaControl.SelectionManager.IsSelected(offset))
				//{
				//    txtConns.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
				//}
				txtConns.Document.Insert(offset, menuItem.Tag.ToString());
			}
		}

		private void toolStripButtonEditConnStr_Click(object sender, EventArgs e)
		{
			// this could be alot better!
			ConnectionStringBuilderForm frm = null;
			int lineNumber = txtConns.ActiveTextAreaControl.Caret.Line;
			string[] lines = txtConns.Text.Replace("\r", string.Empty).Split(new char[] { '\n' });
			string line = lines[lineNumber];

			if (line.Trim().Length == 0) // its a blank line so new
			{
				frm = new ConnectionStringBuilderForm(); // new blank form
			}
			else
			{
				// parse line - it could be a comment etc
				ConnectionDefinition connDefToEdit = ConnectionDefinition.Parse(line);
				if (connDefToEdit != null)
				{
					frm = new ConnectionStringBuilderForm(
						connDefToEdit.Name,
						connDefToEdit.ProviderName,
						connDefToEdit.ConnectionString);
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
				}
			}


			if (frm != null)
			{
				frm.ShowDialog(this);
				if (frm.DialogResult == DialogResult.OK)
				{
					// push the data into a ConnectionDefinition and render string
					ConnectionDefinition newConnDef = new ConnectionDefinition();
					newConnDef.Name = frm.ConnectionName;
					newConnDef.ProviderName = frm.ProviderName;
					newConnDef.ConnectionString = frm.ConnectionString;

					// insert def into current line - replace if required
					LineSegment lineSegment = txtConns.Document.GetLineSegment(lineNumber);
					txtConns.Document.Replace(lineSegment.Offset, lineSegment.Length, newConnDef.ToParsableFormat());

					//MessageBox.Show(frm.ConnectionString);
				}
			}
		}
	}
}