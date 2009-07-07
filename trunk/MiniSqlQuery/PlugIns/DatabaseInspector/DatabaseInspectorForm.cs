using System;
using System.Collections.Generic;
using System.Data;
using System.Media;
using System.Windows.Forms;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	public partial class DatabaseInspectorForm : DockContent, IDatabaseInspector
	{
		private static object RootTag = new object();
		private static object TablesTag = new object();
		private static object ViewsTag = new object();
		private DataTable _metaData;
		internal DatabaseMetaDataService _metaDataService;
		private bool _populated;
		private TreeNode _rightClickedNode;
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
				string result = null;

				if (_rightClickedNode != null)
				{
					result = _rightClickedNode.Text;
				}

				return result;
			}
		}

		public DataTable DbSchema
		{
			get { return _metaData; }
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
			ExecLoadDatabaseDetails();
		}


		private void DatabaseInspectorControl_Load(object sender, EventArgs e)
		{
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
					_metaDataService = new DatabaseMetaDataService(
						Services.Settings.ProviderFactory,
						Services.Settings.ConnectionDefinition.ConnectionString);
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
				if (_metaData != null)
				{
					_metaData.Dispose();
				}

				try
				{
					Services.HostWindow.SetPointerState(Cursors.WaitCursor);
					_metaData = _metaDataService.GetSchema();
				}
				finally
				{
					Services.HostWindow.SetPointerState(Cursors.Default);
				}

//#if DEBUG
//                _metaData.WriteXml("DbMetaData.xml", XmlWriteMode.WriteSchema);
//#endif

				DatabaseTreeView.Nodes.Clear();
				TreeNode root = CreateRootNodes();
				root.ToolTipText = connection;

				// create a list of unique the schema.table names
				List<string> tableNames = FindTypes("table");
				List<string> viewNames = FindTypes("view");

				CreateNodes("table", tableNames);
				CreateNodes("view", viewNames);

				DatabaseTreeView.Nodes.Add(root);
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

		private void CreateNodes(string objectType, List<string> names)
		{
			foreach (string name in names)
			{
				TreeNode tableNode = new TreeNode(name);
				tableNode.Name = name;
				string imageKey = "Table";
				if (objectType != "table")
				{
					imageKey = "View";
				}

				tableNode.ImageKey = imageKey;
				tableNode.SelectedImageKey = imageKey;
				tableNode.ContextMenuStrip = TableNodeContextMenuStrip;
				string[] schemaTablePair = name.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
				string schemaName = string.Empty;
				string tableName = string.Empty;
				if (schemaTablePair.Length == 1)
				{
					tableName = schemaTablePair[0];
				}
				else if (schemaTablePair.Length == 2)
				{
					schemaName = schemaTablePair[0];
					tableName = schemaTablePair[1];
				}

				string filter = string.Format("Table = '{0}'", tableName);
				if (string.IsNullOrEmpty(schemaName) == false)
				{
					filter += string.Format(" AND Schema = '{0}'", schemaName);
				}
				DataView columnsDv = new DataView(_metaData, filter, null, DataViewRowState.CurrentRows);
				if (objectType == "table")
				{
					tableNode.Tag = new TableInfo(tableName, schemaName);
				}
				else
				{
					tableNode.Tag = new ViewInfo(tableName, schemaName);
				}

				foreach (DataRowView rowView in columnsDv)
				{
					//rowView.Row
					int len = (int) rowView["Length"];
					string extra = rowView["DataType"].ToString();
					if (len > 0)
					{
						extra = string.Format("{0} ({1})", rowView["DataType"], len);
					}
					bool nullable = (bool) rowView["IsNullable"];
					if (nullable)
					{
						extra += " NULL";
					}
					else
					{
						extra += " NOT NULL";
					}
					string columnName = string.Format("{0} [{1}]", rowView["Column"], extra);
					TreeNode columnNode = new TreeNode(columnName);
					columnNode.Name = columnName;
					columnNode.ImageKey = "Column";
					columnNode.SelectedImageKey = "Column";
					columnNode.ContextMenuStrip = ColumnNameContextMenuStrip;
					columnNode.Tag = new ColumnInfo(rowView.Row);
					tableNode.Nodes.Add(columnNode);
				}
				if (objectType == "table")
				{
					_tablesNode.Nodes.Add(tableNode);
				}
				else
				{
					_viewsNode.Nodes.Add(tableNode);
				}
			}
		}

		private List<string> FindTypes(string objectType)
		{
			List<string> names = new List<string>();
			DataView tablesDv = new DataView(_metaData, string.Format("ObjectType = '{0}'", objectType), "Schema, Table", DataViewRowState.CurrentRows);
			foreach (DataRowView row in tablesDv)
			{
				string schemaName = (string) row["Schema"];
				string tableName = (string) row["Table"];
				string dot = ".";
				if (string.IsNullOrEmpty(schemaName)) // allow no schema
				{
					dot = string.Empty;
				}
				string fullTableName = string.Concat(schemaName, dot, tableName);

				if (!names.Contains(fullTableName))
				{
					names.Add(fullTableName);
				}
			}
			return names;
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
			//_tablesNode.ContextMenuStrip = ?;
			_tablesNode.Tag = TablesTag;

			_viewsNode = new TreeNode("Views");
			_viewsNode.ImageKey = "Views";
			_viewsNode.SelectedImageKey = "Views";
			//_viewsNode.ContextMenuStrip = ?;
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
				if (node != null && (node.Tag is ISchemaClass))
				{
					string name = node.Text;
					if (node.Tag is TableInfo)
					{
						name = ((TableInfo) node.Tag).Name;
					}
					else if (node.Tag is ColumnInfo)
					{
						name = ((ColumnInfo) node.Tag).ColumnSchemaDataRow["Column"].ToString();
					}
					SetText(name);
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
				if (node != null && (node.Tag is TableInfo || node.Tag is ViewInfo))
				{
					_rightClickedNode = node;
					//Debug.WriteLine(node.Text);
				}
				else
				{
					_rightClickedNode = null;
				}
			}
		}

		#region Nested type: ColumnInfo

		/// <summary>
		/// Simple helper class to identify a node as a column and store it's schema info.
		/// </summary>
		private class ColumnInfo : ISchemaClass
		{
			public DataRow ColumnSchemaDataRow;

			public ColumnInfo(DataRow info)
			{
				ColumnSchemaDataRow = info;
			}
		}

		#endregion

		#region Nested type: ISchemaClass

		/// <summary>
		/// Just for referencing.
		/// </summary>
		private interface ISchemaClass
		{
		}

		#endregion

		#region Nested type: TableInfo

		/// <summary>
		/// Simple helper class to identify a node as a table and store the schema info.
		/// </summary>
		private class TableInfo : ISchemaClass
		{
			public string Name;
			public string Schema;

			public TableInfo(string schema, string name)
			{
				Schema = schema;
				Name = name;
			}
		}
		private class ViewInfo : ISchemaClass
		{
			public string Name;
			public string Schema;

			public ViewInfo(string schema, string name)
			{
				Schema = schema;
				Name = name;
			}
		}

		#endregion

		#region Nested type: TablesNode

		private class TablesNode : ISchemaClass
		{
			public string Name;

			public TablesNode()
			{
				Name = "Tables";
			}
		}


		#endregion
	}
}