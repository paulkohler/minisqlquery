namespace MiniSqlQuery.Core.Controls
{
	partial class ExceptionControl
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
			this.lblError = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtDetails = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lnkCopy = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// lblError
			// 
			this.lblError.AutoSize = true;
			this.lblError.Location = new System.Drawing.Point(3, 0);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(29, 13);
			this.lblError.TabIndex = 0;
			this.lblError.Text = "Error";
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(6, 16);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMessage.Size = new System.Drawing.Size(428, 54);
			this.txtMessage.TabIndex = 1;
			// 
			// txtDetails
			// 
			this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDetails.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDetails.Location = new System.Drawing.Point(6, 94);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.ReadOnly = true;
			this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDetails.Size = new System.Drawing.Size(428, 142);
			this.txtDetails.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Details";
			// 
			// lnkCopy
			// 
			this.lnkCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkCopy.AutoSize = true;
			this.lnkCopy.Location = new System.Drawing.Point(344, 239);
			this.lnkCopy.Name = "lnkCopy";
			this.lnkCopy.Size = new System.Drawing.Size(90, 13);
			this.lnkCopy.TabIndex = 4;
			this.lnkCopy.TabStop = true;
			this.lnkCopy.Text = "Copy to Clipboard";
			this.lnkCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCopy_LinkClicked);
			// 
			// ExceptionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lnkCopy);
			this.Controls.Add(this.txtDetails);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.lblError);
			this.Name = "ExceptionControl";
			this.Size = new System.Drawing.Size(437, 256);
			this.Load += new System.EventHandler(this.ExceptionControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.TextBox txtDetails;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lnkCopy;
	}
}
