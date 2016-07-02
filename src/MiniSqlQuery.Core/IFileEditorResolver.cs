#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	The file editor resolver interface.
	///		Given a file name or extention the service will work out the most appropriate editor to use.
	/// </summary>
	public interface IFileEditorResolver
	{
		/// <summary>
		/// 	Gets an array of the file descriptiors.
		/// </summary>
		/// <returns>An array of <see cref = "FileEditorDescriptor" /> objects.</returns>
		FileEditorDescriptor[] GetFileTypes();

		/// <summary>
		/// 	Registers the specified file editor descriptor.
		/// </summary>
		/// <param name = "fileEditorDescriptor">The file editor descriptor.</param>
		void Register(FileEditorDescriptor fileEditorDescriptor);

		/// <summary>
		/// 	Resolves the editor instance from the container based on the filename.
		/// </summary>
		/// <param name = "filename">The filename.</param>
		/// <returns>An editor.</returns>
		IEditor ResolveEditorInstance(string filename);

		/// <summary>
		/// 	Works out the "name" of the editor to use based on the <paramref name = "extension" />.
		/// </summary>
		/// <param name = "extension">The extention ("sql", "txt"/".txt" etc).</param>
		/// <returns>The name of an editor in the container.</returns>
		string ResolveEditorNameByExtension(string extension);
	}
}