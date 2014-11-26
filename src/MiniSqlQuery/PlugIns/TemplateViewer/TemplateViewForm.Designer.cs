namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	partial class TemplateViewForm
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Templates");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateViewForm));
			this.tvTemplates = new System.Windows.Forms.TreeView();
			this.treeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemRun = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.templateFileWatcher = new System.IO.FileSystemWatcher();
			this.treeMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.templateFileWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// tvTemplates
			// 
			this.tvTemplates.ContextMenuStrip = this.treeMenuStrip;
			this.tvTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvTemplates.HideSelection = false;
			this.tvTemplates.ImageKey = "folder_page";
			this.tvTemplates.ImageList = this.imageList;
			this.tvTemplates.Indent = 15;
			this.tvTemplates.Location = new System.Drawing.Point(0, 0);
			this.tvTemplates.Name = "tvTemplates";
			treeNode1.Name = "templates";
			treeNode1.Text = "Templates";
			this.tvTemplates.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.tvTemplates.SelectedImageIndex = 0;
			this.tvTemplates.ShowRootLines = false;
			this.tvTemplates.Size = new System.Drawing.Size(292, 266);
			this.tvTemplates.TabIndex = 0;
			this.tvTemplates.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTemplates_NodeMouseDoubleClick);
			this.tvTemplates.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTemplates_NodeMouseClick);
			// 
			// treeMenuStrip
			// 
			this.treeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRun,
            this.toolStripMenuItemEdit});
			this.treeMenuStrip.Name = "treeMenuStrip";
			this.treeMenuStrip.Size = new System.Drawing.Size(148, 48);
			// 
			// toolStripMenuItemRun
			// 
			this.toolStripMenuItemRun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripMenuItemRun.Name = "toolStripMenuItemRun";
			this.toolStripMenuItemRun.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemRun.Text = "Run";
			this.toolStripMenuItemRun.Click += new System.EventHandler(this.toolStripMenuItemRun_Click);
			// 
			// toolStripMenuItemEdit
			// 
			this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
			this.toolStripMenuItemEdit.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemEdit.Text = "Edit Template";
			this.toolStripMenuItemEdit.Click += new System.EventHandler(this.toolStripMenuItemEdit_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "folder_page");
			this.imageList.Images.SetKeyName(1, "script");
			this.imageList.Images.SetKeyName(2, "script_code");
			// 
			// templateFileWatcher
			// 
			this.templateFileWatcher.EnableRaisingEvents = true;
			this.templateFileWatcher.Filter = "*.mt";
			this.templateFileWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.LastWrite)));
			this.templateFileWatcher.SynchronizingObject = this;
			this.templateFileWatcher.Renamed += new System.IO.RenamedEventHandler(this.templateFileWatcher_Renamed);
			this.templateFileWatcher.Deleted += new System.IO.FileSystemEventHandler(this.templateFileWatcher_Changed);
			this.templateFileWatcher.Created += new System.IO.FileSystemEventHandler(this.templateFileWatcher_Changed);
			this.templateFileWatcher.Changed += new System.IO.FileSystemEventHandler(this.templateFileWatcher_Changed);
			// 
			// TemplateViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.tvTemplates);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TemplateViewForm";
			this.TabText = "Templates";
			this.Text = "Templates";
			this.Load += new System.EventHandler(this.TemplateViewForm_Load);
			this.treeMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.templateFileWatcher)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvTemplates;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ContextMenuStrip treeMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRun;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
		private System.IO.FileSystemWatcher templateFileWatcher;
	}
}