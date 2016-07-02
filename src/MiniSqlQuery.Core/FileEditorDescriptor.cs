#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// <para>The file editor descriptor.</para>
	/// <para>Creates a relationship between file extensions and a type of editor that supports for example XML syntax highlighting.</para>
	/// </summary>
	public class FileEditorDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="FileEditorDescriptor"/> class.</summary>
		public FileEditorDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="FileEditorDescriptor"/> class.</summary>
		/// <param name="name">The name for display purposes, e.g. "HTML Editor".</param>
		/// <param name="editorKeyName">The editor key name, can be used by the application to resolve an editor by name.</param>
		/// <param name="extensions">The file extensions to support for this editor (e.g. "txt").</param>
		/// <remarks>
		/// <example>
		/// <code>var fd = new FileEditorDescriptor("HTML Editor", "htm-editor", "htm", "html");</code>
		/// </example>
		/// </remarks>
		public FileEditorDescriptor(string name, string editorKeyName, params string[] extensions)
		{
			Name = name;
			EditorKeyName = editorKeyName;
			Extensions = extensions;
		}

		/// <summary>Gets or sets EditorKeyName.</summary>
		/// <value>The editor key name.</value>
		public string EditorKeyName { get; set; }

		/// <summary>Gets or sets Extensions.</summary>
		/// <value>The extensions.</value>
		public string[] Extensions { get; set; }

		/// <summary>Gets or sets Name.</summary>
		/// <value>The display name of the descriptor.</value>
		public string Name { get; set; }

		/// <summary>Converts the descriptor to a string.</summary>
		/// <returns>A string represntation of the descriptor.</returns>
		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, string.Join("; ", Extensions));
		}
	}
}