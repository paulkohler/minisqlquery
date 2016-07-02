#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A class that encapsulates a "find text" request, storing the position
	/// </summary>
	public class FindTextRequest
	{
		/// <summary>
		/// 	The _replace value.
		/// </summary>
		private static string _replaceValue;

		/// <summary>
		/// 	The _search value.
		/// </summary>
		private static string _searchValue;

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "FindTextRequest" /> class. Creates a new request using the specified <paramref name = "textProvider" /> for searching.
		/// </summary>
		/// <param name = "textProvider">The search provider for this request,</param>
		public FindTextRequest(IFindReplaceProvider textProvider)
			: this(textProvider, null)
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the <see cref = "FindTextRequest" /> class. Creates a new request using the specified <paramref name = "textProvider" /> for searching.
		/// </summary>
		/// <param name = "textProvider">The search provider for this request,</param>
		/// <param name = "searchValue">The text to be searched on.</param>
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
		/// 	Gets or sets the position of the currently "found" text (or the starting position of the search).
		/// </summary>
		/// <value>The position.</value>
		public int Position { get; set; }

		/// <summary>
		/// 	Gets or sets the text replace value (shared value).
		/// </summary>
		/// <value>The replace value.</value>
		public string ReplaceValue
		{
			get { return _replaceValue; }
			set { _replaceValue = value; }
		}

		/// <summary>
		/// 	Gets or sets the search text (shared value).
		/// </summary>
		/// <value>The search value.</value>
		public string SearchValue
		{
			get { return _searchValue; }
			set { _searchValue = value; }
		}

		/*
		/// <summary>
		/// If true, signals the <see cref="TextProvider"/> to search "up", otherwise "down".
		/// </summary>
		/// <value>True to search up, false for down (the default).</value>
		public bool SearchUp { get; set; }
		*/

		/// <summary>
		/// 	Gets or sets the string comparison settings, e.g. case insensitive.
		/// </summary>
		/// <value>The string comparison.</value>
		public StringComparison StringComparison { get; set; }

		/// <summary>
		/// 	Gets or sets the search provider. A search request is conducted by the provider, different providers
		/// 	can yield different results, for example plain text or a regular expression searcher.
		/// </summary>
		/// <value>The text provider.</value>
		public IFindReplaceProvider TextProvider { get; set; }
	}
}