namespace MiniSqlQuery.PlugIns.ConnectionStringsManager
{
	partial class ConnectionStringBuilderForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionStringBuilderForm));
			this.cboProvider = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.propertyGridDbConnection = new System.Windows.Forms.PropertyGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.txtConnectionName = new System.Windows.Forms.TextBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOk = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboProvider
			// 
			this.cboProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboProvider.FormattingEnabled = true;
			this.cboProvider.Location = new System.Drawing.Point(120, 94);
			this.cboProvider.Name = "cboProvider";
			this.cboProvider.Size = new System.Drawing.Size(295, 21);
			this.cboProvider.TabIndex = 5;
			this.cboProvider.SelectedIndexChanged += new System.EventHandler(this.cboProvider_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 97);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Provider";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.propertyGridDbConnection);
			this.groupBox1.Location = new System.Drawing.Point(12, 121);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(403, 288);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection Settings";
			// 
			// propertyGridDbConnection
			// 
			this.propertyGridDbConnection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridDbConnection.Location = new System.Drawing.Point(3, 16);
			this.propertyGridDbConnection.Name = "propertyGridDbConnection";
			this.propertyGridDbConnection.Size = new System.Drawing.Size(397, 269);
			this.propertyGridDbConnection.TabIndex = 0;
			this.propertyGridDbConnection.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridDbConnection_PropertyValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Name";
			// 
			// txtConnectionName
			// 
			this.txtConnectionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionName.Location = new System.Drawing.Point(120, 12);
			this.txtConnectionName.Name = "txtConnectionName";
			this.txtConnectionName.Size = new System.Drawing.Size(295, 20);
			this.txtConnectionName.TabIndex = 1;
			this.txtConnectionName.TextChanged += new System.EventHandler(this.ItemsTextChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOk,
            this.toolStripButtonCancel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 412);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(427, 25);
			this.toolStrip1.TabIndex = 15;
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
			// txtComments
			// 
			this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtComments.Location = new System.Drawing.Point(120, 38);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(295, 50);
			this.txtComments.TabIndex = 3;
			this.txtComments.TextChanged += new System.EventHandler(this.ItemsTextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Comments";
			// 
			// ConnectionStringBuilderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(427, 437);
			this.Controls.Add(this.txtComments);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.txtConnectionName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboProvider);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionStringBuilderForm";
			this.Text = "Connection String Builder";
			this.Load += new System.EventHandler(this.ConnectionStringBuilderForm_Load);
			this.Shown += new System.EventHandler(this.ConnectionStringBuilderForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionStringBuilderForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboProvider;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtConnectionName;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
		private System.Windows.Forms.ToolStripButton toolStripButtonOk;
		private System.Windows.Forms.PropertyGrid propertyGridDbConnection;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.Label label3;
	}
}