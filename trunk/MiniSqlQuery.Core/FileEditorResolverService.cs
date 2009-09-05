using System;
using System.Collections.Generic;
using System.IO;

namespace MiniSqlQuery.Core
{
	public class FileEditorResolverService : IFileEditorResolver
	{
		private readonly IApplicationServices _services;
		private readonly List<string> _extentions;

		public FileEditorResolverService(IApplicationServices services)
		{
			_services = services;
			_extentions=new List<string>();
		}

		public string ResolveEditorNameByExtension(string extention)
		{
			string editorName = "default-editor";

			if (extention != null)
			{
				if (extention.StartsWith("."))
				{
					extention = extention.Substring(1);
				}

				// is there a specific editor for this file type
				if (_extentions.Contains(extention))
				{
					editorName = extention + "-editor";
				}
			}

			return editorName;
		}

		public void Register(string extention)
		{
			_extentions.Add(extention);
		}


		public IEditor ResolveEditorInstance(string filename)
		{
			string ext = Path.GetExtension(filename);
			string editorName = ResolveEditorNameByExtension(ext);
			return _services.Resolve<IEditor>(editorName);
		}
	}
}