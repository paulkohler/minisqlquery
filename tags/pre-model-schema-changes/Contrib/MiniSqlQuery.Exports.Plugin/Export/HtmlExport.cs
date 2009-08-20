using System;
using System.Data;
using System.Text;

namespace MiniSqlQuery.Exports.Plugin.Export
{
	public class HtmlExport
	{
		public delegate void WrittenData(string text);

		public static event WrittenData OnWrittenData;

		public static void ExportToHTML(DataTable source, string fileName, HtmlExportFormat format)
		{
			StringBuilder sbCss = new StringBuilder();
			StringBuilder sbHtml = new StringBuilder();
			bool isAltSet = false;

			sbCss.Append("<style>");
			sbCss.Append("body { font-family:" + format.FontFamily + "; font-size:" + format.FontSize + "; color:" + format.FontColor + "; }");
			sbCss.Append(".Header {background-color:" + format.HeaderColor + "}");
			sbCss.Append(".Row    {background-color:" + format.RowColor + "}");
			sbCss.Append(".AltRow    {background-color:" + format.RowAltColor + "}");
			sbCss.Append("</style>");

			//this.SetStatusText = "Created style for html";

			sbHtml.Append("<html>");
			sbHtml.Append("<head><title>Export from " + source.TableName + "</title>");
			sbHtml.Append(sbCss.ToString());
			sbHtml.Append("</head>");
			sbHtml.Append("<body>");

			int fields = source.Columns.Count;
			sbHtml.Append("<table border='0' cellpadding='2'");
			sbHtml.Append("<tr>");
			for (int i = 0; i < fields; i++)
			{
				sbHtml.Append(string.Format("<td class='Header'>{0}</td>", source.Columns[i].ColumnName));

				if (OnWrittenData != null)
				{
					OnWrittenData("Writing column name " + i);
				}
			}
			sbHtml.Append("</tr>");

			int counter = 0;
			foreach (DataRow dr in source.Rows)
			{
				sbHtml.Append("<tr>");

				for (int i = 0; i < fields; i++)
				{
					if (isAltSet)
					{
						sbHtml.Append(string.Format("<td class='AltRow'>{0}</td>", dr[i]));
					}
					else
					{
						sbHtml.Append(string.Format("<td class='Row'>{0}</td>", dr[i]));
					}
				}
				counter++;
				if (OnWrittenData != null)
				{
					OnWrittenData("Writing row " + counter);
				}

				sbHtml.Append("</tr>");

				if (isAltSet == false)
					isAltSet = true;
				else
					isAltSet = false;
			}
			sbHtml.Append("</table>");
			sbHtml.Append("</body></html>");


			System.IO.TextWriter tw = new System.IO.StreamWriter(fileName);
			tw.WriteLine(sbHtml.ToString());
			tw.Close();
			if (OnWrittenData != null)
			{
				OnWrittenData("Finished exporting to html file");
			}
		}
	}
}