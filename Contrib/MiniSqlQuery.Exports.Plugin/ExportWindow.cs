using System;
using System.Data;
using System.Windows.Forms;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Exports.Plugin
{
	public partial class ExportWindow : Form
	{
		private DataSet dsExecutedData;

		public ExportWindow()
		{
			InitializeComponent();
			txtFilePath.Text = string.Format("{0}\\export{1:yyyy-MM-dd}.htm",
			                                 Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DateTime.Today);
		}

		public string SetStatusText
		{
			set
			{
				toolStripStatusLabel1.Text = value;
				statusStrip1.Refresh();
			}
		}

		private void ExportWindow_Load(object sender, EventArgs e)
		{
			IQueryEditor editor = ApplicationServices.Instance.HostWindow.ActiveChildForm as IQueryEditor;

			if (editor != null)
			{
				dsExecutedData = editor.DataSet;
			}
			else
			{
				MessageBox.Show("Couldn't find a query windows, please open a query window and execute to get data to export.");
				Close();
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (rbtXml.Checked)
				ExportXml();

			if (rbtHtml.Checked)
				ExportHtml();

			if (rbtCsv.Checked)
				ExportCSV();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Create new SaveFileDialog object
			SaveFileDialog DialogSave = new SaveFileDialog();

			// Default file extension
			if (rbtCsv.Checked)
			{
				DialogSave.DefaultExt = "csv";
				DialogSave.FilterIndex = 2;
			}

			if (rbtHtml.Checked)
			{
				DialogSave.DefaultExt = "htm";
				DialogSave.FilterIndex = 1;
			}

			if (rbtXml.Checked)
			{
				DialogSave.DefaultExt = "xml";
				DialogSave.FilterIndex = 3;
			}

			//DialogSave.DefaultExt = "txt";

			// Available file extensions
			DialogSave.Filter = "Html File (*.htm)|*.htm|CSV File (*.csv)|*.csv|XML file (*.xml)|*.xml";

			// Adds a extension if the user does not
			DialogSave.AddExtension = true;

			// Restores the selected directory, next time
			DialogSave.RestoreDirectory = true;

			// Dialog title
			DialogSave.Title = "Where do you want to save the file?";

			// Startup directory
			DialogSave.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			// Show the dialog and process the result
			if (DialogSave.ShowDialog() == DialogResult.OK)
			{
				txtFilePath.Text = DialogSave.FileName;
				//MessageBox.Show("You selected the file: " + DialogSave.FileName);
			}

			DialogSave.Dispose();
		}

		//private int GetFieldCount
		//{
		//    get { return dsExecutedData.Tables[0].Columns.Count; }
		//}
		//private int GetRowCount
		//{
		//    get { return dsExecutedData.Tables[0].Rows.Count; }
		//}

		private void ExportHtml()
		{
			Export.HtmlExportFormat format = new Export.HtmlExportFormat();
			format.FontColor = txtFontColor.Text;
			format.FontFamily = txtFontFamily.Text;
			format.FontSize = txtFontSize.Text;

			format.HeaderColor = txtHeaderBGColor.Text;
			format.RowAltColor = txtRowBgAltColor.Text;
			format.RowColor = txtRowBgcolor.Text;

			Export.HtmlExport.OnWrittenData += CSVExport_OnWrittenData;
			Export.HtmlExport.ExportToHTML(dsExecutedData.Tables[0], txtFilePath.Text, format);

			#region Not used

			//StringBuilder sbCss = new StringBuilder();
			//StringBuilder sbHtml = new StringBuilder();
			//bool isAltSet = false;

			//sbCss.Append("<style>");
			//sbCss.Append("body { font-family:" + this.txtFontFamily.Text + "; font-size:" + this.txtFontSize.Text + "; color:" + this.txtFontColor.Text + "; }");
			//sbCss.Append(".Header {background-color:" + this.txtHeaderBGColor.Text + "}");
			//sbCss.Append(".Row    {background-color:" + this.txtRowBgcolor.Text + "}");
			//sbCss.Append(".AltRow    {background-color:" +  this.txtRowBgAltColor.Text + "}");
			//sbCss.Append("</style>");

			//this.SetStatusText = "Created style for html";

			//sbHtml.Append("<html>");
			//sbHtml.Append("<head><title>Export from " + dsExecutedData.Tables[0].TableName + "</title>");
			//sbHtml.Append(sbCss.ToString());
			//sbHtml.Append("</head>");
			//sbHtml.Append("<body>");

			//int fields = this.GetFieldCount;
			//sbHtml.Append("<table border='0' cellpadding='2'");
			//sbHtml.Append("<tr>");
			//for (int i = 0; i < fields; i++)
			//{
			//    sbHtml.Append(string.Format("<td class='Header'>{0}</td>", dsExecutedData.Tables[0].Columns[i].ColumnName));
			//    this.SetStatusText = "Writing column name " + i.ToString();
			//}
			//sbHtml.Append("</tr>");

			//int Counter = 0;
			//foreach (DataRow dr in dsExecutedData.Tables[0].Rows)
			//{
			//    sbHtml.Append("<tr>");

			//    for (int i = 0; i < fields; i++)
			//    {
			//        if (isAltSet)
			//        {
			//            sbHtml.Append(string.Format("<td class='AltRow'>{0}</td>", dr[i].ToString()));

			//        }
			//        else
			//        {
			//            sbHtml.Append(string.Format("<td class='Row'>{0}</td>", dr[i].ToString()));

			//        }
			//    }
			//    Counter++;
			//    this.SetStatusText = "Wring row " + Counter.ToString();
			//    sbHtml.Append("</tr>");

			//    if (isAltSet == false)
			//        isAltSet = true;
			//    else
			//        isAltSet = false;
			//}
			//sbHtml.Append("</table>");
			//sbHtml.Append("</body></html>");


			//System.IO.TextWriter tw = new System.IO.StreamWriter(this.txtFilePath.Text);
			//tw.WriteLine(sbHtml.ToString());
			//tw.Close();
			//this.SetStatusText = "Finished exporting to html file"; 

			#endregion
		}

		private void ExportCSV()
		{
			Export.CSVExport.OnWrittenData += CSVExport_OnWrittenData;
			Export.CSVExport.ExportToCSV(dsExecutedData.Tables[0], txtFilePath.Text, chkRowNames.Checked);
		}

		private void CSVExport_OnWrittenData(string text)
		{
			SetStatusText = text;
		}

		private void ExportXml()
		{
			dsExecutedData.Tables[0].WriteXml(txtFilePath.Text);
			SetStatusText = "Finished exporting to Xml file";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void rbtHtml_CheckedChanged(object sender, EventArgs e)
		{
			ChangeExtension("htm");
		}

		private void ChangeExtension(string extension)
		{
			if (!string.IsNullOrEmpty(txtFilePath.Text))
			{
				string p = txtFilePath.Text;
				int idx = p.LastIndexOf(".");
				p = p.Remove(idx);
				p = p + "." + extension;
				txtFilePath.Text = p;
			}
		}

		private void rbtCsv_CheckedChanged(object sender, EventArgs e)
		{
			ChangeExtension("csv");
		}

		private void rbtXml_CheckedChanged(object sender, EventArgs e)
		{
			ChangeExtension("xml");
		}
	}
}