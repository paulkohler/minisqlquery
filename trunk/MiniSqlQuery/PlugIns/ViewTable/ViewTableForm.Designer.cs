namespace MiniSqlQuery.PlugIns.ViewTable
{
	partial class ViewTableForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTableForm));
			this.dataGridViewResult = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkAutoReload = new System.Windows.Forms.CheckBox();
			this.lnkRefresh = new System.Windows.Forms.LinkLabel();
			this.cboTableName = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewResult
			// 
			this.dataGridViewResult.AllowUserToAddRows = false;
			this.dataGridViewResult.AllowUserToDeleteRows = false;
			this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.NullValue = "<NULL>";
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewResult.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewResult.Location = new System.Drawing.Point(4, 58);
			this.dataGridViewResult.Name = "dataGridViewResult";
			this.dataGridViewResult.ReadOnly = true;
			this.dataGridViewResult.Size = new System.Drawing.Size(562, 305);
			this.dataGridViewResult.TabIndex = 0;
			this.dataGridViewResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewResult_DataError);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkAutoReload);
			this.groupBox1.Controls.Add(this.lnkRefresh);
			this.groupBox1.Controls.Add(this.cboTableName);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(562, 54);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Table Name";
			// 
			// chkAutoReload
			// 
			this.chkAutoReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAutoReload.AutoSize = true;
			this.chkAutoReload.Checked = true;
			this.chkAutoReload.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoReload.Location = new System.Drawing.Point(472, 33);
			this.chkAutoReload.Name = "chkAutoReload";
			this.chkAutoReload.Size = new System.Drawing.Size(85, 17);
			this.chkAutoReload.TabIndex = 2;
			this.chkAutoReload.Text = "Auto Reload";
			this.toolTip1.SetToolTip(this.chkAutoReload, "Automatically reload the table when a \'Truncate\' action is performed.");
			this.chkAutoReload.UseVisualStyleBackColor = true;
			// 
			// lnkRefresh
			// 
			this.lnkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkRefresh.AutoSize = true;
			this.lnkRefresh.Location = new System.Drawing.Point(469, 16);
			this.lnkRefresh.Name = "lnkRefresh";
			this.lnkRefresh.Size = new System.Drawing.Size(71, 13);
			this.lnkRefresh.TabIndex = 1;
			this.lnkRefresh.TabStop = true;
			this.lnkRefresh.Text = "Reload Table";
			this.toolTip1.SetToolTip(this.lnkRefresh, "Reload the selected table now.");
			this.lnkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRefresh_LinkClicked);
			// 
			// cboTableName
			// 
			this.cboTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTableName.FormattingEnabled = true;
			this.cboTableName.Location = new System.Drawing.Point(12, 19);
			this.cboTableName.MaxDropDownItems = 20;
			this.cboTableName.Name = "cboTableName";
			this.cboTableName.Size = new System.Drawing.Size(454, 21);
			this.cboTableName.TabIndex = 0;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// ViewTableForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(570, 367);
			this.Controls.Add(this.dataGridViewResult);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ViewTableForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.TabText = "ViewTableForm";
			this.Text = "ViewTableForm";
			this.Shown += new System.EventHandler(this.ViewTableForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewResult;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cboTableName;
		private System.Windows.Forms.LinkLabel lnkRefresh;
		private System.Windows.Forms.CheckBox chkAutoReload;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
	}
}