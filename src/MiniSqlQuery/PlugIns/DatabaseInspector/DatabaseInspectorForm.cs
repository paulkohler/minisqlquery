#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE
#endregion

using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using MiniSqlQuery.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	/// <summary>The database inspector form.</summary>
	public partial class DatabaseInspectorForm : DockContent, IDatabaseInspector
	{
		/// <summary>The root tag.</summary>
		private static readonly object RootTag = new object();

		/// <summary>The tables tag.</summary>
		private static readonly object TablesTag = new object();

		/// <summary>The views tag.</summary>
		private static readonly object ViewsTag = new object();

		/// <summary>The _host window.</summary>
		private readonly IHostWindow _hostWindow;

		/// <summary>The _services.</summary>
		private readonly IApplicationServices _services;

		/// <summary>The _meta data service.</summary>
		private IDatabaseSchemaService _metaDataService;

		/// <summary>The _model.</summary>
		private DbModelInstance _model;

		/// <summary>The _populated.</summary>
		private bool _populated;

		/// <summary>The _right clicked model object.</summary>
		private IDbModelNamedObject _rightClickedModelObject;

		/// <summary>The _right clicked node.</summary>
		private TreeNode _rightClickedNode;

		/// <summary>The _sql writer.</summary>
		private ISqlWriter _sqlWriter;

		/// <summary>The _tables node.</summary>
		private TreeNode _tablesNode;

		/// <summary>The _views node.</summary>
		private TreeNode _viewsNode;

		/// <summary>Initializes a new instance of the <see cref="DatabaseInspectorForm"/> class.</summary>
		/// <param name="services">The services.</param>
		/// <param name="hostWindow">The host window.</param>
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

		/// <summary>Gets ColumnMenu.</summary>
		public ContextMenuStrip ColumnMenu
		{
			get { return ColumnNameContextMenuStrip; }
		}

		/// <summary>Gets DbSchema.</summary>
		public DbModelInstance DbSchema
		{
			get { return _model; }
		}

		/// <summary>Gets RightClickedModelObject.</summary>
		public IDbModelNamedObject RightClickedModelObject
		{
			get { return _rightClickedModelObject; }
		}

		/// <summary>Gets RightClickedTableName.</summary>
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

		/// <summary>Gets TableMenu.</summary>
		public ContextMenuStrip TableMenu
		{
			get { return TableNodeContextMenuStrip; }
		}

		/// <summary>The load database details.</summary>
		public void LoadDatabaseDetails()
		{
			ExecLoadDatabaseDetails();
		}

		/// <summary>The navigate to.</summary>
		/// <param name="modelObject">The model object.</param>
		public void NavigateTo(IDbModelNamedObject modelObject)
		{
			if (modelObject == null)
			{
				return;
			}
            
            // todo - ensure expanded

			switch (modelObject.ObjectType)
			{
				case ObjectTypes.Table:
					foreach (TreeNode treeNode in _tablesNode.Nodes)
					{
						IDbModelNamedObject obj = treeNode.Tag as IDbModelNamedObject;
						if (obj != null && modelObject == obj)
						{
							SelectNode(treeNode);
						}
					}

					break;
				case ObjectTypes.View:
					foreach (TreeNode treeNode in _viewsNode.Nodes)
					{
						IDbModelNamedObject obj = treeNode.Tag as IDbModelNamedObject;
						if (obj != null && modelObject == obj)
						{
							SelectNode(treeNode);
						}
					}

					break;
				case ObjectTypes.Column:
					DbModelColumn modelColumn = modelObject as DbModelColumn;
					if (modelColumn != null)
					{
						foreach (TreeNode treeNode in _tablesNode.Nodes)
						{
// only look in the tables nodw for FK refs
							DbModelTable modelTable = treeNode.Tag as DbModelTable;
							if (modelTable != null && modelTable == modelColumn.ParentTable)
							{
								// now find the column in the child nodes
								foreach (TreeNode columnNode in treeNode.Nodes)
								{
									DbModelColumn modelReferingColumn = columnNode.Tag as DbModelColumn;
									if (modelReferingColumn != null && modelReferingColumn == modelColumn)
									{
										SelectNode(columnNode);
									}
								}
							}
						}
					}

					break;
			}
		}

		/// <summary>The build image key.</summary>
		/// <param name="column">The column.</param>
		/// <returns>The build image key.</returns>
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

				if (column.ForeignKeyReference != null)
				{
					imageKey += "-FK";
				}
			}

			return imageKey;
		}

		/// <summary>Builds the image list.
		/// It's nicer to hadle image lists this way, easier to update etc</summary>
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

		/// <summary>The build tool tip.</summary>
		/// <param name="table">The table.</param>
		/// <param name="column">The column.</param>
		/// <returns>The build tool tip.</returns>
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

			if (column.ForeignKeyReference != null)
			{
				toolTip += string.Format("; FK -> {0}.{1}", column.ForeignKeyReference.ReferenceTable.FullName, column.ForeignKeyReference.ReferenceColumn.Name);
			}

			if (column.IsReadOnly)
			{
				toolTip += "; Read Only";
			}

			return toolTip;
		}

		/// <summary>The build tree from db model.</summary>
		/// <param name="connection">The connection.</param>
		private void BuildTreeFromDbModel(string connection)
		{
			DatabaseTreeView.Nodes.Clear();
			TreeNode root = CreateRootNodes();
			root.ToolTipText = connection;

		    if (_model.Tables != null)
		    {
		        foreach (DbModelTable table in _model.Tables)
		        {
		            CreateTreeNodes(table);
		        }
		    }

		    if (_model.Views != null)
		    {
		        foreach (DbModelView view in _model.Views)
		        {
		            CreateTreeNodes(view);
		        }
		    }

		    DatabaseTreeView.Nodes.Add(root);
		}

		/// <summary>The create root nodes.</summary>
		/// <returns></returns>
		private TreeNode CreateRootNodes()
		{
			TreeNode root = new TreeNode(Resources.Database);
			root.ImageKey = "Database";
			root.SelectedImageKey = "Database";
			root.ContextMenuStrip = InspectorContextMenuStrip;
			root.Tag = RootTag;

			_tablesNode = new TreeNode(Resources.Tables);
			_tablesNode.ImageKey = "Tables";
			_tablesNode.SelectedImageKey = "Tables";
			_tablesNode.Tag = TablesTag;

			_viewsNode = new TreeNode(Resources.Views);
			_viewsNode.ImageKey = "Views";
			_viewsNode.SelectedImageKey = "Views";
			_viewsNode.Tag = ViewsTag;

			root.Nodes.Add(_tablesNode);
			root.Nodes.Add(_viewsNode);

			return root;
		}

		/// <summary>The create tree nodes.</summary>
		/// <param name="table">The table.</param>
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

		/// <summary>The database inspector form_ form closing.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DatabaseInspectorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Hide();
				e.Cancel = true;
			}
		}


		/// <summary>The database inspector form_ load.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DatabaseInspectorForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>The database tree view_ before expand.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The database tree view_ node mouse click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void DatabaseTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode node = e.Node;
			if (e.Button == MouseButtons.Right)
			{
				IDbModelNamedObject namedObject = node.Tag as IDbModelNamedObject;
				_rightClickedModelObject = namedObject;

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

		/// <summary>The database tree view_ node mouse double click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
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

		/// <summary>The exec load database details.</summary>
		/// <returns>The exec load database details.</returns>
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

				BuildTreeFromDbModel(connection);
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

		/// <summary>The get summary.</summary>
		/// <param name="column">The column.</param>
		/// <returns>The get summary.</returns>
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

		/// <summary>The select node.</summary>
		/// <param name="treeNode">The tree node.</param>
		private void SelectNode(TreeNode treeNode)
		{
			if (treeNode.Parent != null)
			{
				treeNode.Parent.EnsureVisible();
			}

			treeNode.EnsureVisible();
			DatabaseTreeView.SelectedNode = treeNode;
			treeNode.Expand();
		    DatabaseTreeView.Focus();
		}

		/// <summary>The set text.</summary>
		/// <param name="text">The text.</param>
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

		/// <summary>The settings_ database connection reset.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void Settings_DatabaseConnectionReset(object sender, EventArgs e)
		{
			_metaDataService = null;
			_sqlWriter = null;
			ExecLoadDatabaseDetails();
		}

		/// <summary>The load tool strip menu item_ click.</summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadDatabaseDetails();
		}
	}
}