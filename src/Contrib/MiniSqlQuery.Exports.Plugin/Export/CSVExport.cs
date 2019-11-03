using System;
using System.Data;
using System.IO;

namespace MiniSqlQuery.Exports.Plugin.Export
{
    public class CSVExport
    {
        #region Delegates

        public delegate void WrittenData(string text);

        #endregion

        public static event WrittenData OnWrittenData;

        public static void ExportToCSV(DataTable source, string fileName, bool fileNamesFirstRow)
        {
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter(fileName, false);
            // First we will write the headers.
            DataTable dt = source;
            int iColCount = dt.Columns.Count;

            if (fileNamesFirstRow)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    CsvWrite(sw, dt.Columns[i].ColumnName);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                    if (OnWrittenData != null)
                    {
                        OnWrittenData(string.Format("Wrote column name {0}", i));
                    }
                }
                sw.Write(sw.NewLine);
                if (OnWrittenData != null)
                {
                    OnWrittenData("Wrote filednames row..");
                }
            }
            // Now write all the rows.
            int counter = 0;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        CsvWrite(sw, dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                counter++;
                if (OnWrittenData != null)
                {
                    OnWrittenData(string.Format("Wrote row {0}", counter));
                }
            }
            sw.Close();
            if (OnWrittenData != null)
            {
                OnWrittenData("Finished exporting CSV file to " + fileName);
            }
        }

        /// <summary>
        /// Perform a CSV compliant wtrite of <paramref name="text"/> to the <paramref name="sw"/>.
        /// Handles commas, quotes and newlines.
        /// </summary>
        /// <param name="sw">The writer.</param>
        /// <param name="text">The text.</param>
        private static void CsvWrite(TextWriter sw, string text)
        {
            if (text != null)
            {
                bool needsQuotes = false;
                if (text.Contains("\"") || text.Contains(",") || text.Contains("\n"))
                {
                    needsQuotes = true;
                    text = text.Replace("\"", "\"\"");
                }
                if (needsQuotes)
                {
                    text = string.Concat("\"", text, "\"");
                }
                sw.Write(text);
            }
        }
    }
}