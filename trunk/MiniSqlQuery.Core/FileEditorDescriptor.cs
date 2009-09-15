using System;

namespace MiniSqlQuery.Core
{
	public class FileEditorDescriptor
	{
		public FileEditorDescriptor()
		{
		}

		public FileEditorDescriptor(string name, string editorKeyName, params string[] extentions)
		{
			Name = name;
			EditorKeyName = editorKeyName;
			Extentions = extentions;
		}

		public string Name { get; set; }
		public string EditorKeyName { get; set; }
		public string[] Extentions { get; set; }

		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, string.Join("; ", Extentions));
		}
	}
}