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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelTanks = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelIcons = new System.Windows.Forms.LinkLabel();
            this.linkLabelTextEditor = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabelDockPanel = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanelTanks.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.labelVersion.Location = new System.Drawing.Point(9, 35);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(585, 17);
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
            this.labelProductName.Location = new System.Drawing.Point(9, 9);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(0);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(585, 17);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.Location = new System.Drawing.Point(9, 52);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(0);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(585, 17);
            this.labelCopyright.TabIndex = 22;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 167);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 235);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanelTanks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 209);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thanks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelTanks
            // 
            this.flowLayoutPanelTanks.Controls.Add(this.label1);
            this.flowLayoutPanelTanks.Controls.Add(this.linkLabelIcons);
            this.flowLayoutPanelTanks.Controls.Add(this.linkLabelTextEditor);
            this.flowLayoutPanelTanks.Controls.Add(this.linkLabelDockPanel);
            this.flowLayoutPanelTanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTanks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelTanks.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelTanks.Name = "flowLayoutPanelTanks";
            this.flowLayoutPanelTanks.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelTanks.Size = new System.Drawing.Size(568, 203);
            this.flowLayoutPanelTanks.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.label1.Size = new System.Drawing.Size(554, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mini SQL Query relies on sever other \'free\' products to pull together a simple bu" +
                "t useful SQL working environment. In no particular order, thank you...";
            // 
            // linkLabelIcons
            // 
            this.linkLabelIcons.AutoSize = true;
            this.linkLabelIcons.LinkArea = new System.Windows.Forms.LinkArea(20, 13);
            this.linkLabelIcons.Location = new System.Drawing.Point(7, 42);
            this.linkLabelIcons.Name = "linkLabelIcons";
            this.linkLabelIcons.Padding = new System.Windows.Forms.Padding(6);
            this.linkLabelIcons.Size = new System.Drawing.Size(271, 29);
            this.linkLabelIcons.TabIndex = 1;
            this.linkLabelIcons.TabStop = true;
            this.linkLabelIcons.Text = "Mark James, for the Silk icon set (famfamfam.com)";
            this.linkLabelIcons.UseCompatibleTextRendering = true;
            this.linkLabelIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelIcons_LinkClicked);
            // 
            // linkLabelTextEditor
            // 
            this.linkLabelTextEditor.AutoSize = true;
            this.linkLabelTextEditor.LinkArea = new System.Windows.Forms.LinkArea(16, 20);
            this.linkLabelTextEditor.Location = new System.Drawing.Point(7, 71);
            this.linkLabelTextEditor.Name = "linkLabelTextEditor";
            this.linkLabelTextEditor.Padding = new System.Windows.Forms.Padding(6);
            this.linkLabelTextEditor.Size = new System.Drawing.Size(550, 42);
            this.linkLabelTextEditor.TabIndex = 1;
            this.linkLabelTextEditor.TabStop = true;
            this.linkLabelTextEditor.Text = "ic#code for the SharpDevelop project where I get the ICSharpCode.TextEditor from " +
                "that removes the need for using an plain old textbox (www.icsharpcode.net)";
            this.linkLabelTextEditor.UseCompatibleTextRendering = true;
            this.linkLabelTextEditor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTextEditor_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pluginList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(574, 209);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plugins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabelDockPanel
            // 
            this.linkLabelDockPanel.AutoSize = true;
            this.linkLabelDockPanel.LinkArea = new System.Windows.Forms.LinkArea(41, 13);
            this.linkLabelDockPanel.Location = new System.Drawing.Point(7, 113);
            this.linkLabelDockPanel.Name = "linkLabelDockPanel";
            this.linkLabelDockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.linkLabelDockPanel.Size = new System.Drawing.Size(292, 29);
            this.linkLabelDockPanel.TabIndex = 2;
            this.linkLabelDockPanel.TabStop = true;
            this.linkLabelDockPanel.Text = "Weifen Luo for his extremely easy to use docking suite.";
            this.linkLabelDockPanel.UseCompatibleTextRendering = true;
            this.linkLabelDockPanel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDockPanel_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(606, 443);
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
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanelTanks.ResumeLayout(false);
            this.flowLayoutPanelTanks.PerformLayout();
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTanks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelIcons;
        private System.Windows.Forms.LinkLabel linkLabelTextEditor;
        private System.Windows.Forms.LinkLabel linkLabelDockPanel;
	}
}
