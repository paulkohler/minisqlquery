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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ResultsControl = new System.Windows.Forms.TabControl();
			this.tabPageResults = new System.Windows.Forms.TabPage();
			this.gridResults1 = new System.Windows.Forms.DataGridView();
			this.editorContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.ResultsControl.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ResultsControl);
			this.splitContainer1.Size = new System.Drawing.Size(879, 414);
			this.splitContainer1.SplitterDistance = 150;
			this.splitContainer1.TabIndex = 0;
			// 
			// txtQuery
			// 
			this.txtQuery.ContextMenuStrip = this.contextMenuStripQuery;
			this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQuery.EnableFolding = false;
			this.txtQuery.IsIconBarVisible = false;
			this.txtQuery.Location = new System.Drawing.Point(0, 0);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.ShowEOLMarkers = true;
			this.txtQuery.ShowInvalidLines = false;
			this.txtQuery.ShowSpaces = true;
			this.txtQuery.ShowTabs = true;
			this.txtQuery.ShowVRuler = true;
			this.txtQuery.Size = new System.Drawing.Size(879, 150);
			this.txtQuery.TabIndex = 1;
			// 
			// contextMenuStripQuery
			// 
			this.contextMenuStripQuery.Name = "contextMenuStripQuery";
			this.contextMenuStripQuery.Size = new System.Drawing.Size(61, 4);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queryToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(879, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// queryToolStripMenuItem
			// 
			this.queryToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
			this.queryToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.queryToolStripMenuItem.Text = "&Query";
			// 
			// tabControlResults
			// 
			this.ResultsControl.Controls.Add(this.tabPageResults);
			this.ResultsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultsControl.Location = new System.Drawing.Point(0, 0);
			this.ResultsControl.Name = "ResultsControl";
			this.ResultsControl.SelectedIndex = 0;
			this.ResultsControl.Size = new System.Drawing.Size(879, 260);
			this.ResultsControl.TabIndex = 0;
			// 
			// tabPageResults
			// 
			this.tabPageResults.Controls.Add(this.gridResults1);
			this.tabPageResults.Location = new System.Drawing.Point(4, 22);
			this.tabPageResults.Name = "tabPageResults";
			this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageResults.Size = new System.Drawing.Size(871, 234);
			this.tabPageResults.TabIndex = 0;
			this.tabPageResults.Text = "Results";
			this.tabPageResults.UseVisualStyleBackColor = true;
			// 
			// gridResults1
			// 
			this.gridResults1.AllowUserToAddRows = false;
			this.gridResults1.AllowUserToDeleteRows = false;
			this.gridResults1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridResults1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridResults1.Location = new System.Drawing.Point(3, 3);
			this.gridResults1.Name = "gridResults1";
			this.gridResults1.ReadOnly = true;
			this.gridResults1.Size = new System.Drawing.Size(865, 228);
			this.gridResults1.TabIndex = 0;
			// 
			// editorContextMenuStrip
			// 
			this.editorContextMenuStrip.Name = "editorContextMenuStrip";
			this.editorContextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// QueryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 414);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "QueryForm";
			this.TabPageContextMenuStrip = this.editorContextMenuStrip;
			this.TabText = "Query";
			this.Text = "Query";
			this.Deactivate += new System.EventHandler(this.QueryForm_Deactivate);
			this.Load += new System.EventHandler(this.QueryForm_Load);
			this.Activated += new System.EventHandler(this.QueryForm_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryForm_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResultsControl.ResumeLayout(false);
			this.tabPageResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridResults1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabPage tabPageResults;
		private System.Windows.Forms.DataGridView gridResults1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripQuery;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
		private ICSharpCode.TextEditor.TextEditorControl txtQuery;
		private System.Windows.Forms.ContextMenuStrip editorContextMenuStrip;
	}
}