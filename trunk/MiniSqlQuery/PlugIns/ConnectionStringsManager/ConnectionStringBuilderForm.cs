using System;
using System.Data.Common;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	public partial class ConnectionStringBuilderForm : Form
	{
		private readonly IApplicationServices _services;
		public DbConnectionDefinition ConnectionDefinition { get; set; }
		public const string DefaultProviderName = "System.Data.SqlClient";

		private bool _initialised;
		private bool _loaded;
		private string _initProvider;
		private string _providerName = DefaultProviderName;
		private string _connStr;
		private DbConnectionStringBuilder _connStrBldr;
		private bool _dirty;

		public string ConnectionName
		{
			get { return txtConnectionName.Text; }
			set { txtConnectionName.Text = value; }
		}

		public string Comments
		{
			get { return txtComments.Text; }
			set { txtComments.Text = value; }
		}

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

		public DbConnectionStringBuilder DbConnectionStringBuilderInstance
		{
			get { return _connStrBldr; }
		}


		public ConnectionStringBuilderForm(IApplicationServices services)
		{
			InitializeComponent();
			_services = services;
		}

		public ConnectionStringBuilderForm(DbConnectionDefinition definition, IApplicationServices services)
			: this(services)
		{
			ConnectionDefinition = definition;
			ConnectionName = ConnectionDefinition.Name;
			Comments = ConnectionDefinition.Comment;
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
			_loaded = true;
		}

		private void ConnectionStringBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_dirty)
			{
				DialogResult saveFile = _services.HostWindow.DisplayMessageBox(
					this,
					"The connection details have changed, do you want to save?\r\n", "Save Changes?",
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


		private void toolStripButtonOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			WriteValuesBack();
			Close();
		}

		private void toolStripButtonCancel_Click(object sender, EventArgs e)
		{
			// todo - is dirty?
			DialogResult = DialogResult.Cancel;
			Close();
		}

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

		private void ItemsTextChanged(object sender, EventArgs e)
		{
			SetDirty();
		}

		private void propertyGridDbConnection_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			SetDirty();
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

		private void toolStripButtonTest_Click(object sender, EventArgs e)
		{
			// do a standalone raw connection test
			Exception exp = QueryRunner.TestDbConnection(ProviderName, ConnectionString);
			if (exp == null)
			{
				string msg = string.Format("Connected to '{0}' successfully.", ConnectionName);
				_services.HostWindow.DisplaySimpleMessageBox(this, msg, "Connection Successful");
			}
			else
			{
				string msg = string.Format("Failed connecting to '{0}'.{1}{2}", ConnectionName, Environment.NewLine, exp.Message);
				_services.HostWindow.DisplaySimpleMessageBox(this, msg, "Connection Failed");
			}
		}

#if DEBUG

		public override string ToString()
		{
			return string.Format("[ConnectionStringBuilderForm => Name: {0}; Provider: {1}; ConnectionString: {2}]", ConnectionName, ProviderName,
			                     ConnectionString);
		}

#endif
	}
}