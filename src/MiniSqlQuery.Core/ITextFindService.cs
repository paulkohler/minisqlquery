#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	A text finding serice interface. A window can implement this interface and sit will allow searching of its text.
	/// </summary>
	public interface ITextFindService
	{
		/// <summary>
		/// 	Finds the next string of text depending on the contrnts of the <paramref name = "request" />.
		/// </summary>
		/// <param name = "request">The request.</param>
		/// <returns>A find request with position updated.</returns>
		FindTextRequest FindNext(FindTextRequest request);
	}
}