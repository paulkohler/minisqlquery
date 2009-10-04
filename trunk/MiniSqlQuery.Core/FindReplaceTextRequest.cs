#region License
// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion
using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// A class that encapsulates a "find text" request, storing the position 
	/// </summary>
	public class FindTextRequest
	{
		private static string _replaceValue;
		private static string _searchValue;

		/// <summary>
		/// Creates a new request using the specified <paramref name="textProvider"/> for searching.
		/// </summary>
		/// <param name="textProvider">The search provider for this request,</param>
		public FindTextRequest(IFindReplaceProvider textProvider)
			: this(textProvider, null)
		{
		}

		/// <summary>
		/// Creates a new request using the specified <paramref name="textProvider"/> for searching.
		/// </summary>
		/// <param name="textProvider">The search provider for this request,</param>
		/// <param name="searchValue">The text to be searched on.</param>
		public FindTextRequest(IFindReplaceProvider textProvider, string searchValue)
		{
			TextProvider = textProvider;
			if (searchValue != null)
			{
				SearchValue = searchValue;
			}
			Position = 0;
			StringComparison = StringComparison.CurrentCultureIgnoreCase;
		}

		/// <summary>
		/// The search text (shared value).
		/// </summary>
		/// <value>The search value.</value>
		public string SearchValue
		{
			get { return _searchValue; }
			set { _searchValue = value; }
		}

		/// <summary>
		/// Gets or sets the text replace value (shared value).
		/// </summary>
		/// <value>The replace value.</value>
		public string ReplaceValue
		{
			get { return _replaceValue; }
			set { _replaceValue = value; }
		}

		/// <summary>
		/// The position of the currently "found" text (or the starting position of the search).
		/// </summary>
		public int Position { get; set; }

		///// <summary>
		///// If true, signals the <see cref="TextProvider"/> to search "up", otherwise "down".
		///// </summary>
		///// <value>True to search up, false for down (the default).</value>
		//public bool SearchUp { get; set; }

		/// <summary>
		/// The string comparison settings, e.g. case insensitive.
		/// </summary>
		public StringComparison StringComparison { get; set; }

		/// <summary>
		/// The search provider. A search request is conducted by the provider, different providers
		/// can yield different results, for example plain text or a regular expression searcher.
		/// </summary>
		public IFindReplaceProvider TextProvider { get; set; }
	}
}