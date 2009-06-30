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
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// tvTemplates
			// 
			this.tvTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
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
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "folder_page");
			this.imageList.Images.SetKeyName(1, "script");
			this.imageList.Images.SetKeyName(2, "script_code");
			// 
			// TemplateViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.tvTemplates);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TemplateViewForm";
			this.TabText = "Templates";
			this.Text = "Templates";
			this.Load += new System.EventHandler(this.TemplateViewForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvTemplates;
		private System.Windows.Forms.ImageList imageList;
	}
}