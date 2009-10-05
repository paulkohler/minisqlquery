#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// The functions of the editing window.
	/// </summary>
	public interface IQueryEditor : IPerformTask, IFindReplaceProvider, INavigatableDocument, IQueryBatchProvider, IEditor
	{
		/// <summary>
		/// Provides access to the actual editor control.
		/// </summary>
		Control EditorControl { get; }

		/// <summary>
		/// Sets the "status" text for the form.
		/// </summary>
		/// <param name="text">The message to appear in the status bar.</param>
		void SetStatus(string text);
	}
}
