#region License

// Copyright 2005-2019 Paul Kohler (https://github.com/paulkohler/minisqlquery). All rights reserved.
// This source code is made available under the terms of the GNU Lesser General Public License v3.0
// https://github.com/paulkohler/minisqlquery/blob/master/LICENSE

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A control that allows its text to be "found" and optionally "replaced".
	/// 	The query editor is an obvious provider but other windows can also provide
	/// 	find/replace functionality by implementing this interface (tools, output windows etc).
	/// </summary>
	public interface IFindReplaceProvider : ISupportCursorOffset
	{
		/// <summary>
		/// 	Gets a value indicating whether the text can be replaced, otherwise false.
		/// </summary>
		/// <value>The can replace text.</value>
		bool CanReplaceText { get; }

		/// <summary>
		/// 	Gets a reference to the text finding service.
		/// </summary>
		/// <seealso cref = "SetTextFindService" />
		/// <value>The text find service.</value>
		ITextFindService TextFindService { get; }

		/// <summary>
		/// 	Attemps to find <paramref name = "value" /> in the controls text.
		/// </summary>
		/// <param name = "value">The string to search for.</param>
		/// <param name = "startIndex">The starting position within the buffer.</param>
		/// <param name = "comparisonType">The string comparison type to use, e.g. <see cref="StringComparison.InvariantCultureIgnoreCase"/></param>
		/// <returns>The find string.</returns>
		int FindString(string value, int startIndex, StringComparison comparisonType);

		/// <summary>
		/// 	Replaces the text from <paramref name = "startIndex" /> for <paramref name = "length" /> characters 
		/// 	with <paramref name = "value" />.
		/// </summary>
		/// <param name = "value">The new string.</param>
		/// <param name = "startIndex">the starting position.</param>
		/// <param name = "length">The length (0 implies an insert).</param>
		/// <returns>True if successful, otherwise false.</returns>
		/// <seealso cref = "CanReplaceText" />
		bool ReplaceString(string value, int startIndex, int length);

		/// <summary>
		/// 	Overrides the default text find service with <paramref name = "textFindService" /> (e.g. a RegEx service).
		/// </summary>
		/// <param name = "textFindService">The service to use.</param>
		void SetTextFindService(ITextFindService textFindService);
	}
}