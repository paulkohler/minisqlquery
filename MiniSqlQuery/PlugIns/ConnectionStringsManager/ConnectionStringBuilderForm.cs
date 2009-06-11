using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using System.Data.Common;
using MiniSqlQuery.PlugIns.ConnectionStringsManager;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	public partial class ConnectionStringBuilderForm : Form
	{
		public DbConnectionDefinition ConnectionDefinition { get; set; }
		public const string DefaultProviderName = "System.Data.SqlClient";

		bool _initialised;
		string _initProvider = null;
		string _providerName = DefaultProviderName;
		string _connStr = null;
		DbConnectionStringBuilder _connStrBldr = null;

		public string ConnectionName 
		{
			get
			{
				return txtConnectionName.Text;
			}
			set
			{
				txtConnectionName.Text = value;
			}
		}

		public string ProviderName 
		{
			get
			{
				return _providerName;
			}
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

		public DbConnectionStringBuilder DbConnectionStringBuilderInstance
		{
			get
			{
				return _connStrBldr;
			}
		}


		public ConnectionStringBuilderForm()
		{
			InitializeComponent();
		}

		//public ConnectionStringBuilderForm(string name, string provider, string connection)
		//    : this()
		//{
		//    ConnectionName = name;
		//    _initProvider = provider;
		//    _connStr = connection;
		//}

		public ConnectionStringBuilderForm(DbConnectionDefinition definition)
			: this()
		{
			ConnectionDefinition = definition;
			ConnectionName = ConnectionDefinition.Name;
			_initProvider = ConnectionDefinition.ProviderName;
			_connStr = ConnectionDefinition.ConnectionString;
		}

		private void ConnectionStringBuilderForm_Load(object sender, EventArgs e)
		{
			cboProvider.DataSource = Utility.GetSqlProviderNames();
			_initialised = true;
		}

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
		}


		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			// todo - is dirty?
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			if (ConnectionDefinition == null)
			{
				ConnectionDefinition = new DbConnectionDefinition();
			}
			ConnectionDefinition.Name = ConnectionName;
			ConnectionDefinition.ProviderName = ProviderName;
			ConnectionDefinition.ConnectionString = ConnectionString;
			// todo ConnectionDefinition.Comment = Comment;
			Close();
		}


		private void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_initialised)
			{
				return;
			}

			if (cboProvider.SelectedItem != null)
			{
				ProviderName = cboProvider.SelectedItem.ToString();
				BindNewConnectionStringBuilder();
			}
		}


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
				MessageBox.Show("Could not populate with current connection string - " + argExp.Message);
			}
			propertyGridDbConnection.SelectedObject = _connStrBldr;
		}

		public override string ToString()
		{
			return string.Format("[ConnectionStringBuilderForm => Name: {0}; Provider: {1}; ConnectionString: {2}]", ConnectionName, ProviderName, ConnectionString);
		}
	}
}