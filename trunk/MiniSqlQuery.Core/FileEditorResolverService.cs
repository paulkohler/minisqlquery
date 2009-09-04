using System;
using System.IO;

namespace MiniSqlQuery.Core
{
	public interface IFileEditorResolver
	{
		IEditor ResolveEditorInstance(string filename);
		string ResolveEditorNameByExtension(string extension);
	}

	public class FileEditorResolverService : IFileEditorResolver
	{
		private readonly IApplicationServices _services;

		public FileEditorResolverService(IApplicationServices services)
		{
			_services = services;
		}

		public string ResolveEditorNameByExtension(string ext)
		{
			string editroName = "default-editor";

			if (ext.StartsWith("."))
			{
				ext = ext.Substring(1);
			}

			switch (ext.ToLower())
			{
				case "sql":
				case "mt":
				case "cs":
				case "vb":
				case "xml":
				case "htm":
				case "html":
				case "txt":
					editroName = ext + "-editor";
					break;
			}

			return editroName;
		}


		public IEditor ResolveEditorInstance(string filename)
		{
			string ext = Path.GetExtension(filename);
			string editorName = ResolveEditorNameByExtension(ext);
			return _services.Resolve<IEditor>(editorName);
		}
	}
}