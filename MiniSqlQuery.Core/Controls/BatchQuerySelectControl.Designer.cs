namespace MiniSqlQuery.Core.Controls
{
	partial class BatchQuerySelectControl
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
			this.lstBatches = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lstBatches
			// 
			this.lstBatches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstBatches.FormattingEnabled = true;
			this.lstBatches.Location = new System.Drawing.Point(0, 0);
			this.lstBatches.Name = "lstBatches";
			this.lstBatches.Size = new System.Drawing.Size(293, 160);
			this.lstBatches.TabIndex = 0;
			this.lstBatches.SelectedIndexChanged += new System.EventHandler(this.lstBatches_SelectedIndexChanged);
			// 
			// BatchQuerySelectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lstBatches);
			this.Name = "BatchQuerySelectControl";
			this.Size = new System.Drawing.Size(293, 163);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstBatches;
	}
}
