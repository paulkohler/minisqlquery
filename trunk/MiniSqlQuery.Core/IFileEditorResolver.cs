using System;

namespace MiniSqlQuery.Core
{
	public interface IFileEditorResolver
	{
		IEditor ResolveEditorInstance(string filename);
		string ResolveEditorNameByExtension(string extension);
		void Register(FileEditorDescriptor fileEditorDescriptor);
		FileEditorDescriptor[] GetFileTypes();
	}
}