#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Collections.Generic;
using System.IO;

namespace MiniSqlQuery.Core
{
	public class FileEditorResolverService : IFileEditorResolver
	{
		private readonly IApplicationServices _services;
		private readonly Dictionary<string, FileEditorDescriptor> _extentionMap;
		private readonly List<FileEditorDescriptor> _fileEditorDescriptors;

		public FileEditorResolverService(IApplicationServices services)
		{
			_services = services;
			_extentionMap= new Dictionary<string, FileEditorDescriptor>();
			_fileEditorDescriptors = new List<FileEditorDescriptor>();
		}

		public string ResolveEditorNameByExtension(string extention)
		{
			string editorName = _extentionMap["*"].EditorKeyName;

			if (extention != null)
			{
				if (extention.StartsWith("."))
				{
					extention = extention.Substring(1);
				}

				// is there a specific editor for this file type
				if (_extentionMap.ContainsKey(extention))
				{
					editorName = _extentionMap[extention].EditorKeyName;
				}
			}

			return editorName;
		}

		public void Register(FileEditorDescriptor fileEditorDescriptor)
		{
			_fileEditorDescriptors.Add(fileEditorDescriptor);
			if (fileEditorDescriptor.Extensions == null || fileEditorDescriptor.Extensions.Length == 0)
			{
				_extentionMap.Add("*", fileEditorDescriptor);
			}
			else
			{
				// create a map of all ext to editors
				foreach (string extention in fileEditorDescriptor.Extensions)
				{
					_extentionMap.Add(extention, fileEditorDescriptor);
				}
			}
		}

		public FileEditorDescriptor[] GetFileTypes()
		{
			return _fileEditorDescriptors.ToArray();
		}


		public IEditor ResolveEditorInstance(string filename)
		{
			string ext = Path.GetExtension(filename);
			string editorName = ResolveEditorNameByExtension(ext);
			return _services.Resolve<IEditor>(editorName);
		}
	}
}