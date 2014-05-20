namespace MiniSqlQuery.PlugIns.DatabaseInspector
{
	partial class FindObjectForm
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
            this.cboObjects = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboObjects
            // 
            this.cboObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboObjects.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboObjects.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboObjects.Location = new System.Drawing.Point(0, 0);
            this.cboObjects.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboObjects.Name = "cboObjects";
            this.cboObjects.Size = new System.Drawing.Size(684, 24);
            this.cboObjects.TabIndex = 0;
            // 
            // FindObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 26);
            this.Controls.Add(this.cboObjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FindObjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Object";
            this.Load += new System.EventHandler(this.FindObjectForm_Load);
            this.Shown += new System.EventHandler(this.FindObjectForm_Shown);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cboObjects;
	}
}