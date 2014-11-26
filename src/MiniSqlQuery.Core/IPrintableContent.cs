#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Drawing.Printing;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	If implemented it signals that the class supports printing of the "contents" of the object.
	/// </summary>
	public interface IPrintableContent
	{
		/// <summary>
		/// 	Gets the "document" to print (or null if not supported in the current context).
		/// </summary>
		/// <value>The print document.</value>
		PrintDocument PrintDocument { get; }
	}
}