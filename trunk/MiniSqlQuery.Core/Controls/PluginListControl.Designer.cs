namespace MiniSqlQuery.Core.Controls
{
	partial class PluginListControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAssembly = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderAssembly});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(361, 146);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 150;
			// 
			// columnHeaderDescription
			// 
			this.columnHeaderDescription.Text = "Description";
			this.columnHeaderDescription.Width = 200;
			// 
			// columnHeaderAssembly
			// 
			this.columnHeaderAssembly.Text = "Assembly";
			this.columnHeaderAssembly.Width = 200;
			// 
			// PluginListControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Name = "PluginListControl";
			this.Size = new System.Drawing.Size(361, 146);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderAssembly;
	}
}
