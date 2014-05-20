#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	/// <summary>The find object form.</summary>
	public partial class FindObjectForm : Form
	{
		/// <summary>The _database inspector.</summary>
		private readonly IDatabaseInspector _databaseInspector;

		/// <summary>Initializes a new instance of the <see cref="FindObjectForm"/> class.</summary>
		/// <param name="databaseInspector">The database inspector.</param>
		public FindObjectForm(IDatabaseInspector databaseInspector)
		{
			_databaseInspector = databaseInspector;
			InitializeComponent();
		}

		/// <summary>Gets SelectedTableName.</summary>
		public string SelectedTableName
		{
			get { return cboObjects.Text; }
		}

		/// <summary>The process cmd key.</summary>
		/// <param name="msg">The msg.</param>
		/// <param name="keyData">The key data.</param>
		/// <returns>The process cmd key.</returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
					DialogResult = DialogResult.Cancel;
					Close();
					break;

				case Keys.Enter:
					DialogResult = DialogResult.OK;
					Close();
					break;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>The find object form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void FindObjectForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>The find object form_ shown.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void FindObjectForm_Shown(object sender, EventArgs e)
		{
		    if (cboObjects.AutoCompleteCustomSource == null)
		    {
		        cboObjects.AutoCompleteCustomSource = new AutoCompleteStringCollection();
		    }

            var collection = cboObjects.AutoCompleteCustomSource;

			try
			{
				UseWaitCursor = true;

			    if (_databaseInspector.DbSchema == null)
			    {
			        _databaseInspector.LoadDatabaseDetails();

			        // And if it is still null (e.g. connection error) then bail out:
			        if (_databaseInspector.DbSchema == null)
			        {
			            return;
			        }
			    }

			    foreach (DbModelTable table in _databaseInspector.DbSchema.Tables)
				{
				    var name = table.Schema;
				    if (!String.IsNullOrEmpty(name))
				    {
				        name += ".";
				    }
				    name += table.Name;

					collection.Add(name);
				}

				foreach (DbModelView view in _databaseInspector.DbSchema.Views)
				{
					collection.Add(view.FullName);
				}
			}
			finally
			{
				UseWaitCursor = false;
			}

            this.cboObjects.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cboObjects.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		}
	}
}