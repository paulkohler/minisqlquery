#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Properties;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	/// <summary>The connection string builder form.</summary>
	public partial class ConnectionStringBuilderForm : Form
	{
		/// <summary>The _host window.</summary>
		private readonly IHostWindow _hostWindow;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>Gets or sets ConnectionDefinition.</summary>
		public DbConnectionDefinition ConnectionDefinition { get; set; }

		/// <summary>The default provider name.</summary>
		public const string DefaultProviderName = "System.Data.SqlClient";

		/// <summary>The _initialised.</summary>
		private bool _initialised;

		/// <summary>The _loaded.</summary>
		private bool _loaded;

		/// <summary>The _init provider.</summary>
		private string _initProvider;

		/// <summary>The _provider name.</summary>
		private string _providerName = DefaultProviderName;

		/// <summary>The _conn str.</summary>
		private string _connStr;

		/// <summary>The _conn str bldr.</summary>
		private DbConnectionStringBuilder _connStrBldr;

		/// <summary>The _dirty.</summary>
		private bool _dirty;

		/// <summary>Gets or sets ConnectionName.</summary>
		public string ConnectionName
		{
			get { return txtConnectionName.Text; }
			set { txtConnectionName.Text = value; }
		}

		/// <summary>Gets or sets Comments.</summary>
		public string Comments
		{
			get { return txtComments.Text; }
			set { txtComments.Text = value; }
		}

		/// <summary>Gets or sets ProviderName.</summary>
		public string ProviderName
		{
			get { return _providerName; }
			set
			{
				_providerName = value;
				cboProvider.SelectedItem = _providerName;
			}
		}

		/// <summary>
		/// The supplied connection string - or if during an edit - the new one.
		/// </summary>
		public string ConnectionString
		{
			get
			{
				if (_connStrBldr != null)
				{
					return _connStrBldr.ConnectionString;
				}

				return _connStr;
			}

			set
			{
				_connStr = value; // store
				if (_connStrBldr == null)
				{
					BindNewConnectionStringBuilder();
				}
			}
		}

		/// <summary>Gets DbConnectionStringBuilderInstance.</summary>
		public DbConnectionStringBuilder DbConnectionStringBuilderInstance
		{
			get { return _connStrBldr; }
		}


		/// <summary>Initializes a new instance of the <see cref="ConnectionStringBuilderForm"/> class.</summary>
		/// <param name="hostWindow">The host window.</param>
		/// <param name="services">The services.</param>
		public ConnectionStringBuilderForm(IHostWindow hostWindow, IApplicationServices services)
		{
			InitializeComponent();
			_hostWindow = hostWindow;
			_services = services;
			Icon = ImageResource.database_edit_icon;
		}

		/// <summary>Initializes a new instance of the <see cref="ConnectionStringBuilderForm"/> class.</summary>
		/// <param name="hostWindow">The host window.</param>
		/// <param name="definition">The definition.</param>
		/// <param name="services">The services.</param>
		public ConnectionStringBuilderForm(IHostWindow hostWindow, DbConnectionDefinition definition, IApplicationServices services)
			: this(hostWindow, services)
		{
			ConnectionDefinition = definition;
			ConnectionName = ConnectionDefinition.Name;
			Comments = ConnectionDefinition.Comment;
			_initProvider = ConnectionDefinition.ProviderName;
			_connStr = ConnectionDefinition.ConnectionString;
		}

		/// <summary>The connection string builder form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ConnectionStringBuilderForm_Load(object sender, EventArgs e)
		{
			cboProvider.DataSource = Utility.GetSqlProviderNames();
			_initialised = true;
		}

		/// <summary>The connection string builder form_ shown.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ConnectionStringBuilderForm_Shown(object sender, EventArgs e)
		{
			if (_initProvider == null)
			{
				ProviderName = DefaultProviderName;
			}
			else
			{
				ProviderName = _initProvider;
			}

			_loaded = true;
		}

		/// <summary>The connection string builder form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ConnectionStringBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_dirty)
			{
				DialogResult saveFile = _hostWindow.DisplayMessageBox(
					this, 
					Resources.The_connection_details_have_changed__do_you_want_to_save, Resources.Save_Changes, 
					MessageBoxButtons.YesNoCancel, 
					MessageBoxIcon.Question, 
					MessageBoxDefaultButton.Button1, 
					0, 
					null, 
					null);

				switch (saveFile)
				{
					case DialogResult.Yes:
						WriteValuesBack();
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}


		/// <summary>The tool strip button ok_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			WriteValuesBack();
			Close();
		}

		/// <summary>The tool strip button cancel_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			// todo - is dirty?
			DialogResult = DialogResult.Cancel;
			Close();
		}

		/// <summary>The write values back.</summary>
		protected void WriteValuesBack()
		{
			if (ConnectionDefinition == null)
			{
				ConnectionDefinition = new DbConnectionDefinition();
			}

			ConnectionDefinition.Name = ConnectionName;
			ConnectionDefinition.ProviderName = ProviderName;
			ConnectionDefinition.ConnectionString = ConnectionString;
			ConnectionDefinition.Comment = Comments;
			_dirty = false;
		}


		/// <summary>The bind new connection string builder.</summary>
		private void BindNewConnectionStringBuilder()
		{
			if (!_initialised)
			{
				return;
			}

			bool getBuilderFailed = false;
			try
			{
				_connStrBldr = DbProviderFactories.GetFactory(ProviderName).CreateConnectionStringBuilder();
				if (_connStrBldr == null)
				{
					getBuilderFailed = true;
				}
			}
			catch
			{
				getBuilderFailed = true;
			}

			if (getBuilderFailed)
			{
				_connStrBldr = new GenericConnectionStringBuilder();
			}

			try
			{
				_connStrBldr.ConnectionString = _connStr;
			}
			catch (ArgumentException argExp)
			{
				// consume error but notify
				MessageBox.Show(Resources.BindNewConnectionStringBuilder_Could_not_populate_with_current_connection_string___ + argExp.Message);
			}

			propertyGridDbConnection.SelectedObject = _connStrBldr;
		}

		/// <summary>The cbo provider_ selected index changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_initialised)
			{
				return;
			}

			SetDirty();
			if (cboProvider.SelectedItem != null)
			{
				ProviderName = cboProvider.SelectedItem.ToString();
				BindNewConnectionStringBuilder();
			}
		}

		/// <summary>The items text changed.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void ItemsTextChanged(object sender, EventArgs e)
		{
			SetDirty();
		}

		/// <summary>The property grid db connection_ property value changed.</summary>
		/// <param name="s">The s.</param>
		/// <param name="e">The e.</param>
		private void propertyGridDbConnection_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			SetDirty();
		}

		/// <summary>The set dirty.</summary>
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

		/// <summary>The tool strip button test_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void toolStripButtonTest_Click(object sender, EventArgs e)
		{
			// do a standalone raw connection test
			Exception exp = QueryRunner.TestDbConnection(ProviderName, ConnectionString);
			if (exp == null)
			{
				string msg = string.Format(Resources.Connected_to_0_successfully, ConnectionName);
				_hostWindow.DisplaySimpleMessageBox(this, msg, Resources.Connection_Successful);
			}
			else
			{
				string msg = string.Format(Resources.Failed_connecting_to_0_1_2, ConnectionName, Environment.NewLine, exp.Message);
				_hostWindow.DisplaySimpleMessageBox(this, msg, Resources.Connection_Failed);
			}
		}

#if DEBUG

		/// <summary>The to string.</summary>
		/// <returns>The to string.</returns>
		public override string ToString()
		{
			return string.Format("[ConnectionStringBuilderForm => Name: {0}; Provider: {1}; ConnectionString: {2}]", ConnectionName, ProviderName, 
			                     ConnectionString);
		}

#endif
	}
}