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
            this.toolStripButtonCopyAsNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditConnStr = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTest = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripButtonDelete,
            this.toolStripSeparator2,
            this.toolStripButtonTest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 342);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(881, 27);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOk
            // 
            this.toolStripButtonOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOk.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOk.Image")));
            this.toolStripButtonOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOk.Name = "toolStripButtonOk";
            this.toolStripButtonOk.Size = new System.Drawing.Size(33, 24);
            this.toolStripButtonOk.Text = "&OK";
            this.toolStripButtonOk.Click += new System.EventHandler(this.toolStripButtonOk_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(57, 24);
            this.toolStripButtonCancel.Text = "&Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(57, 24);
            this.toolStripButtonAdd.Text = "Add";
            this.toolStripButtonAdd.ToolTipText = "Add";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonCopyAsNew
            // 
            this.toolStripButtonCopyAsNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyAsNew.Image")));
            this.toolStripButtonCopyAsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopyAsNew.Name = "toolStripButtonCopyAsNew";
            this.toolStripButtonCopyAsNew.Size = new System.Drawing.Size(115, 24);
            this.toolStripButtonCopyAsNew.Text = "Copy as New";
            this.toolStripButtonCopyAsNew.Click += new System.EventHandler(this.toolStripButtonCopyAsNew_Click);
            // 
            // toolStripButtonEditConnStr
            // 
            this.toolStripButtonEditConnStr.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditConnStr.Image")));
            this.toolStripButtonEditConnStr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditConnStr.Name = "toolStripButtonEditConnStr";
            this.toolStripButtonEditConnStr.Size = new System.Drawing.Size(55, 24);
            this.toolStripButtonEditConnStr.Text = "Edit";
            this.toolStripButtonEditConnStr.Click += new System.EventHandler(this.toolStripButtonEditConnStr_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(73, 24);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonTest
            // 
            this.toolStripButtonTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonTest.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTest.Image")));
            this.toolStripButtonTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTest.Name = "toolStripButtonTest";
            this.toolStripButtonTest.Size = new System.Drawing.Size(49, 24);
            this.toolStripButtonTest.Text = "Test...";
            this.toolStripButtonTest.Click += new System.EventHandler(this.toolStripButtonTest_Click);
            // 
            // lstConnections
            // 
            this.lstConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstConnections.FormattingEnabled = true;
            this.lstConnections.ItemHeight = 16;
            this.lstConnections.Location = new System.Drawing.Point(16, 15);
            this.lstConnections.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstConnections.Name = "lstConnections";
            this.lstConnections.Size = new System.Drawing.Size(335, 292);
            this.lstConnections.TabIndex = 5;
            this.lstConnections.SelectedValueChanged += new System.EventHandler(this.lstConnections_SelectedValueChanged);
            this.lstConnections.DoubleClick += new System.EventHandler(this.lstConnections_DoubleClick);
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
            this.grpDetails.Location = new System.Drawing.Point(360, 15);
            this.grpDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDetails.Size = new System.Drawing.Size(505, 319);
            this.grpDetails.TabIndex = 6;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(133, 206);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(363, 105);
            this.txtComment.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 209);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Comment";
            // 
            // txtConn
            // 
            this.txtConn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConn.Location = new System.Drawing.Point(133, 87);
            this.txtConn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConn.Multiline = true;
            this.txtConn.Name = "txtConn";
            this.txtConn.ReadOnly = true;
            this.txtConn.Size = new System.Drawing.Size(363, 110);
            this.txtConn.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Connection";
            // 
            // txtProvider
            // 
            this.txtProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProvider.Location = new System.Drawing.Point(133, 55);
            this.txtProvider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.ReadOnly = true;
            this.txtProvider.Size = new System.Drawing.Size(363, 22);
            this.txtProvider.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Provider";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(133, 23);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(363, 22);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // DbConnectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 369);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.lstConnections);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbConnectionsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Connection List Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbConnectionsForm_FormClosing);
            this.Load += new System.EventHandler(this.DbConnectionsForm_Load);
            this.Shown += new System.EventHandler(this.DbConnectionsForm_Shown);
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonTest;
	}
}