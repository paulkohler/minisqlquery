namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	partial class TemplateEditorForm : ITemplateEditor
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
			this.txtEdit = new ICSharpCode.TextEditor.TextEditorControl();
			this.SuspendLayout();
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.IsReadOnly = false;
			this.txtEdit.Location = new System.Drawing.Point(0, 0);
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(446, 312);
			this.txtEdit.TabIndex = 0;
			// 
			// TemplateEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446, 312);
			this.Controls.Add(this.txtEdit);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TemplateEditorForm";
			this.Text = "TemplateEditorForm";
			this.Load += new System.EventHandler(this.TemplateEditorForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private ICSharpCode.TextEditor.TextEditorControl txtEdit;
	}
}
