namespace MiniSqlQuery.ConnectionStringsManager.PlugIn
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
			this.txtConns = new ICSharpCode.TextEditor.TextEditorControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOk = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSplitButtonInsertProvider = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripButtonEditConnStr = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtConns
			// 
			this.txtConns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtConns.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.None;
			this.txtConns.IsIconBarVisible = false;
			this.txtConns.Location = new System.Drawing.Point(12, 12);
			this.txtConns.Name = "txtConns";
			this.txtConns.ShowEOLMarkers = true;
			this.txtConns.ShowInvalidLines = false;
			this.txtConns.ShowLineNumbers = false;
			this.txtConns.ShowSpaces = true;
			this.txtConns.ShowTabs = true;
			this.txtConns.ShowVRuler = true;
			this.txtConns.Size = new System.Drawing.Size(730, 330);
			this.txtConns.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOk,
            this.toolStripButtonCancel,
            this.toolStripSeparator1,
            this.toolStripSplitButtonInsertProvider,
            this.toolStripButtonEditConnStr});
			this.toolStrip1.Location = new System.Drawing.Point(0, 345);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(754, 25);
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
			// toolStripSplitButtonInsertProvider
			// 
			this.toolStripSplitButtonInsertProvider.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButtonInsertProvider.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonInsertProvider.Image")));
			this.toolStripSplitButtonInsertProvider.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonInsertProvider.Name = "toolStripSplitButtonInsertProvider";
			this.toolStripSplitButtonInsertProvider.Size = new System.Drawing.Size(134, 22);
			this.toolStripSplitButtonInsertProvider.Text = "&Insert Provider Name";
			this.toolStripSplitButtonInsertProvider.Visible = false;
			// 
			// toolStripButtonEditConnStr
			// 
			this.toolStripButtonEditConnStr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonEditConnStr.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditConnStr.Image")));
			this.toolStripButtonEditConnStr.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEditConnStr.Name = "toolStripButtonEditConnStr";
			this.toolStripButtonEditConnStr.Size = new System.Drawing.Size(130, 22);
			this.toolStripButtonEditConnStr.Text = "Edit Connection String";
			this.toolStripButtonEditConnStr.Click += new System.EventHandler(this.toolStripButtonEditConnStr_Click);
			// 
			// DbConnectionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(754, 370);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.txtConns);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DbConnectionsForm";
			this.ShowIcon = false;
			this.Text = "Database Connection List Editor";
			this.Load += new System.EventHandler(this.DbConnectionsForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbConnectionsForm_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ICSharpCode.TextEditor.TextEditorControl txtConns;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonOk;
		private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonInsertProvider;
		private System.Windows.Forms.ToolStripButton toolStripButtonEditConnStr;
	}
}