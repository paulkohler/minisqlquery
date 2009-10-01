namespace MiniSqlQuery
{
	partial class AboutForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.pluginList = new MiniSqlQuery.Core.Controls.PluginListControl();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelProductName = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageReadMe = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.tabPageLicense = new System.Windows.Forms.TabPage();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.tabPagePlugins = new System.Windows.Forms.TabPage();
			this.pic = new System.Windows.Forms.PictureBox();
			this.tabControl1.SuspendLayout();
			this.tabPageReadMe.SuspendLayout();
			this.tabPageLicense.SuspendLayout();
			this.tabPagePlugins.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxDescription.Location = new System.Drawing.Point(12, 77);
			this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxDescription.Size = new System.Drawing.Size(582, 84);
			this.textBoxDescription.TabIndex = 23;
			this.textBoxDescription.TabStop = false;
			this.textBoxDescription.Text = "Description";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(519, 408);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// pluginList
			// 
			this.pluginList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pluginList.Location = new System.Drawing.Point(3, 3);
			this.pluginList.Name = "pluginList";
			this.pluginList.Size = new System.Drawing.Size(568, 203);
			this.pluginList.TabIndex = 0;
			// 
			// labelVersion
			// 
			this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelVersion.Location = new System.Drawing.Point(81, 35);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(513, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Version";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelVersion.Click += new System.EventHandler(this.labelVersion_Click);
			// 
			// labelProductName
			// 
			this.labelProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProductName.Location = new System.Drawing.Point(81, 9);
			this.labelProductName.Margin = new System.Windows.Forms.Padding(0);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(513, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Product Name";
			this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCopyright
			// 
			this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelCopyright.Location = new System.Drawing.Point(81, 52);
			this.labelCopyright.Margin = new System.Windows.Forms.Padding(0);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(513, 17);
			this.labelCopyright.TabIndex = 22;
			this.labelCopyright.Text = "Copyright";
			this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageReadMe);
			this.tabControl1.Controls.Add(this.tabPageLicense);
			this.tabControl1.Controls.Add(this.tabPagePlugins);
			this.tabControl1.Location = new System.Drawing.Point(12, 167);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(582, 235);
			this.tabControl1.TabIndex = 25;
			// 
			// tabPageReadMe
			// 
			this.tabPageReadMe.Controls.Add(this.webBrowser1);
			this.tabPageReadMe.Location = new System.Drawing.Point(4, 22);
			this.tabPageReadMe.Name = "tabPageReadMe";
			this.tabPageReadMe.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageReadMe.Size = new System.Drawing.Size(574, 209);
			this.tabPageReadMe.TabIndex = 2;
			this.tabPageReadMe.Text = "Read Me!";
			this.tabPageReadMe.UseVisualStyleBackColor = true;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(3, 3);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(568, 203);
			this.webBrowser1.TabIndex = 0;
			// 
			// tabPageLicense
			// 
			this.tabPageLicense.Controls.Add(this.txtLicense);
			this.tabPageLicense.Location = new System.Drawing.Point(4, 22);
			this.tabPageLicense.Name = "tabPageLicense";
			this.tabPageLicense.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLicense.Size = new System.Drawing.Size(574, 209);
			this.tabPageLicense.TabIndex = 3;
			this.tabPageLicense.Text = "License";
			this.tabPageLicense.UseVisualStyleBackColor = true;
			// 
			// txtLicense
			// 
			this.txtLicense.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLicense.Location = new System.Drawing.Point(3, 3);
			this.txtLicense.Multiline = true;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.ReadOnly = true;
			this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLicense.Size = new System.Drawing.Size(568, 203);
			this.txtLicense.TabIndex = 0;
			// 
			// tabPagePlugins
			// 
			this.tabPagePlugins.Controls.Add(this.pluginList);
			this.tabPagePlugins.Location = new System.Drawing.Point(4, 22);
			this.tabPagePlugins.Name = "tabPagePlugins";
			this.tabPagePlugins.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePlugins.Size = new System.Drawing.Size(574, 209);
			this.tabPagePlugins.TabIndex = 1;
			this.tabPagePlugins.Text = "Plugins";
			this.tabPagePlugins.UseVisualStyleBackColor = true;
			// 
			// pic
			// 
			this.pic.Location = new System.Drawing.Point(26, 20);
			this.pic.Name = "pic";
			this.pic.Size = new System.Drawing.Size(32, 32);
			this.pic.TabIndex = 26;
			this.pic.TabStop = false;
			// 
			// AboutForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(606, 443);
			this.Controls.Add(this.pic);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.labelCopyright);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.labelProductName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPageReadMe.ResumeLayout(false);
			this.tabPageLicense.ResumeLayout(false);
			this.tabPageLicense.PerformLayout();
			this.tabPagePlugins.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button okButton;
		private global::MiniSqlQuery.Core.Controls.PluginListControl pluginList;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPagePlugins;
		private System.Windows.Forms.PictureBox pic;
		private System.Windows.Forms.TabPage tabPageReadMe;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.TabPage tabPageLicense;
		private System.Windows.Forms.TextBox txtLicense;
	}
}
