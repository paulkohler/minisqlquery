namespace MiniSqlQuery
{
	partial class QueryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtQuery = new ICSharpCode.TextEditor.TextEditorControl();
			this.contextMenuStripQuery = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._resultsTabControl = new System.Windows.Forms.TabControl();
			this.ctxDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPageResults = new System.Windows.Forms.TabPage();
			this.gridResults1 = new System.Windows.Forms.DataGridView();
			this.editorContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.queryBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this._resultsTabControl.SuspendLayout();
			this.ctxDataGrid.SuspendLayout();
			this.tabPageResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridResults1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtQuery);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._resultsTabControl);
			this.splitContainer1.Size = new System.Drawing.Size(1037, 508);
			this.splitContainer1.SplitterDistance = 230;
			this.splitContainer1.TabIndex = 0;
			// 
			// txtQuery
			// 
			this.txtQuery.ContextMenuStrip = this.contextMenuStripQuery;
			this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQuery.EnableFolding = false;
			this.txtQuery.IsReadOnly = false;
			this.txtQuery.Location = new System.Drawing.Point(0, 0);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.ShowEOLMarkers = true;
			this.txtQuery.ShowSpaces = true;
			this.txtQuery.ShowTabs = true;
			this.txtQuery.Size = new System.Drawing.Size(1037, 230);
			this.txtQuery.TabIndex = 1;
			// 
			// contextMenuStripQuery
			// 
			this.contextMenuStripQuery.Name = "contextMenuStripQuery";
			this.contextMenuStripQuery.Size = new System.Drawing.Size(61, 4);
			// 
			// _resultsTabControl
			// 
			this._resultsTabControl.ContextMenuStrip = this.ctxDataGrid;
			this._resultsTabControl.Controls.Add(this.tabPageResults);
			this._resultsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._resultsTabControl.Location = new System.Drawing.Point(0, 0);
			this._resultsTabControl.Name = "_resultsTabControl";
			this._resultsTabControl.SelectedIndex = 0;
			this._resultsTabControl.Size = new System.Drawing.Size(1037, 274);
			this._resultsTabControl.TabIndex = 0;
			this._resultsTabControl.SelectedIndexChanged += new System.EventHandler(this.SetResultCountOnTabSelectedIndexChanged);
			// 
			// ctxDataGrid
			// 
			this.ctxDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem});
			this.ctxDataGrid.Name = "ctxDataGrid";
			this.ctxDataGrid.Size = new System.Drawing.Size(153, 76);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.copyToolStripMenuItem.Text = "Copy Selected";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// tabPageResults
			// 
			this.tabPageResults.Controls.Add(this.gridResults1);
			this.tabPageResults.Location = new System.Drawing.Point(4, 22);
			this.tabPageResults.Name = "tabPageResults";
			this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageResults.Size = new System.Drawing.Size(1029, 248);
			this.tabPageResults.TabIndex = 0;
			this.tabPageResults.Text = "Results";
			this.tabPageResults.UseVisualStyleBackColor = true;
			// 
			// gridResults1
			// 
			this.gridResults1.AllowUserToAddRows = false;
			this.gridResults1.AllowUserToDeleteRows = false;
			this.gridResults1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.gridResults1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridResults1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridResults1.Location = new System.Drawing.Point(3, 3);
			this.gridResults1.Name = "gridResults1";
			this.gridResults1.ReadOnly = true;
			this.gridResults1.Size = new System.Drawing.Size(1023, 242);
			this.gridResults1.TabIndex = 0;
			// 
			// editorContextMenuStrip
			// 
			this.editorContextMenuStrip.Name = "editorContextMenuStrip";
			this.editorContextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// queryBackgroundWorker
			// 
			this.queryBackgroundWorker.WorkerReportsProgress = true;
			this.queryBackgroundWorker.WorkerSupportsCancellation = true;
			this.queryBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.queryBackgroundWorker_DoWork);
			this.queryBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.queryBackgroundWorker_ProgressChanged);
			this.queryBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.queryBackgroundWorker_RunWorkerCompleted);
			// 
			// QueryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1037, 508);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "QueryForm";
			this.TabPageContextMenuStrip = this.editorContextMenuStrip;
			this.TabText = "Query";
			this.Text = "Query";
			this.Activated += new System.EventHandler(this.QueryForm_Activated);
			this.Deactivate += new System.EventHandler(this.QueryForm_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryForm_FormClosing);
			this.Load += new System.EventHandler(this.QueryForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this._resultsTabControl.ResumeLayout(false);
			this.ctxDataGrid.ResumeLayout(false);
			this.tabPageResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridResults1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl _resultsTabControl;
		private System.Windows.Forms.TabPage tabPageResults;
		private System.Windows.Forms.DataGridView gridResults1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripQuery;
		private ICSharpCode.TextEditor.TextEditorControl txtQuery;
		private System.Windows.Forms.ContextMenuStrip editorContextMenuStrip;
		protected System.ComponentModel.BackgroundWorker queryBackgroundWorker;
        private System.Windows.Forms.ContextMenuStrip ctxDataGrid;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
	}
}