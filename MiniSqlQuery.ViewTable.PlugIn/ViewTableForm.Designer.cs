namespace MiniSqlQuery.ViewTable.PlugIn
{
	partial class ViewTableForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTableForm));
			this.dataGridViewResult = new System.Windows.Forms.DataGridView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lnkRefresh = new System.Windows.Forms.LinkLabel();
			this.cboTableName = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewResult
			// 
			this.dataGridViewResult.AllowUserToAddRows = false;
			this.dataGridViewResult.AllowUserToDeleteRows = false;
			this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewResult.Location = new System.Drawing.Point(4, 77);
			this.dataGridViewResult.Name = "dataGridViewResult";
			this.dataGridViewResult.ReadOnly = true;
			this.dataGridViewResult.Size = new System.Drawing.Size(562, 286);
			this.dataGridViewResult.TabIndex = 0;
			this.dataGridViewResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewResult_DataError);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queryToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(4, 4);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(562, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// queryToolStripMenuItem
			// 
			this.queryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
			this.queryToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
			this.queryToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.queryToolStripMenuItem.Text = "&Query";
			this.queryToolStripMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.refreshToolStripMenuItem.Text = "&Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lnkRefresh);
			this.groupBox1.Controls.Add(this.cboTableName);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(4, 28);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(562, 49);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Table Name";
			// 
			// lnkRefresh
			// 
			this.lnkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkRefresh.AutoSize = true;
			this.lnkRefresh.Location = new System.Drawing.Point(485, 22);
			this.lnkRefresh.Name = "lnkRefresh";
			this.lnkRefresh.Size = new System.Drawing.Size(71, 13);
			this.lnkRefresh.TabIndex = 1;
			this.lnkRefresh.TabStop = true;
			this.lnkRefresh.Text = "Reload Table";
			this.lnkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRefresh_LinkClicked);
			// 
			// cboTableName
			// 
			this.cboTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTableName.FormattingEnabled = true;
			this.cboTableName.Location = new System.Drawing.Point(12, 19);
			this.cboTableName.Name = "cboTableName";
			this.cboTableName.Size = new System.Drawing.Size(467, 21);
			this.cboTableName.TabIndex = 0;
			// 
			// ViewTableForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(570, 367);
			this.Controls.Add(this.dataGridViewResult);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ViewTableForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.TabText = "ViewTableForm";
			this.Text = "ViewTableForm";
			this.Load += new System.EventHandler(this.ViewTableForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewResult;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cboTableName;
		private System.Windows.Forms.LinkLabel lnkRefresh;
	}
}