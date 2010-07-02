#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>The file editor descriptor.</summary>
	public class FileEditorDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="FileEditorDescriptor"/> class.</summary>
		public FileEditorDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="FileEditorDescriptor"/> class.</summary>
		/// <param name="name">The name.</param>
		/// <param name="editorKeyName">The editor key name.</param>
		/// <param name="extensions">The extensions.</param>
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
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>The to string.</summary>
		/// <returns>The to string.</returns>
		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, string.Join("; ", Extensions));
		}
	}
}