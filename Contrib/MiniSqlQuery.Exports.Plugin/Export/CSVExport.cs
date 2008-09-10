using System;
using System.Data;
using System.IO;

namespace MiniSqlQuery.Exports.Plugin.Export
{
	public class CSVExport
	{
		#region Delegates

		public delegate void WrittenData(string Text);

		#endregion

		public static event WrittenData OnWrittenData;

		public static void ExportToCSV(DataTable source, string FileName, bool FileNamesFirstRow)
		{
			// Create the CSV file to which grid data will be exported.
			StreamWriter sw = new StreamWriter(FileName, false);
			// First we will write the headers.
			DataTable dt = source;
			int iColCount = dt.Columns.Count;

			if (FileNamesFirstRow)
			{
				for (int i = 0; i < iColCount; i++)
				{
					sw.Write(dt.Columns[i]);
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
			int Counter = 0;
			foreach (DataRow dr in dt.Rows)
			{
				for (int i = 0; i < iColCount; i++)
				{
					if (!Convert.IsDBNull(dr[i]))
					{
						sw.Write(dr[i].ToString());
					}
					if (i < iColCount - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
				Counter++;
				if (OnWrittenData != null)
				{
					OnWrittenData(string.Format("Wrote row {0}", Counter));
				}
			}
			sw.Close();
			if (OnWrittenData != null)
			{
				OnWrittenData("Finished exporting CSV file to " + FileName);
			}
		}
	}
}