#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private readonly List<string> _items = new List<string>();
        private string _selectedItem;

        /// <summary>Initializes a new instance of the <see cref="FindObjectForm"/> class.</summary>
        /// <param name="databaseInspector">The database inspector.</param>
        public FindObjectForm(IDatabaseInspector databaseInspector)
        {
            _databaseInspector = databaseInspector;
            InitializeComponent();
        }

        /// <summary>Gets the Selected Table Name.</summary>
        [Obsolete]
        public string SelectedTableName
        {
            get { return _selectedItem; }
        }

        /// <summary>Gets the Selected Object Name.</summary>
        public string SelectedObjectName
        {
            get { return _selectedItem; }
        }

        /// <summary>Check the keys, escape to exit, enter to select. If up or down are pressed move the list item.</summary>
        /// <param name="msg">The windows message.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>The process command key result.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;

                case Keys.Enter:
                    Done();
                    break;

                case Keys.Up:
                    MoveSelectionUp();
                    return true;

                case Keys.Down:
                    MoveSelectionDown();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FindObjectForm_Load(object sender, EventArgs e)
        {
        }

        private void FindObjectForm_Shown(object sender, EventArgs e)
        {
            _items.Clear();

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

                    _items.Add(name);
                }

                foreach (DbModelView view in _databaseInspector.DbSchema.Views)
                {
                    _items.Add(view.FullName);
                }
            }
            finally
            {
                UseWaitCursor = false;
            }

            lstItems.DataSource = _items;
            txtSearchPattern.Focus();
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            Done();
        }

        private void txtSearchPattern_TextChanged(object sender, EventArgs e)
        {
            if (_items != null)
            {
                var searchValue = txtSearchPattern.Text.ToLowerInvariant();
                var dataSource = _items.Where(objectName =>
                {
                    objectName = objectName ?? String.Empty;
                    objectName = objectName.ToLowerInvariant();
                    return objectName.Contains(searchValue);
                }).ToList();
                Debug.WriteLine(string.Format("search '{0}' yields {1}...", searchValue, dataSource.Count));

                lstItems.DataSource = dataSource;
            }
            else
            {
                lstItems.DataSource = null;
            }

            SetSelectedName();
        }

        /// <summary>
        /// If there is a valid selection remember it.
        /// </summary>
        private void SetSelectedName()
        {
            _selectedItem = String.Empty;

            if (lstItems.SelectedItem != null)
            {
                _selectedItem = lstItems.SelectedItem.ToString();
            }
        }

        /// <summary>
        /// Move the list selection up one if we can.
        /// </summary>
        private void MoveSelectionUp()
        {
            if (lstItems.SelectedIndex < 1)
            {
                return;
            }
            lstItems.SelectedIndex--;
        }

        /// <summary>
        /// Move the list selection down one if we can.
        /// </summary>
        private void MoveSelectionDown()
        {
            var maxIndex = lstItems.Items.Count - 1;
            if (lstItems.SelectedIndex >= maxIndex)
            {
                return;
            }
            lstItems.SelectedIndex++;
        }

        /// <summary>
        /// Set the selected item and close the form.
        /// </summary>
        private void Done()
        {
            SetSelectedName();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}