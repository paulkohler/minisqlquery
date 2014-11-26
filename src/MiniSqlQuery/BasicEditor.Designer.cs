namespace MiniSqlQuery
{
	partial class BasicEditor
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
			this.txtEdit = new ICSharpCode.TextEditor.TextEditorControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblEditorInfo = new System.Windows.Forms.Label();
			this.formContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.IsReadOnly = false;
			this.txtEdit.Location = new System.Drawing.Point(0, 0);
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(381, 325);
			this.txtEdit.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblEditorInfo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 304);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(381, 21);
			this.panel1.TabIndex = 1;
			// 
			// lblEditorInfo
			// 
			this.lblEditorInfo.AutoSize = true;
			this.lblEditorInfo.Location = new System.Drawing.Point(3, 3);
			this.lblEditorInfo.Name = "lblEditorInfo";
			this.lblEditorInfo.Size = new System.Drawing.Size(62, 13);
			this.lblEditorInfo.TabIndex = 0;
			this.lblEditorInfo.Text = "lblEditorInfo";
			// 
			// formContextMenuStrip
			// 
			this.formContextMenuStrip.Name = "formContextMenuStrip";
			this.formContextMenuStrip.Size = new System.Drawing.Size(153, 26);
			// 
			// BasicEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 325);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.txtEdit);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "BasicEditor";
			this.TabPageContextMenuStrip = this.formContextMenuStrip;
			this.Text = "DefaultEditor";
			this.Load += new System.EventHandler(this.BasicEditor_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BasicEditor_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private ICSharpCode.TextEditor.TextEditorControl txtEdit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblEditorInfo;
		private System.Windows.Forms.ContextMenuStrip formContextMenuStrip;
	}
}