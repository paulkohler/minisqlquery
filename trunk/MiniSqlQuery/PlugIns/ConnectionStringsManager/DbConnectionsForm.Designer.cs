namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	partial class DbConnectionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbConnectionsForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOk = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonEditConnStr = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.lstConnections = new System.Windows.Forms.ListBox();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtConn = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtProvider = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStripButtonCopyAsNew = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.grpDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOk,
            this.toolStripButtonCancel,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripButtonCopyAsNew,
            this.toolStripButtonEditConnStr,
            this.toolStripButtonDelete});
			this.toolStrip1.Location = new System.Drawing.Point(0, 275);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(661, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonOk
			// 
			this.toolStripButtonOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonOk.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOk.Image")));
			this.toolStripButtonOk.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOk.Name = "toolStripButtonOk";
			this.toolStripButtonOk.Size = new System.Drawing.Size(27, 22);
			this.toolStripButtonOk.Text = "&OK";
			this.toolStripButtonOk.Click += new System.EventHandler(this.toolStripButtonOk_Click);
			// 
			// toolStripButtonCancel
			// 
			this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
			this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCancel.Name = "toolStripButtonCancel";
			this.toolStripButtonCancel.Size = new System.Drawing.Size(47, 22);
			this.toolStripButtonCancel.Text = "&Cancel";
			this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonAdd
			// 
			this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
			this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAdd.Name = "toolStripButtonAdd";
			this.toolStripButtonAdd.Size = new System.Drawing.Size(49, 22);
			this.toolStripButtonAdd.Text = "Add";
			this.toolStripButtonAdd.ToolTipText = "Add";
			this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
			// 
			// toolStripButtonEditConnStr
			// 
			this.toolStripButtonEditConnStr.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditConnStr.Image")));
			this.toolStripButtonEditConnStr.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEditConnStr.Name = "toolStripButtonEditConnStr";
			this.toolStripButtonEditConnStr.Size = new System.Drawing.Size(47, 22);
			this.toolStripButtonEditConnStr.Text = "Edit";
			this.toolStripButtonEditConnStr.Click += new System.EventHandler(this.toolStripButtonEditConnStr_Click);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(60, 22);
			this.toolStripButtonDelete.Text = "Delete";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			// 
			// lstConnections
			// 
			this.lstConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.lstConnections.FormattingEnabled = true;
			this.lstConnections.Location = new System.Drawing.Point(12, 12);
			this.lstConnections.Name = "lstConnections";
			this.lstConnections.Size = new System.Drawing.Size(252, 238);
			this.lstConnections.TabIndex = 5;
			this.lstConnections.DoubleClick += new System.EventHandler(this.lstConnections_DoubleClick);
			this.lstConnections.SelectedValueChanged += new System.EventHandler(this.lstConnections_SelectedValueChanged);
			// 
			// grpDetails
			// 
			this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpDetails.Controls.Add(this.txtComment);
			this.grpDetails.Controls.Add(this.label4);
			this.grpDetails.Controls.Add(this.txtConn);
			this.grpDetails.Controls.Add(this.label3);
			this.grpDetails.Controls.Add(this.txtProvider);
			this.grpDetails.Controls.Add(this.label2);
			this.grpDetails.Controls.Add(this.txtName);
			this.grpDetails.Controls.Add(this.label1);
			this.grpDetails.Location = new System.Drawing.Point(270, 12);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(379, 259);
			this.grpDetails.TabIndex = 6;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Details";
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtComment.Location = new System.Drawing.Point(100, 167);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.ReadOnly = true;
			this.txtComment.Size = new System.Drawing.Size(273, 86);
			this.txtComment.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 170);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Comment";
			// 
			// txtConn
			// 
			this.txtConn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConn.Location = new System.Drawing.Point(100, 71);
			this.txtConn.Multiline = true;
			this.txtConn.Name = "txtConn";
			this.txtConn.ReadOnly = true;
			this.txtConn.Size = new System.Drawing.Size(273, 90);
			this.txtConn.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Connection";
			// 
			// txtProvider
			// 
			this.txtProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtProvider.Location = new System.Drawing.Point(100, 45);
			this.txtProvider.Name = "txtProvider";
			this.txtProvider.ReadOnly = true;
			this.txtProvider.Size = new System.Drawing.Size(273, 20);
			this.txtProvider.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Provider";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(100, 19);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = true;
			this.txtName.Size = new System.Drawing.Size(273, 20);
			this.txtName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// toolStripButtonCopyAsNew
			// 
			this.toolStripButtonCopyAsNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyAsNew.Image")));
			this.toolStripButtonCopyAsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCopyAsNew.Name = "toolStripButtonCopyAsNew";
			this.toolStripButtonCopyAsNew.Size = new System.Drawing.Size(96, 22);
			this.toolStripButtonCopyAsNew.Text = "Copy as New";
			this.toolStripButtonCopyAsNew.Click += new System.EventHandler(this.toolStripButtonCopyAsNew_Click);
			// 
			// DbConnectionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 300);
			this.Controls.Add(this.grpDetails);
			this.Controls.Add(this.lstConnections);
			this.Controls.Add(this.toolStrip1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DbConnectionsForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Connection List Editor";
			this.Shown += new System.EventHandler(this.DbConnectionsForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbConnectionsForm_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.grpDetails.ResumeLayout(false);
			this.grpDetails.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonOk;
		private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonEditConnStr;
		private System.Windows.Forms.ListBox lstConnections;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.TextBox txtProvider;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtConn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private System.Windows.Forms.ToolStripButton toolStripButtonCopyAsNew;
	}
}