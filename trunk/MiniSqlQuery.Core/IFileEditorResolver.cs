#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>The i file editor resolver.</summary>
	public interface IFileEditorResolver
	{
		/// <summary>The get file types.</summary>
		/// <returns></returns>
		FileEditorDescriptor[] GetFileTypes();

		/// <summary>The register.</summary>
		/// <param name="fileEditorDescriptor">The file editor descriptor.</param>
		void Register(FileEditorDescriptor fileEditorDescriptor);

		/// <summary>The resolve editor instance.</summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		IEditor ResolveEditorInstance(string filename);

		/// <summary>The resolve editor name by extension.</summary>
		/// <param name="extension">The extension.</param>
		/// <returns>The resolve editor name by extension.</returns>
		string ResolveEditorNameByExtension(string extension);
	}
}