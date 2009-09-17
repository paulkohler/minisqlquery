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

		private readonly IApplicationServices _services;
		private readonly IHostWindow _hostWindow;

		public DatabaseInspectorForm(IApplicationServices services, IHostWindow hostWindow)
		{
			InitializeComponent();
			BuildImageList();

			DatabaseTreeView.Nodes.Clear();
			TreeNode root = CreateRootNodes();
			root.Nodes.Add("Loading problem - check connection details and reset...");
			DatabaseTreeView.Nodes.Add(root);

			_services = services;
			_hostWindow = hostWindow;

			_services.Settings.DatabaseConnectionReset += Settings_DatabaseConnectionReset;
		}

		/// <summary>
		/// Builds the image list.
		/// It's nicer to hadle image lists this way, easier to update etc
		/// </summary>
		private void BuildImageList()
		{
			InspectorImageList.Images.Add("Table", ImageResource.table);
			InspectorImageList.Images.Add("Database", ImageResource.database);
			InspectorImageList.Images.Add("Column", ImageResource.column);
			InspectorImageList.Images.Add("Tables", ImageResource.table_multiple);
			InspectorImageList.Images.Add("Views", ImageResource.view_multiple);
			InspectorImageList.Images.Add("View", ImageResource.view);
			InspectorImageList.Images.Add("Column-PK", ImageResource.key);
			InspectorImageList.Images.Add("Column-FK", ImageResource.key_go_disabled);
			InspectorImageList.Images.Add("Column-PK-FK", ImageResource.key_go);
			InspectorImageList.Images.Add("Column-RowVersion", ImageResource.column_row_version);
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
				_hostWindow.SetPointerState(Cursors.WaitCursor);
				if (_metaDataService == null)
				{
					_metaDataService = DatabaseMetaDataService.Create(_services.Settings.ConnectionDefinition.ProviderName);
				}
				connection = _metaDataService.GetDescription();
				populate = true;
			}
			catch (Exception exp)
			{
				string msg = string.Format(
					"{0}\r\n\r\nCheck the connection and select 'Reset Database Connection'.",
					exp.Message);
				_hostWindow.DisplaySimpleMessageBox(_hostWindow.Instance, msg, "DB Connection Error");
				_hostWindow.SetStatus(this, exp.Message);
			}
			finally
			{
				_hostWindow.SetPointerState(Cursors.Default);
			}

			if (populate)
			{
				try
				{
					_hostWindow.SetPointerState(Cursors.WaitCursor);
					_model = _metaDataService.GetDbObjectModel(_services.Settings.ConnectionDefinition.ConnectionString);
				}
				finally
				{
					_hostWindow.SetPointerState(Cursors.Default);
				}

				BildTreeFromDbModel(connection);
				_hostWindow.SetStatus(this, string.Empty);
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

			foreach (DbModelTable table in _model.Tables)
			{
				CreateTreeNodes(table);
			}
			foreach (DbModelTable view in _model.Views)
			{
				CreateTreeNodes(view);
			}

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
				string friendlyColumnName = Utility.MakeSqlFriendly(column.Name);
				TreeNode columnNode = new TreeNode(friendlyColumnName);
				columnNode.Name = column.Name;
				string imageKey = BuildImageKey(column);
				columnNode.ImageKey = imageKey;
				columnNode.SelectedImageKey = imageKey;
				columnNode.ContextMenuStrip = ColumnNameContextMenuStrip;
				columnNode.Tag = column;
				columnNode.Text = GetSummary(column);
				string toolTip = BuildToolTip(table, column);
				columnNode.ToolTipText = toolTip;
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

		private string BuildImageKey(DbModelColumn column)
		{
			string imageKey = column.ObjectType;
			if (column.IsRowVersion)
			{
				imageKey += "-RowVersion";
			}
			else
			{
				if (column.IsKey)
				{
					imageKey += "-PK";
				}
				if (column.ForiegnKeyReference != null)
				{
					imageKey += "-FK";
				}
			}
			return imageKey;
		}

		private string BuildToolTip(DbModelTable table, DbModelColumn column)
		{
			string friendlyColumnName = Utility.MakeSqlFriendly(column.Name);
			string toolTip = table.FullName + "." + friendlyColumnName;
			if (column.IsKey)
			{
				toolTip += "; Primary Key";
			}
			if (column.IsAutoIncrement)
			{
				toolTip += "; Auto*";
			}
			if (column.ForiegnKeyReference != null)
			{
				toolTip += string.Format("; FK -> {0}.{1}", column.ForiegnKeyReference.ReferenceTable.FullName, column.ForiegnKeyReference.ReferenceColumn.Name);
			}
			if (column.IsReadOnly)
			{
				toolTip += "; Read Only";
			}
			return toolTip;
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
			IQueryEditor editor = _hostWindow.ActiveChildForm as IQueryEditor;

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
				_sqlWriter = _services.Resolve<ISqlWriter>();
			}
			_sqlWriter.WriteSummary(stringWriter, column);
			return stringWriter.ToString();
		}
	}
}