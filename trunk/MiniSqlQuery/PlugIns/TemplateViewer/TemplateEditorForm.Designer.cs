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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageTemplateSource = new System.Windows.Forms.TabPage();
			this.tabPageHelp = new System.Windows.Forms.TabPage();
			this.rtfHelp = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtErrors = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPageTemplateSource.SuspendLayout();
			this.tabPageHelp.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.IsReadOnly = false;
			this.txtEdit.Location = new System.Drawing.Point(3, 3);
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(533, 315);
			this.txtEdit.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageTemplateSource);
			this.tabControl1.Controls.Add(this.tabPageHelp);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(547, 347);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPageTemplateSource
			// 
			this.tabPageTemplateSource.Controls.Add(this.txtEdit);
			this.tabPageTemplateSource.Location = new System.Drawing.Point(4, 22);
			this.tabPageTemplateSource.Name = "tabPageTemplateSource";
			this.tabPageTemplateSource.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTemplateSource.Size = new System.Drawing.Size(539, 321);
			this.tabPageTemplateSource.TabIndex = 0;
			this.tabPageTemplateSource.Text = "Template Source";
			this.tabPageTemplateSource.UseVisualStyleBackColor = true;
			// 
			// tabPageHelp
			// 
			this.tabPageHelp.Controls.Add(this.rtfHelp);
			this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
			this.tabPageHelp.Name = "tabPageHelp";
			this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHelp.Size = new System.Drawing.Size(500, 231);
			this.tabPageHelp.TabIndex = 1;
			this.tabPageHelp.Text = "Quick Help";
			this.tabPageHelp.UseVisualStyleBackColor = true;
			// 
			// rtfHelp
			// 
			this.rtfHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtfHelp.Location = new System.Drawing.Point(3, 3);
			this.rtfHelp.Name = "rtfHelp";
			this.rtfHelp.ReadOnly = true;
			this.rtfHelp.ShowSelectionMargin = true;
			this.rtfHelp.Size = new System.Drawing.Size(494, 225);
			this.rtfHelp.TabIndex = 0;
			this.rtfHelp.Text = "";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(4, 4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtErrors);
			this.splitContainer1.Size = new System.Drawing.Size(547, 437);
			this.splitContainer1.SplitterDistance = 347;
			this.splitContainer1.TabIndex = 2;
			// 
			// txtErrors
			// 
			this.txtErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtErrors.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtErrors.Location = new System.Drawing.Point(0, 0);
			this.txtErrors.Multiline = true;
			this.txtErrors.Name = "txtErrors";
			this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtErrors.Size = new System.Drawing.Size(547, 86);
			this.txtErrors.TabIndex = 0;
			// 
			// TemplateEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 445);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TemplateEditorForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "TemplateEditorForm";
			this.Load += new System.EventHandler(this.TemplateEditorForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplateEditorForm_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabPageTemplateSource.ResumeLayout(false);
			this.tabPageHelp.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ICSharpCode.TextEditor.TextEditorControl txtEdit;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageTemplateSource;
		private System.Windows.Forms.TabPage tabPageHelp;
		private System.Windows.Forms.RichTextBox rtfHelp;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtErrors;
	}
}
