using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	public partial class DatabaseInspectorForm : DockContent, IDatabaseInspector
	{
		private static object RootTag = new object();
		private static object TablesTag = new object();
		private static object ViewsTag = new object();
		internal IDatabaseSchemaService _metaDataService;
		private DbModelInstance _model;
		private bool _populated;
		private TreeNode _rightClickedNode;
		private ISqlWriter _sqlWriter;
		private TreeNode _tablesNode;
		private TreeNode _viewsNode;

		internal IApplicationServices Services;

		public DatabaseInspectorForm(IApplicationServices services)
		{
			InitializeComponent();

			DatabaseTreeView.Nodes.Clear();
			TreeNode root = CreateRootNodes();
			root.Nodes.Add("Loading problem - check connection details and reset...");
			DatabaseTreeView.Nodes.Add(root);

			Services = services;

			Services.Settings.DatabaseConnectionReset += Settings_DatabaseConnectionReset;
		}

		#region IDatabaseInspector Members

		public string RightClickedTableName
		{
			get
			{
				if (_rightClickedNode == null)
				{
					return null;
				}
				return _rightClickedNode.Text;
			}
		}

		public DbModelInstance DbSchema
		{
			get { return _model; }
		}

		public ContextMenuStrip TableMenu
		{
			get { return TableNodeContextMenuStrip; }
		}

		public void LoadDatabaseDetails()
		{
			ExecLoadDatabaseDetails();
		}

		#endregion

		private void Settings_DatabaseConnectionReset(object sender, EventArgs e)
		{
			_metaDataService = null;
			_sqlWriter = null;
			ExecLoadDatabaseDetails();
		}


		private void DatabaseInspectorControl_Load(object sender, EventArgs e)
		{
		}

		private void DatabaseInspectorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Hide();
				e.Cancel = true;
			}
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadDatabaseDetails();
		}

		private bool ExecLoadDatabaseDetails()
		{
			bool populate = false;
			string connection = string.Empty;
			bool success = false;

			try
			{
				Services.HostWindow.SetPointerState(Cursors.WaitCursor);
				if (_metaDataService == null)
				{
					_metaDataService = DatabaseMetaDataService.Create(Services.Settings.ConnectionDefinition.ProviderName);
				}
				connection = _metaDataService.GetDescription();
				populate = true;
			}
			catch (Exception exp)
			{
				string msg = string.Format(
					"{0}\r\n\r\nCheck the connection and select 'Reset Database Connection'.",
					exp.Message);
				Services.HostWindow.DisplaySimpleMessageBox(Services.HostWindow.Instance, msg, "DB Connection Error");
				Services.HostWindow.SetStatus(this, exp.Message);
			}
			finally
			{
				Services.HostWindow.SetPointerState(Cursors.Default);
			}

			if (populate)
			{
				try
				{
					Services.HostWindow.SetPointerState(Cursors.WaitCursor);
					_model = _metaDataService.GetDbObjectModel(Services.Settings.ConnectionDefinition.ConnectionString);
				}
				finally
				{
					Services.HostWindow.SetPointerState(Cursors.Default);
				}

				BildTreeFromDbModel(connection);
				Services.HostWindow.SetStatus(this, string.Empty);
				success = true;
			}
			else
			{
				_populated = false;
				DatabaseTreeView.CollapseAll();
			}

			return success;
		}

		private void BildTreeFromDbModel(string connection)
		{
			DatabaseTreeView.Nodes.Clear();
			TreeNode root = CreateRootNodes();
			root.ToolTipText = connection;

			_model.Tables.ForEach(CreateTreeNodes);
			_model.Views.ForEach(CreateTreeNodes);

			DatabaseTreeView.Nodes.Add(root);
		}

		private void CreateTreeNodes(DbModelTable table)
		{
			TreeNode tableNode = new TreeNode(table.FullName);
			tableNode.Name = table.FullName;
			tableNode.ImageKey = table.ObjectType;
			tableNode.SelectedImageKey = table.ObjectType;
			tableNode.ContextMenuStrip = TableNodeContextMenuStrip;
			tableNode.Tag = table;

			foreach (DbModelColumn column in table.Columns)
			{
				TreeNode columnNode = new TreeNode(column.Name);
				columnNode.Name = column.Name;
				columnNode.ImageKey = column.ObjectType;
				columnNode.SelectedImageKey = column.ObjectType;
				columnNode.ContextMenuStrip = ColumnNameContextMenuStrip;
				columnNode.Tag = column;
				columnNode.Text = GetSummary(column);
				tableNode.Nodes.Add(columnNode);
			}

			switch (table.ObjectType)
			{
				case ObjectTypes.Table:
					_tablesNode.Nodes.Add(tableNode);
					break;
				case ObjectTypes.View:
					_viewsNode.Nodes.Add(tableNode);
					break;
			}
		}

		private TreeNode CreateRootNodes()
		{
			TreeNode root = new TreeNode("Database");
			root.ImageKey = "Database";
			root.SelectedImageKey = "Database";
			root.ContextMenuStrip = InspectorContextMenuStrip;
			root.Tag = RootTag;

			_tablesNode = new TreeNode("Tables");
			_tablesNode.ImageKey = "Tables";
			_tablesNode.SelectedImageKey = "Tables";
			_tablesNode.Tag = TablesTag;

			_viewsNode = new TreeNode("Views");
			_viewsNode.ImageKey = "Views";
			_viewsNode.SelectedImageKey = "Views";
			_viewsNode.Tag = ViewsTag;

			root.Nodes.Add(_tablesNode);
			root.Nodes.Add(_viewsNode);

			return root;
		}


		private void SetText(string text)
		{
			IQueryEditor editor = Services.HostWindow.ActiveChildForm as IQueryEditor;

			if (editor != null)
			{
				editor.InsertText(text);
			}
			else
			{
				SystemSounds.Beep.Play();
			}
		}

		private void DatabaseTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (e.Button == MouseButtons.Left)
			{
				IDbModelNamedObject namedObject = node.Tag as IDbModelNamedObject;
				if (namedObject != null)
				{
					SetText(namedObject.FullName);
				}
			}
		}

		private void DatabaseTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if (node != null && node.Tag == RootTag && !_populated)
			{
				_populated = true;
				bool ok = ExecLoadDatabaseDetails();

				if (ok && DatabaseTreeView.Nodes.Count > 0)
				{
					DatabaseTreeView.Nodes[0].Expand();
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		private void DatabaseInspectorForm_Load(object sender, EventArgs e)
		{
		}

		private void DatabaseTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (e.Button == MouseButtons.Right)
			{
				IDbModelNamedObject namedObject = node.Tag as IDbModelNamedObject;

				if (namedObject != null && 
					(namedObject.ObjectType == ObjectTypes.Table || namedObject.ObjectType == ObjectTypes.View))
				{
					_rightClickedNode = node;
				}
				else
				{
					_rightClickedNode = null;
				}
			}
		}

		private string GetSummary(DbModelColumn column)
		{
			StringWriter stringWriter = new StringWriter();
			if (_sqlWriter == null)
			{
				_sqlWriter = Services.Resolve<ISqlWriter>(null);
			}
			_sqlWriter.WriteSummary(stringWriter, column);
			return stringWriter.ToString();
		}
	}
}