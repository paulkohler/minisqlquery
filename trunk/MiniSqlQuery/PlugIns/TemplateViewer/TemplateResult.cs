using System;

namespace MiniSqlQuery.PlugIns.TemplateViewer
{
	public class TemplateResult
	{
		public string Text { get; set; }
		public string Extension { get; set; }

		public string SyntaxName
		{
			get
			{
				string ext = Extension ?? "";
				switch (ext.ToLower())
				{
					case "cs":
						return "C#";

					default:
						return "SQL";
				}
			}
		}
	}
}