namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	partial class DatabaseInspectorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseInspectorForm));
			this.InspectorContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InspectorImageList = new System.Windows.Forms.ImageList(this.components);
			this.TableNodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DatabaseTreeView = new System.Windows.Forms.TreeView();
			this.ColumnNameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.InspectorContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// InspectorContextMenuStrip
			// 
			this.InspectorContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
			this.InspectorContextMenuStrip.Name = "InspectorContextMenuStrip";
			this.InspectorContextMenuStrip.Size = new System.Drawing.Size(160, 26);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.loadToolStripMenuItem.Text = "&Load Meta-Data";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// InspectorImageList
			// 
			this.InspectorImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.InspectorImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.InspectorImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TableNodeContextMenuStrip
			// 
			this.TableNodeContextMenuStrip.Name = "TableNodeContextMenuStrip";
			this.TableNodeContextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// DatabaseTreeView
			// 
			this.DatabaseTreeView.ContextMenuStrip = this.InspectorContextMenuStrip;
			this.DatabaseTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DatabaseTreeView.ImageIndex = 0;
			this.DatabaseTreeView.ImageList = this.InspectorImageList;
			this.DatabaseTreeView.Location = new System.Drawing.Point(0, 0);
			this.DatabaseTreeView.Name = "DatabaseTreeView";
			this.DatabaseTreeView.SelectedImageIndex = 0;
			this.DatabaseTreeView.ShowNodeToolTips = true;
			this.DatabaseTreeView.Size = new System.Drawing.Size(307, 294);
			this.DatabaseTreeView.TabIndex = 2;
			this.DatabaseTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.DatabaseTreeView_NodeMouseDoubleClick);
			this.DatabaseTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DatabaseTreeView_BeforeExpand);
			this.DatabaseTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.DatabaseTreeView_NodeMouseClick);
			// 
			// ColumnNameContextMenuStrip
			// 
			this.ColumnNameContextMenuStrip.Name = "ColumnNameContextMenuStrip";
			this.ColumnNameContextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// DatabaseInspectorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(307, 294);
			this.Controls.Add(this.DatabaseTreeView);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DatabaseInspectorForm";
			this.TabText = "DB Inspector";
			this.Text = "Database Inspector";
			this.Load += new System.EventHandler(this.DatabaseInspectorForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseInspectorForm_FormClosing);
			this.InspectorContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip InspectorContextMenuStrip;
		private System.Windows.Forms.ContextMenuStrip TableNodeContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ImageList InspectorImageList;
		private System.Windows.Forms.TreeView DatabaseTreeView;
		private System.Windows.Forms.ContextMenuStrip ColumnNameContextMenuStrip;


	}
}