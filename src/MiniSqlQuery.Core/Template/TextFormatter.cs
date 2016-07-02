#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license
#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core.Template
{
	/// <summary>The i text formatter.</summary>
	public interface ITextFormatter
	{
		/// <summary>The format.</summary>
		/// <param name="text">The text.</param>
		/// <param name="items">The items.</param>
		/// <returns>The format.</returns>
		string Format(string text, Dictionary<string, object> items);
	}
}