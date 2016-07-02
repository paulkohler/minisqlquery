#region License

// Copyright 2005-2009 Paul Kohler (https://github.com/paul-kohler-au/minisqlquery). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion

using System;
using System.Collections.Generic;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	Used for sorting plugins at load time, a very simple ordering system for the plugins.
	/// </summary>
	public class PlugInComparer : IComparer<IPlugIn>
	{
		/// <summary>
		/// 	Orders two plugin classes.
		/// </summary>
		/// <param name = "x">The left side object.</param>
		/// <param name = "y">The right side object.</param>
		/// <returns>The compare result.</returns>
		public int Compare(IPlugIn x, IPlugIn y)
		{
			int result;

			if (x == null && y == null)
			{
				result = 0;
			}
			else if (y == null)
			{
				result = -1;
			}
			else if (x == null)
			{
				result = 1;
			}
			else
			{
				int numX = x.RequestedLoadOrder;
				int numY = y.RequestedLoadOrder;

				result = numX - numY;
			}

			return result;
		}
	}
}