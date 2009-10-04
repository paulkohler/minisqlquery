#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core
{
	public class FileEditorDescriptor
	{
		public FileEditorDescriptor()
		{
		}

		public FileEditorDescriptor(string name, string editorKeyName, params string[] extensions)
		{
			Name = name;
			EditorKeyName = editorKeyName;
			Extensions = extensions;
		}

		public string Name { get; set; }
		public string EditorKeyName { get; set; }
		public string[] Extensions { get; set; }

		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, string.Join("; ", Extensions));
		}
	}
}