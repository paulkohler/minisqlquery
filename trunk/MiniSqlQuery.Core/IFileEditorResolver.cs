#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
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